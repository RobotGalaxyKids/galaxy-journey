using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {


	public GameObject Camecamecameha,Illusion,SpaceShip1,SpaceShip2;
	public GameObject[] TTaget;



	Vector3 orgPos,tPos;

	Transform resultTrans;

	int nTarget = 0;
	// Use this for initialization
	void Start () {

		orgPos = Camecamecameha.transform.localPosition;
//		tPos = new Vector3 (0f,5.94f,18.34f);
		tPos = new Vector3 (-1.5f,-0.5f,21.1f);
//		resultTrans = new GameObject().transform ;
//		resultTrans.rotation = new Quaternion (this.gameObject.transform.rotation.x,this.gameObject.transform.rotation.y,this.gameObject.transform.rotation.z,this.gameObject.transform.rotation.w);
		
	}



	// Update is called once per frame
	void Update () {
		
		this.transform.position = Vector3.MoveTowards(this.transform.position,new Vector3(TTaget[nTarget%TTaget.Length].transform.position.x,TTaget[nTarget%TTaget.Length].transform.position.y + 0.8f,TTaget[nTarget%TTaget.Length].transform.position.z),5*Time.deltaTime);
//		this.transform.position = Vector3.MoveTowards(TTaget[nTarget].transform.position,Target.transform.position,1*Time.deltaTime);
		this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation,Illusion.transform.rotation,500*Time.deltaTime);
//
//		if(this.transform.rotation == Illusion.transform.rotation){
//
//			this.transform.position = Vector3.MoveTowards(this.transform.position,new Vector3(TTaget[nTarget%4].transform.position.x,TTaget[nTarget%4].transform.position.y + 0.8f,TTaget[nTarget%4].transform.position.z),5*Time.deltaTime);
//
//		}

		
		if(this.transform.position.Equals(new Vector3(TTaget[3].transform.position.x,TTaget[3].transform.position.y+0.8f,TTaget[3].transform.position.z)) ){
			//print("HI");
			Camecamecameha.transform.localPosition =	Vector3.MoveTowards(Camecamecameha.transform.localPosition,new Vector3(TTaget[3].transform.localPosition.x,TTaget[3].transform.localPosition.y+1.5f,TTaget[3].transform.localPosition.z-2),15*Time.deltaTime);


		}else {

			Camecamecameha.transform.localPosition=	Vector3.MoveTowards(Camecamecameha.transform.localPosition,orgPos,15*Time.deltaTime);
		//	Camecamecameha.transform.position = orgPos;



		}


		if(Input.GetKey(KeyCode.W)){

		//	this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y,this.transform.localPosition.z+5*Time.deltaTime);
		//	this.transform.Rotate( -50 *Time.deltaTime ,0,0);
			

		}

		if(Input.GetKey(KeyCode.S)){

//			this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y,this.transform.localPosition.z-5*Time.deltaTime);
			this.transform.Rotate( +50 *Time.deltaTime ,0,0);

		}

		if(Input.GetKey(KeyCode.A)){

//			this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y + 5*Time.deltaTime,this.transform.localPosition.z);
			this.transform.Rotate(0, -50 * Time.deltaTime ,0);

		}

		if(Input.GetKey(KeyCode.D)){

//			this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y - 5*Time.deltaTime,this.transform.localPosition.z);
			this.transform.Rotate(0, +50 * Time.deltaTime ,0);

		}

		if(Input.GetKeyUp(KeyCode.Space)){

			nTarget++;
			Illusion.transform.position = this.gameObject.transform.position;
			Illusion.transform.rotation = this.gameObject.transform.rotation;
//			this.transform.rotation = new Quaternion (this.gameObject.transform.rotation.x,this.gameObject.transform.rotation.y,this.gameObject.transform.rotation.z,this.gameObject.transform.rotation.w);
			Illusion.transform.LookAt(TTaget[nTarget%TTaget.Length].transform);
			Illusion.transform.Rotate(0, 180 ,0);


		}



	
	}
}
