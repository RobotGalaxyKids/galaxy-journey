using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarfallManager : MonoBehaviour {


	public enum GameState
	{
		Start,
		Roll,
		MoveGame,
		Effect,
		EndGame
	}	

	public GameObject Player1Obj,Player2Obj,DiceButton,QPanel,Dice1,Dice2,Choice1,Choice2,Choice3,FXPlayer,TurnDisplay;
	public AudioClip diceSound, qPop,Booing,winSound;
	Dictionary<string,string[]> PositonInfoDict ;
	GameState gameState;
	GameObject ObjUsing,targetPoint;

	int playerPos,robotPos,usingPos;
	int dicePoint = 0;
	int countDice;
	int nextStep;
	float speed = 3;
	float timecd = 5f;
	float timeInRandom;
	bool doItOnce = true;
	bool xFin = false;
	bool yFin = false;
	bool finishEffext = false;
	string turnControl = "User";
	int tempD1;
	int tempD2;
	bool colorUp=true;


	// Use this for initialization
	void Start () {

		/////
		TextAsset jString = (TextAsset) Resources.Load("SampleCard");
		string jsonString = jString.text;
		PositonInfoDict = JsonMapper.ToObject<Dictionary<string, string[]>>(jsonString);
		/////

		Dice1.GetComponent<Text>().text = "0";
		Dice2.GetComponent<Text>().text = "0";


		//Player1Obj.GetComponents<PlayerStatus>()
		playerPos = 1;
		robotPos = 1;

		Player1Obj.transform.position = new Vector3(-7.67f,-1.22f);
		Player2Obj.transform.position = new Vector3(-7.87f,-1.22f);

		gameState = GameState.Start;
		DiceButton.SetActive(true);
		QPanel.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {

		

		switch(gameState) {

			case GameState.Start : 
				// Check Whos turn
				if(doItOnce){
					DiceButton.SetActive(true);
					if(turnControl.Equals("User")){
						ObjUsing = Player1Obj;
						usingPos = playerPos;
//						print("UserTurn");
						TurnDisplay.GetComponent<Text>().text = "Player1's Turn";
					}else if (turnControl.Equals("Robot")){
						usingPos = robotPos;
						ObjUsing = Player2Obj;
//						print("RobotTurn");
						TurnDisplay.GetComponent<Text>().text = "Player2's Turn";
					}

					doItOnce = false;

				}


			break;

			case GameState.Roll :

				//Roll Dice;
				if(doItOnce){
					if((usingPos+dicePoint) > PositonInfoDict.Count){
						 countDice  = PositonInfoDict.Count - usingPos ;

					 }
				 }

				if(timeInRandom>0){
					Dice1.GetComponent<Text>().text = ""+Random.Range(1,7);
					Dice2.GetComponent<Text>().text = ""+Random.Range(1,7);
				 	timeInRandom-=Time.deltaTime;
					


				 }else if(timeInRandom<=0){
					Dice1.GetComponent<Text>().text = ""+tempD1;
					Dice2.GetComponent<Text>().text = ""+tempD2;
					doItOnce = true;
					gameState = GameState.MoveGame;
				}

			break;

			case GameState.MoveGame :
				
				// Make Current Player to End Point.
				
				//print("Come");
				if(colorUp){
					if(targetPoint.GetComponent<SpriteRenderer>().color.g < 1){
						targetPoint.GetComponent<SpriteRenderer>().color = new Color(targetPoint.GetComponent<SpriteRenderer>().color.r,
						targetPoint.GetComponent<SpriteRenderer>().color.g + Time.deltaTime ,
						targetPoint.GetComponent<SpriteRenderer>().color.b);
					}else{

						colorUp = false;

					}

				}else {
					//print("<255");
					if(targetPoint.GetComponent<SpriteRenderer>().color.g > 0.5f){
						
						targetPoint.GetComponent<SpriteRenderer>().color = new Color(targetPoint.GetComponent<SpriteRenderer>().color.r,
						targetPoint.GetComponent<SpriteRenderer>().color.g - Time.deltaTime ,
						targetPoint.GetComponent<SpriteRenderer>().color.b);
					}else{
						colorUp = true;

					}

				}

				if(doItOnce){


					Player1Obj.GetComponent<CircleCollider2D>().isTrigger = true;
					Player2Obj.GetComponent<CircleCollider2D>().isTrigger = true;

				}

				if(countDice == 1){

					Player1Obj.GetComponent<CircleCollider2D>().isTrigger = false;
					Player2Obj.GetComponent<CircleCollider2D>().isTrigger = false;

				}




				// Move Current Player to next Position

	
				////////	//////X
				if(true){
					nextStep = usingPos+1;
				}else{

					nextStep = int.Parse( PositonInfoDict[""+(usingPos)][5]);

				}

				if(ObjUsing.transform.position.x< float.Parse(PositonInfoDict[""+(nextStep)][0])){

					if(ObjUsing.transform.position.x+speed*Time.deltaTime<float.Parse(PositonInfoDict[""+(nextStep)][0])){

						ObjUsing.transform.position = new Vector3(ObjUsing.transform.position.x+speed*Time.deltaTime,ObjUsing.transform.position.y);
					}else{

						ObjUsing.transform.position = new Vector3(float.Parse(PositonInfoDict[""+(nextStep)][0]),ObjUsing.transform.position.y);	
						xFin = true;
					}
						
				}


				if(ObjUsing.transform.position.x> float.Parse(PositonInfoDict[""+(nextStep)][0])){

					if(ObjUsing.transform.position.x-speed*Time.deltaTime>float.Parse(PositonInfoDict[""+(nextStep)][0])){

						ObjUsing.transform.position = new Vector3(ObjUsing.transform.position.x-speed*Time.deltaTime,ObjUsing.transform.position.y);
					}else{

						ObjUsing.transform.position = new Vector3(float.Parse(PositonInfoDict[""+(nextStep)][0]),ObjUsing.transform.position.y);	
						xFin = true;
					}
					
				}

					/////////	//////Y
				if(ObjUsing.transform.position.y< float.Parse(PositonInfoDict[""+(nextStep)][1])){

					if(ObjUsing.transform.position.y+speed*Time.deltaTime<float.Parse(PositonInfoDict[""+(nextStep)][1])){

						ObjUsing.transform.position = new Vector3(ObjUsing.transform.position.x,ObjUsing.transform.position.y+speed*Time.deltaTime);
					}else{

						ObjUsing.transform.position = new Vector3(ObjUsing.transform.position.x,float.Parse(PositonInfoDict[""+(nextStep)][1]));	
						yFin = true;
					}
					
				}


				if(ObjUsing.transform.position.y> float.Parse(PositonInfoDict[""+(nextStep)][1])){

					if(ObjUsing.transform.position.y-speed*Time.deltaTime>float.Parse(PositonInfoDict[""+(nextStep)][1])){

						ObjUsing.transform.position = new Vector3(ObjUsing.transform.position.x,ObjUsing.transform.position.y-speed*Time.deltaTime);
					}else{

						ObjUsing.transform.position = new Vector3(ObjUsing.transform.position.x,float.Parse(PositonInfoDict[""+(nextStep)][1]));	
						yFin = true;
					}
					
				}



				if(xFin&&yFin&&countDice>0){
					countDice--;
					if(countDice !=0){
						xFin = false;
						yFin = false;
					}
					usingPos ++;


				}

			



				if(xFin&&yFin&&countDice<=0){
					xFin = false;
					yFin = false;

					//playerPos += dicePoint;
					if(turnControl == "User"){
						
						playerPos = usingPos;
//						print("Player Pos is now " + playerPos);
					}
					else{
						robotPos = usingPos;
//						print("Robot Pos is now " + robotPos);
					}
					doItOnce = true;


					targetPoint.GetComponent<SpriteRenderer>().color = Color.white;
					gameState = GameState.Effect;


				}

			



			break;

			case GameState.Effect :
				if(doItOnce){
					if(usingPos==PositonInfoDict.Count){
						GameObject.Find("DiceShow").GetComponent<Text>().text = turnControl + " Won";
//						print("==================== Winner is : "+turnControl + " ========================");
						FXPlayer.GetComponent<AudioSource>().clip = winSound;
						FXPlayer.GetComponent<AudioSource>().Play();
						gameState = GameState.EndGame;
						return;

					}else{

						if(PositonInfoDict[""+usingPos][4].Equals("Yes")){
							//SPECAIL EFFECT
							FXPlayer.GetComponent<AudioSource>().clip = qPop;
							FXPlayer.GetComponent<AudioSource>().Play();
							QPanel.SetActive(true);
							QPanel.transform.Find("Text").GetComponent<Text>().text = PositonInfoDict[""+usingPos][5];
							Choice1.GetComponent<Text>().text = PositonInfoDict[""+usingPos][6];
							Choice2.GetComponent<Text>().text = PositonInfoDict[""+usingPos][7];
							Choice3.GetComponent<Text>().text = PositonInfoDict[""+usingPos][8];
							
					}else if(PositonInfoDict[""+usingPos][4].Equals("Ladder") || PositonInfoDict[""+usingPos][4].Equals("Snake") ){
							//LADDER & SNAKE EFFECT
							FXPlayer.GetComponent<AudioSource>().clip = Booing;
							FXPlayer.GetComponent<AudioSource>().Play();
							countDice = 1;
							usingPos = int.Parse( PositonInfoDict[""+usingPos][5]) - 1;
							targetPoint = GameObject.Find("pos"+(usingPos+countDice));

							gameState = GameState.MoveGame;

						}else{
							//No FX
							finishEffext = true;

						}



					}
					doItOnce = false;
				}




				if(finishEffext){
					finishEffext = false;
					doItOnce = true;
					if(turnControl == "User"){
						turnControl = "Robot";
					}
					else{

						turnControl = "User";

					}
					gameState = GameState.Start;
				}
				//Take Effect

			break;


			case GameState.EndGame :

				timecd -= Time.deltaTime;
				if(timecd <= 0) SceneManager.LoadScene("StarFall");
				

			break;


			default : 

			break;

		}

	}


	public void RollSi (){


		FXPlayer.GetComponent<AudioSource>().clip = diceSound;
		FXPlayer.GetComponent<AudioSource>().Play();
 		tempD1 = Random.Range(1,7);
		tempD2 = Random.Range(1,7);
		Dice1.GetComponent<Text>().text = ""+tempD1;
		Dice2.GetComponent<Text>().text = ""+tempD2;
		timeInRandom = 2f;
		dicePoint = tempD1 + tempD2;
//		dicePoint = 4;

		if(usingPos+dicePoint>PositonInfoDict.Count){
			targetPoint = GameObject.Find("pos"+PositonInfoDict.Count);
		}else{
			targetPoint = GameObject.Find("pos"+(usingPos+dicePoint));
		}

		GameObject.Find("DiceShow").GetComponent<Text>().text = "Dice Point! : "+dicePoint;

		countDice = dicePoint;
		doItOnce = true;
		gameState = GameState.Roll;
		DiceButton.SetActive(false);

	}


	public void Roll4 (){


		FXPlayer.GetComponent<AudioSource>().clip = diceSound;
		FXPlayer.GetComponent<AudioSource>().Play();
 		tempD1 = 2;
		tempD2 = 2;
		Dice1.GetComponent<Text>().text = "2";
		Dice2.GetComponent<Text>().text = "2";
		timeInRandom = 2f;
//		dicePoint = tempD1 + tempD2;
		dicePoint = 4;

		if(usingPos+dicePoint>PositonInfoDict.Count){
			targetPoint = GameObject.Find("pos"+PositonInfoDict.Count);
		}else{
			targetPoint = GameObject.Find("pos"+(usingPos+dicePoint));
		}

		GameObject.Find("DiceShow").GetComponent<Text>().text = "Dice Point! : "+dicePoint;

		countDice = dicePoint;
		doItOnce = true;
		gameState = GameState.Roll;
		DiceButton.SetActive(false);

	}

	public void Click1(){

		finishEffext = true;
		QPanel.SetActive(false);

	}

	public void Click2(){

		finishEffext = true;
		QPanel.SetActive(false);

	}

	public void Click3(){

		finishEffext = true;
		QPanel.SetActive(false);

	}
}
