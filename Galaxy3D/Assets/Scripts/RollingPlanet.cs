using UnityEngine;
using System.Collections;

public class RollingPlanet : MonoBehaviour {

	bool goingUp = false;
	float floatingSpeed;
	Vector3 orgPos;
	float amp = 0.3f;

	// Use this for initialization
	void Start () {
//		return;
		orgPos = this.gameObject.transform.position;
		floatingSpeed = Random.Range(1,100)/300f;

	}
	
	// Update is called once per frame
	void Update () {

//		return;
		if(goingUp){
			transform.Rotate(0,0,-0.1f);
			if(this.transform.position.y> orgPos.y+amp){
				goingUp = false;
			}else{

				this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y+floatingSpeed*Time.deltaTime,this.gameObject.transform.position.z);

			}


		}else{

			transform.Rotate(0,0,0.1f);
			if(this.transform.position.y< orgPos.y-amp){
				goingUp = true;

			}else{

				this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y-floatingSpeed*Time.deltaTime,this.gameObject.transform.position.z);
			}


		}




	}
}
