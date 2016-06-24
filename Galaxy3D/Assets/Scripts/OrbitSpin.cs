using UnityEngine;
using System.Collections;

public class OrbitSpin : MonoBehaviour {

	void Start () {

		for(int i = 0 ; i<4 ; i++){

			Vector3 posX = new Vector3(0,0,0); ;

			if(i==0){

				posX = new Vector3(0,3f,0);

			}else if(i==1){

			 	posX = new Vector3(3f,0,0);

			}else if(i==2){

			 	posX = new Vector3(0,-3f,0);

			}else if(i==3){

			 	posX = new Vector3(-3f,0,0);

			}



			GameObject x = new GameObject("pos"+(i+1));
			x.transform.SetParent(this.gameObject.transform);
			x.transform.localPosition = posX ;

		}

	}

	
	// Update is called once per frame
	void Update () {

		transform.Rotate(0,0,-1.5f);
	}
}
