using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerSetting : MonoBehaviour {

	public enum PlayerTypeSet
    {
        Player,
        AI,
        Robot,
        None
    }

	public GameObject ContentShip,PlayerTypeBtn,NameInput;
	Color Grray = new Color (0.8f,0.8f,0.8f);

	public int indexShip =0;
	public string ShipName = "";

	public PlayerTypeSet PType = PlayerTypeSet.Player;
	public GameObject UpBtn,DownBtn,ContentMask;



	// Use this for initialization
	void Start () {

		PlayerTypeBtn.transform.Find("Text").GetComponent<Text>().text = "Player";

		if(this.gameObject.name.Substring(6) == "1"){
			
			ShipName = "NANA" ;

		}else if(this.gameObject.name.Substring(6) == "2"){

			ShipName = "ATOM" ;

		}else if(this.gameObject.name.Substring(6) == "3"){

			ShipName = "BIGBLUE" ;

		}else if(this.gameObject.name.Substring(6) == "4"){

			ShipName = "OZ" ;

		}

		NameInput.GetComponent<InputField>().text = ShipName;
	}

	// Update is called once per frame
	void Update () {


//		print(ContentShip.transform.localPosition);

		ContentShip.transform.localPosition = Vector3.MoveTowards(ContentShip.transform.localPosition,new Vector3(0,59.3f+(118*indexShip)+(0*indexShip),0), 400*Time.deltaTime);
	
	}

	public void  TypeBtnPressed(){

		
		if(PType == PlayerTypeSet.Player){

			ShipName = "AI"+Random.Range(1,100);
			NameInput.GetComponent<InputField>().text = ShipName ;
			NameInput.GetComponent<InputField>().enabled = false;
			NameInput.GetComponent<Image>().color = Grray ;
			
			PlayerTypeBtn.transform.Find("Text").GetComponent<Text>().text = "AI";
			PType = PlayerTypeSet.AI ;


		}else if(PType == PlayerTypeSet.AI){

			ShipName = "RobotName";
			NameInput.GetComponent<InputField>().text = ShipName ;
			NameInput.GetComponent<InputField>().enabled = false;
			NameInput.GetComponent<Image>().color = Grray ;

			PlayerTypeBtn.transform.Find("Text").GetComponent<Text>().text = "Robot";
			PType = PlayerTypeSet.Robot ;


		}else if(PType == PlayerTypeSet.Robot){

			ShipName = "";
			NameInput.GetComponent<InputField>().text = " ";
			NameInput.GetComponent<InputField>().enabled = false;
			NameInput.GetComponent<Image>().color = Grray ;

			PlayerTypeBtn.transform.Find("Text").GetComponent<Text>().text = "None";

			UpBtn.SetActive(false);
			DownBtn.SetActive(false);
			ContentMask.SetActive(false);

			PType = PlayerTypeSet.None ;

		}else if(PType == PlayerTypeSet.None){
			ShipName = "PlayerName";
			NameInput.GetComponent<InputField>().text = "PlayerName";
			NameInput.GetComponent<InputField>().enabled = true;
			NameInput.GetComponent<Image>().color = Color.white ;
			PlayerTypeBtn.transform.Find("Text").GetComponent<Text>().text = "Player";

			UpBtn.SetActive(true);
			DownBtn.SetActive(true);
			ContentMask.SetActive(true);


			PType = PlayerTypeSet.Player;

		}

//		PlayerTypeBtn.transform.Find("Text").GetComponent<Text>().text = "Player";


	}


	public void UpBtnPress(){
//		print(indexShip);
		if(indexShip > 0){
//			print(indexShip);
			indexShip -=1;

		}
		


	}

	public void DownBtnPress(){
		if(indexShip <6){
			indexShip +=1;
		}

	}


//	public string GetInfoPlayer(int x){
//
//	return "";
//
//	}


	public void AfterEndEdit(){

		ShipName = NameInput.GetComponent<InputField>().text;

	}

}
