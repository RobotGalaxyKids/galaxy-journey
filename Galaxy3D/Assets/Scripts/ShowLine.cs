using UnityEngine;
using System.Collections;

public class ShowLine : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){

		this.gameObject.GetComponent<LineRenderer>().enabled = true;

	}

	void OnMouseUp(){


		this.gameObject.GetComponent<LineRenderer>().enabled = false;

	}
}
