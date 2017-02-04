using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{

    public bool mergedThisTurn = false;

    public int indRow;
    public int indCol;

    public int Number
    {
        get
        {
            return number;
        }
        set
        {
            number = value;
            if (number == 0)
                SetEmpty();
            else
            {
                ApplyStyle(number);
                SetVisible();
            }
        }
    }
    private int number;

    private Text tileText;
    private Image tileImage;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        tileText = GetComponentInChildren<Text>();
        tileImage = transform.Find("NumberedTile").GetComponent<Image>();
    }

    public void PlayMergeAnimation()
    {
        anim.SetTrigger("Merge");
    }

    public void PlayAppearAnimation()
    {
        anim.SetTrigger("Appear");
    }

    void ApplyStyleFromHolder(int index)
    {
        tileText.text = TileStyleHolder.Instance.tileStyles[index].number.ToString();
        tileText.color = TileStyleHolder.Instance.tileStyles[index].textColor;
        tileImage.color = TileStyleHolder.Instance.tileStyles[index].tileColor;
    }
    void ApplyStyle(int num)
    {
        switch (num)
        {
            case 2:
                ApplyStyleFromHolder(0);
                break;
            case 4:
                ApplyStyleFromHolder(1);
                break;
            case 8:
                ApplyStyleFromHolder(2);
                break;
            case 16:
                ApplyStyleFromHolder(3);
                break;
            case 32:
                ApplyStyleFromHolder(4);
                break;
            case 64:
                ApplyStyleFromHolder(5);
                break;
            case 128:
                ApplyStyleFromHolder(6);
                break;
            case 256:
                ApplyStyleFromHolder(7);
                break;
            case 512:
                ApplyStyleFromHolder(8);
                break;
            case 1024:
                ApplyStyleFromHolder(9);
                break;
            case 2048:
                ApplyStyleFromHolder(10);
                break;
            case 4096:
                ApplyStyleFromHolder(11);
                break;
            default:
                Debug.LogError("Yo man! Check the number you are passing to the style");
                break;
        }
    }

    private void SetVisible()
    {
        tileImage.enabled = true;
        tileText.enabled = true;
    }
    private void SetEmpty()
    {
        tileImage.enabled = false;
        tileText.enabled = false;
    }

}
