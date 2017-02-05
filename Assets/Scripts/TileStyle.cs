using UnityEngine;
using System.Collections;

[System.Serializable]
public class TileStyleHolder{

	public int TileNumber = 2;
	public Color32 TileColor = new Color32(255,255,255,255);
	public Color32 TextColor = new Color32 (0, 0, 0, 0);
}


public class TileStyle : MonoBehaviour {

	public static TileStyle Instance;
	public TileStyleHolder[] TileStyleHeld;


	void Awake(){

		Instance = this;

	}

}
