using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public GameObject LongContent,ShopPanel,Slot1,Slot2,Slot3,Slot4;
	int stage = 0;
	int playerCount = 4 ;



	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {

//		print(LongContent.transform.localPosition);
		if(stage == 0){

			LongContent.transform.localPosition = Vector3.MoveTowards(LongContent.transform.localPosition, new Vector3(400,0,0), 900 * Time.deltaTime);


		}else if (stage == 1){

			LongContent.transform.localPosition = Vector3.MoveTowards(LongContent.transform.localPosition, new Vector3(-400,0,0), 900 * Time.deltaTime);

		}
	
	}


	public void GotoGame(){



		if(Slot1.GetComponent<PlayerSetting>().PType == PlayerSetting.PlayerTypeSet.None){


			playerCount --;


		}

		if(Slot2.GetComponent<PlayerSetting>().PType == PlayerSetting.PlayerTypeSet.None){


			playerCount --;


		}

		if(Slot3.GetComponent<PlayerSetting>().PType == PlayerSetting.PlayerTypeSet.None){


			playerCount --;


		}

		if(Slot4.GetComponent<PlayerSetting>().PType == PlayerSetting.PlayerTypeSet.None){


			playerCount --;


		}



		print("____________________________________________________________________________________");

		print("Type "+Slot1.GetComponent<PlayerSetting>().PType.ToString() + "   |  Use Ship : "+ Slot1.GetComponent<PlayerSetting>().indexShip + "   |  Ship Name : "+ Slot1.GetComponent<PlayerSetting>().ShipName);

		print("Type "+Slot2.GetComponent<PlayerSetting>().PType.ToString() + "   |  Use Ship : "+ Slot2.GetComponent<PlayerSetting>().indexShip + "   |  Ship Name : "+ Slot2.GetComponent<PlayerSetting>().ShipName);
	
		print("Type "+Slot3.GetComponent<PlayerSetting>().PType.ToString() + "   |  Use Ship : "+ Slot3.GetComponent<PlayerSetting>().indexShip + "   |  Ship Name : "+ Slot3.GetComponent<PlayerSetting>().ShipName);

		print("Type "+Slot4.GetComponent<PlayerSetting>().PType.ToString() + "   |  Use Ship : "+ Slot4.GetComponent<PlayerSetting>().indexShip + "   |  Ship Name : "+ Slot4.GetComponent<PlayerSetting>().ShipName);


		if(playerCount >=2){

			PlayerPrefs.SetInt("PlayerCount",playerCount);

			PlayerPrefs.SetString("P1Name",Slot1.GetComponent<PlayerSetting>().ShipName);
			PlayerPrefs.SetString("P1Type",Slot1.GetComponent<PlayerSetting>().PType.ToString());
			PlayerPrefs.SetInt("P1Index",Slot1.GetComponent<PlayerSetting>().indexShip);

			PlayerPrefs.SetString("P2Name",Slot2.GetComponent<PlayerSetting>().ShipName);
			PlayerPrefs.SetString("P2Type",Slot2.GetComponent<PlayerSetting>().PType.ToString());
			PlayerPrefs.SetInt("P2Index",Slot2.GetComponent<PlayerSetting>().indexShip);

			PlayerPrefs.SetString("P3Name",Slot3.GetComponent<PlayerSetting>().ShipName);
			PlayerPrefs.SetString("P3Type",Slot3.GetComponent<PlayerSetting>().PType.ToString());
			PlayerPrefs.SetInt("P3Index",Slot3.GetComponent<PlayerSetting>().indexShip);

			PlayerPrefs.SetString("P4Name",Slot4.GetComponent<PlayerSetting>().ShipName);
			PlayerPrefs.SetString("P4Type",Slot4.GetComponent<PlayerSetting>().PType.ToString());
			PlayerPrefs.SetInt("P4Index",Slot4.GetComponent<PlayerSetting>().indexShip);


			Application.LoadLevel("GalaxyJourney");
		}else{
			playerCount = 4;
			print("Please play at least 2 Player");

		}
		

	}

	public void To2ndPage(){

		 stage =1;

	}

	public void To1stPage(){

		stage = 0;

	}


	public void ShopClick(){


		ShopPanel.SetActive(!ShopPanel.activeSelf);

	}
}
