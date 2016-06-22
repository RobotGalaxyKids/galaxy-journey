using UnityEngine;
using System.Collections;

public class ShowLine : MonoBehaviour {



	void OnMouseDown(){

		this.gameObject.GetComponent<LineRenderer>().enabled = true;

	}

	void OnMouseUp(){


		this.gameObject.GetComponent<LineRenderer>().enabled = false;

	}
}
