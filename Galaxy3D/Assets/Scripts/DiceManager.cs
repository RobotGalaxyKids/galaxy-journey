using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class DiceManager : MonoBehaviour  {


	public GameObject Dice;
	public Camera mainCamera;
	public Text textDice;
	public Image rollBtn;

	private Vector3 initPos;
	private float initXpose;

	public bool pressed = false;
	bool rolling = false;

	void Start()
	{
		Dice.GetComponent<Rigidbody> ().isKinematic = true;

	} 
	public void ClickDown()
	{
		pressed = true;


	}

	public void ClickUp()
	{
		Dice.GetComponent<Rigidbody> ().isKinematic = false;
		RollDice ();
	}
	void Update()
	{

		if (rolling && Dice.GetComponent<Rigidbody> ().velocity.magnitude < 0.001f) {

			rolling = false;
//			textDice.text =  DiceScript.Instance.GetDiceCount ().ToString();
//			StartCoroutine(deleyDiceCount(1));

		}

	}
		
	void RollDice()
	{
		rolling = true;

		//rollBtn.enabled = false; 

		Vector3 force = Vector3.forward * Random.Range (4000.0f, 6000.0f)  + Vector3.right * Random.Range(-500.0f, 500.0f);
		Dice.GetComponent<Rigidbody>().AddForce (force);

		Vector3 v = Vector3.forward;
		v = Random.rotation * v;
		Dice.GetComponent<Rigidbody>().AddTorque (v * 25.0f);

	}



}
