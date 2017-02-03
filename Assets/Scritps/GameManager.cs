using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject youWonText;
    public GameObject gameOverText;
    public Text gameOverScoreText;
    public GameObject gameOverPanel;

    private Tile[,] allTiles = new Tile[4, 4];
    private List<Tile> emptyTiles = new List<Tile>();
    private List<Tile[]> columns = new List<Tile[]>();
    private List<Tile[]> rows = new List<Tile[]>();

    // Use this for initialization
    void Start()
    {
        Tile[] AllTilesOneDim = GameObject.FindObjectsOfType<Tile>();
        foreach (Tile t in AllTilesOneDim)
        {

            t.Number = 0;
            allTiles[t.indRow, t.indCol] = t;
            emptyTiles.Add(t);
        }

        //Setting the values of rows and cloumns to list
        #region 
        columns.Add(new Tile[] { allTiles[0, 0], allTiles[1, 0], allTiles[2, 0], allTiles[3, 0] });
        columns.Add(new Tile[] { allTiles[0, 1], allTiles[1, 1], allTiles[2, 1], allTiles[3, 1] });
        columns.Add(new Tile[] { allTiles[0, 2], allTiles[1, 2], allTiles[2, 2], allTiles[3, 2] });
        columns.Add(new Tile[] { allTiles[0, 3], allTiles[1, 3], allTiles[2, 3], allTiles[3, 3] });

        rows.Add(new Tile[] { allTiles[0, 0], allTiles[0, 1], allTiles[0, 2], allTiles[0, 3] });
        rows.Add(new Tile[] { allTiles[1, 0], allTiles[1, 1], allTiles[1, 2], allTiles[1, 3] });
        rows.Add(new Tile[] { allTiles[2, 0], allTiles[2, 1], allTiles[2, 2], allTiles[2, 3] });
        rows.Add(new Tile[] { allTiles[3, 0], allTiles[3, 1], allTiles[3, 2], allTiles[3, 3] });
        #endregion

        //Generate two initial tiles
        Generate();
        Generate();
    }

    private void YouWon()
    {
        gameOverText.SetActive(false);
        youWonText.SetActive(true);
        gameOverScoreText.text = ScoreTracker.Instance.Score.ToString();
        gameOverPanel.SetActive(true);
    }
    private void GameOver()
    {
        gameOverScoreText.text = ScoreTracker.Instance.Score.ToString();
        gameOverPanel.SetActive(true);
    }

    bool CanMove()
    {
        if (emptyTiles.Count > 0)
            return true;
        else
        {
            //check columns
            for (int i = 0; i < columns.Count; i++)
                for (int j = 0; j < rows.Count - 1; j++)
                    if (allTiles[j, i].Number == allTiles[j + 1, i].Number)
                        return true;

            //check rows
            for (int i = 0; i < rows.Count; i++)
                for (int j = 0; j < columns.Count - 1; j++)
                    if (allTiles[i, j].Number == allTiles[i, j+1].Number)
                        return true;

            return false;
        }
    }

    private void UpdateEmptyTiles()
    {
        emptyTiles.Clear();
        foreach (Tile t in allTiles)
        {
            if (t.Number == 0)
                emptyTiles.Add(t);
        }
    }

    public void Move(MoveDirection md)
    {
        Debug.Log(md.ToString() + " move.");

        bool moveMade = false;
        ResetMergedFlags();

        for (int i = 0; i < rows.Count; i++)
            switch (md)
            {
                case MoveDirection.Down:
                    while (MakeOneMoveUpIndex(columns[i]))
                        moveMade = true;
                    break;
                case MoveDirection.Right:
                    while (MakeOneMoveUpIndex(rows[i]))
                        moveMade = true;
                    break;
                case MoveDirection.Up:
                    while (MakeOneMoveDownIndex(columns[i]))
                        moveMade = true;
                    break;
                case MoveDirection.Left:
                    while (MakeOneMoveDownIndex(rows[i]))
                        moveMade = true;
                    break;
            }
        if (moveMade)
        {
            UpdateEmptyTiles();
            Generate();

            if (!CanMove())
                GameOver();
        }
    }

    //Make one move Down Index UP and Left moves in the game 
    //This move decreases the index of the 
    bool MakeOneMoveDownIndex(Tile[] lineOfTiles)
    {
        for (int i = 0; i < lineOfTiles.Length - 1; i++)
        {

            //MOVE BLOCK
            //Swap the empty and filled tiles
            if (lineOfTiles[i].Number == 0 && lineOfTiles[i + 1].Number != 0)
            {
                lineOfTiles[i].Number = lineOfTiles[i + 1].Number;
                lineOfTiles[i + 1].Number = 0;
                return true;
            }

            //MERGE BLOCK
            if (lineOfTiles[i].Number != 0 && lineOfTiles[i].Number == lineOfTiles[i + 1].Number &&
               lineOfTiles[i].mergedThisTurn == false && lineOfTiles[i + 1].mergedThisTurn == false)
            {
                lineOfTiles[i].Number *= 2;
                lineOfTiles[i + 1].Number = 0;
                lineOfTiles[i].mergedThisTurn = true;
                ScoreTracker.Instance.Score += lineOfTiles[i].Number;
                if (lineOfTiles[i].Number == 2048)
                    YouWon();
                return true;
            }
        }
        return false;
    }

    //Make one move Up Index Down and Right moves in the game 
    //This move increase the index of the tile
    bool MakeOneMoveUpIndex(Tile[] lineOfTiles)
    {
        for (int i = lineOfTiles.Length - 1; i > 0; i--)
        {

            //MOVE BLOCK
            //Swap the empty and filled tiles
            if (lineOfTiles[i].Number == 0 && lineOfTiles[i - 1].Number != 0)
            {
                lineOfTiles[i].Number = lineOfTiles[i - 1].Number;
                lineOfTiles[i - 1].Number = 0;
                return true;
            }

            //MERGE BLOCK
            if (lineOfTiles[i].Number != 0 && lineOfTiles[i].Number == lineOfTiles[i - 1].Number &&
               lineOfTiles[i].mergedThisTurn == false && lineOfTiles[i - 1].mergedThisTurn == false)
            {
                lineOfTiles[i].Number *= 2;
                lineOfTiles[i - 1].Number = 0;
                lineOfTiles[i].mergedThisTurn = true;
                ScoreTracker.Instance.Score += lineOfTiles[i].Number;
                if (lineOfTiles[i].Number == 2048)
                    YouWon();
                return true;
            }
        }
        return false;
    }

    public void NewGameButtonHandler()
    {
        gameOverPanel.SetActive(false);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
        Application.LoadLevel(Application.loadedLevel);
    }

    void Generate()
    {
        if (emptyTiles.Count > 0)
        {
            int indexForNewNumber = Random.Range(0, emptyTiles.Count);
            int randomNum = Random.Range(0, 10);
            if (randomNum == 0)
                emptyTiles[indexForNewNumber].Number = 4;
            else
                emptyTiles[indexForNewNumber].Number = 2;
            emptyTiles.RemoveAt(indexForNewNumber);
            Debug.Log("Tiles Generated");
        }
    }

    private void ResetMergedFlags()
    {
        foreach (Tile t in allTiles)
            t.mergedThisTurn = false;
    }
}
