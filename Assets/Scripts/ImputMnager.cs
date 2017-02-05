using UnityEngine;
using System.Collections;

public enum MoveDirection
{
	Left, Right, Up, Down
}

public class ImputMnager : MonoBehaviour {

	private GameManager gm;

	void Awake(){

		gm= GameObject.FindObjectOfType<GameManager> ();

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.RightArrow)){

		    //Right Move
			gm.Move(MoveDirection.Right);
		}
		else if(Input.GetKeyDown(KeyCode.LeftArrow)){

			//Left Move
			gm.Move(MoveDirection.Left);
		}	
		else if(Input.GetKeyDown(KeyCode.UpArrow)){

			//Up Move
			gm.Move(MoveDirection.Up);
		}
		else if(Input.GetKeyDown(KeyCode.DownArrow)){

			//Down Move
			gm.Move(MoveDirection.Down);
		}
	}
}
