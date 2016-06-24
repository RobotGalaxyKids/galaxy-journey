using UnityEngine;
using System.Collections;

public class OverAllCameraLook : MonoBehaviour {

		Quaternion rotation;
		public GameObject spaceShip;
		float distance ;  
		public bool KeepDistance2ndStage = true, finishFirstMove = false;
	
	// Use this for initialization
	void Awake () {
//		distance = spaceShip.transform.position.z -  this.transform.position.z;
		rotation = transform.rotation;
//		distance = spaceShip.transform.position.z -  this.transform.position.z;
		distance = 15;
		//print(distance);
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		if(KeepDistance2ndStage){


			transform.position =  Vector3.MoveTowards(transform.position, new Vector3(spaceShip.transform.position.x,spaceShip.transform.position.y+0f,spaceShip.transform.position.z - distance),50f*Time.deltaTime) ;


			if(transform.position == new Vector3(spaceShip.transform.position.x,spaceShip.transform.position.y+0f,spaceShip.transform.position.z - distance)){

				finishFirstMove = true;

			}

//			transform.position = new Vector3(spaceShip.transform.position.x,spaceShip.transform.position.y+5f,spaceShip.transform.position.z - distance);
//			transform.localPosition = Vector3.MoveTowards(transform.localPosition,new Vector3(spaceShip.transform.localPosition.x,spaceShip.transform.localPosition.y,-38),20*Time.deltaTime);
		}


	}


	public void SetOnShip(){

		transform.position = new Vector3(spaceShip.transform.position.x,spaceShip.transform.position.y+0f,spaceShip.transform.position.z - distance);


	}
}
