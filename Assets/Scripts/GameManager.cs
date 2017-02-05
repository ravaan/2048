using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	private Tile[,] AllTiles = new Tile[4, 4];
	private List<Tile> EmptyTiles= new List<Tile>();
	private List<Tile[]> columns = new List<Tile[]> (); 
	private List<Tile[]> rows= new List<Tile[]> ();

	void Start () {
		// Use this for initialization
		Tile[] AllTileOneDim = GameObject.FindObjectsOfType<Tile>();

		foreach (Tile t in AllTileOneDim) {

			t.Number = 0;
			AllTiles [t.indrow, t.indcol] = t;
			EmptyTiles.Add (t);
		
		}
		 
		columns.Add (new Tile[] {AllTiles[0,0], AllTiles[1,0], AllTiles[2,0], AllTiles[3,0]});
		columns.Add (new Tile[] {AllTiles[0,1], AllTiles[1,1], AllTiles[2,1], AllTiles[3,1]});
		columns.Add (new Tile[] {AllTiles[0,2], AllTiles[1,2], AllTiles[2,2], AllTiles[3,2]});
		columns.Add (new Tile[] {AllTiles[0,3], AllTiles[1,3], AllTiles[2,3], AllTiles[3,3]});

		rows.Add (new Tile[] {AllTiles[0,0], AllTiles[0,1], AllTiles[0,2], AllTiles[0,3]});
		rows.Add (new Tile[] {AllTiles[1,0], AllTiles[1,1], AllTiles[1,2], AllTiles[1,3]});
		rows.Add (new Tile[] {AllTiles[2,0], AllTiles[2,1], AllTiles[2,2], AllTiles[2,3]});
		rows.Add (new Tile[] {AllTiles[3,0], AllTiles[3,1], AllTiles[3,2], AllTiles[3,3]});

	}

	bool MakeOneMoveDownIndex(Tile[] LineoOfTiles){
		
		for (int i = 0; i < LineoOfTiles.Length-1; i++) {

			//MOVE BLOCK
			if ((LineoOfTiles [i].Number== 0) && (LineoOfTiles [i + 1].Number !=0)) {

				LineoOfTiles [i].Number = LineoOfTiles [i + 1].Number;
				LineoOfTiles [i + 1].Number = 0;
				return true;
			}

		}

		return false;
		
	}

	bool MakeOneMoveUpIndex(Tile[] LineoOfTiles){

		for (int i = LineoOfTiles.Length-1; i > 0  ; i--) {

			//MOVE BLOCK
			if (LineoOfTiles [i].Number == 0 && LineoOfTiles [i - 1].Number != 0) {

				LineoOfTiles [i].Number = LineoOfTiles [i - 1].Number;
				LineoOfTiles [i - 1].Number = 0;
				return true;
			}

		}

		return false;
	
	}

	void Generate(){

		if (EmptyTiles.Count > 0) {

			int indexfornewnumber = Random.Range (0, EmptyTiles.Count);
			int RandomNum = Random.Range (0, 10);

			if (RandomNum == 0)
				EmptyTiles [indexfornewnumber].Number = 4;

			else
				EmptyTiles [indexfornewnumber].Number = 2;
			
			EmptyTiles.RemoveAt (indexfornewnumber);
		}

	}
	

	void Update () {
	
		// Update is called once per frame

		if (Input.GetKeyDown (KeyCode.G))
			Generate ();

	}

	public void Move (MoveDirection md)	{

		Debug.Log (md.ToString() + " Mover.");
		for (int i = 0; i < rows.Count; i++) {

			switch(md){
				
			case MoveDirection.Down:
				while(MakeOneMoveUpIndex(columns[i])){}
				break;
			case MoveDirection.Left:
				while(MakeOneMoveDownIndex(rows[i])){}
				break;
			case MoveDirection.Right:
				while(MakeOneMoveUpIndex(rows[i])){}
				break;
			case MoveDirection.Up:
				while(MakeOneMoveDownIndex(columns[i])){}
				break;

			}
		}
	}

}
