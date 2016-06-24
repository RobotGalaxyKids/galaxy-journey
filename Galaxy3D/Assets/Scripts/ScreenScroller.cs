using UnityEngine;
using System.Collections;

public class ScreenScroller : MonoBehaviour {

	public GameObject CameCamera;

	bool onDrag = false;
	Vector3 orgPos;
	public float speed = 0f;

	public bool canSwipe =false;

	bool goLeft = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(onDrag){

			//print("Draging " + onDrag);
			OnDragScreen();

		}else if(speed> 0){

			slowStop();

		}

	}

	void OnMouseDown(){
		onDrag = true;


		Vector3 mousePos2D = Input.mousePosition;
		mousePos2D.z = -Camera.main.transform.position.z;
		orgPos = Camera.main.ScreenToWorldPoint (mousePos2D);

	//	print("Hold" + orgPos );



	}

	void OnMouseUp(){

		Vector3 mousePos2D = Input.mousePosition;
		mousePos2D.z = -Camera.main.transform.position.z;


		onDrag = false;
		//print("Release"+ Camera.main.ScreenToWorldPoint (mousePos2D));

	}

	void OnDragScreen(){

		if(!canSwipe) return;
		
		Vector3 mousePos2D = Input.mousePosition;
		mousePos2D.z = -Camera.main.transform.position.z;
		speed =  Mathf.Abs(Camera.main.ScreenToWorldPoint (mousePos2D).x - orgPos.x)*3f;

		if(Camera.main.ScreenToWorldPoint (mousePos2D).x < orgPos.x -2 && CameCamera.transform.position.x < 154){
			goLeft = false;
			CameCamera.transform.position = Vector3.MoveTowards(CameCamera.transform.position,new Vector3 (CameCamera.transform.position.x+50,CameCamera.transform.position.y,CameCamera.transform.position.z),speed*Time.deltaTime);
			orgPos.x +=speed*Time.deltaTime;
		}else if (Camera.main.ScreenToWorldPoint (mousePos2D).x > orgPos.x +2 && CameCamera.transform.position.x > -21){
			goLeft = true;
			CameCamera.transform.position = Vector3.MoveTowards(CameCamera.transform.position,new Vector3 (CameCamera.transform.position.x-50,CameCamera.transform.position.y,CameCamera.transform.position.z),speed*Time.deltaTime);
			orgPos.x -=speed*Time.deltaTime;
		}


//		print();

	}

	void slowStop(){

		if(!canSwipe) return;

		if(goLeft&& CameCamera.transform.position.x > -21){

			CameCamera.transform.position = Vector3.MoveTowards(CameCamera.transform.position,new Vector3 (CameCamera.transform.position.x-50,CameCamera.transform.position.y,CameCamera.transform.position.z),speed*Time.deltaTime);
			orgPos.x -=speed*Time.deltaTime;

		}else if((!goLeft) && CameCamera.transform.position.x < 256){

			CameCamera.transform.position = Vector3.MoveTowards(CameCamera.transform.position,new Vector3 (CameCamera.transform.position.x+50,CameCamera.transform.position.y,CameCamera.transform.position.z),speed*Time.deltaTime);
			orgPos.x +=speed*Time.deltaTime;

		}

		speed -= 200* Time.deltaTime ;

	}

}
