using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

	int	currentPos = 0;


	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {


	}
//
//	public void MoveTo(Vector3 Destination){
//		destination = Destination;
//		speedY = Destination.y - orgPos.y;
//		speedX = Destination.x - orgPos.x;
//
//		
//
//
//	}

	public int GetPosOnBoard(){

		return currentPos;

	}

	public void SetPosOnBoard(int Pos){

		currentPos = Pos;

	}

}
