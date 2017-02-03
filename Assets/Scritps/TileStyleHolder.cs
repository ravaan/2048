using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileStyle
{
    public int number= 2;
    public Color32 tileColor = new Color32(216,198,182,0);
    public Color32 textColor = new Color32(0,0,0,0);
}

public class TileStyleHolder : MonoBehaviour {

    //SINGLETON
    public static TileStyleHolder Instance;

    public TileStyle[] tileStyles;

    void Awake()
    {
        Instance = this;
    }
}
