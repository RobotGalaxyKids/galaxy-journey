using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using LitJson;

public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        Start,
        Waiting,
        Roll,
        MoveGame,
        Effect,
        Swapping,
        EndGame
    }


    public GameObject 	CamecamecamehaPilot,
        				ScreenCTR,
				        Illusion,
				        TextStageObj,
				        QPanel,
				        Question,
				        Ans1,
				        Ans2,
				        Ans3;

    public GameObject EffectPanel, OkEffectBtn, TextEffect,FXControl;

    public GameObject MinimapContent, numberPrefab;

    public GameObject RollBtn;
    public GameObject[] TTaget, miniNumObj;
    public Sprite[] ShipModel;


    public GameObject PrefabShip;



    public AudioClip ShipMove,QuesionPop,EndGame;
    // Planet Collection, Going to delet it soon :3
    GameObject usingSpaceship;
    Dictionary<string,string[]> PositonInfoDict;
    Vector3 orgMini, tPos,nextStarLocate;
    // tpos = OverCamera Position, org = original Camera Position
    Vector3 tempPos1, tempPos2;
    GameState gameState = GameState.Start;






   
    int turnIndex = 0;
    int nTarget = 0;
    // now targeting at x position Planet
    int usingPos = 0, playerPos = 0, enemyPos = 0;
    int dicePoint = 0;
    int countDice = 0;
    int playerCount = 0;

    int[] shipPosition;
    string[] shipName;
    string[] shipPlayerType;
    GameObject[] shipInUse;

    // leftStep


    float shipSpeed = 20f;
    float minCameraDistant = 0;
    float distBTWShip = 1.5f;
    float cameraSpeed = 25f;
    float waitTime = 1f;

   
    bool finishEffect = false;
    bool goingBack = false;
    bool stopCam = false;
    bool doItOnce = true;
	bool doItMove = true;
    bool swapBool = false;

    bool tutorMode = false;
    bool effect2Move = false;
    bool atSpot = false;


    float numberBoxWidth = 1f;



   	Color markColor = new Color32(255,201,113,255);
	Color oldColor = new Color32(43,202,255,255);

    // Use this for initialization
    void Start()
    {

//    	print("Space1pos " + SpaceShip1.transform.position);
    	
		playerCount = PlayerPrefs.GetInt("PlayerCount");


		shipPosition = new int[playerCount] ;
		shipName = new string[playerCount] ;
		shipPlayerType = new string[playerCount];
		shipInUse = new GameObject[playerCount];

		int shipIndexFix = 0;

		for(int i =0 ; i< 4; i++ ){

			Color cShip = Color.white;
			Vector3 pShip = new Vector3(0,0,0);



			if(i == 0){
				pShip = new Vector3(0,2,0);
				cShip = Color.white;

			}else if(i == 1){
				pShip = new Vector3(2,0,0);
				cShip = Color.white;

			}else if(i == 2){
				pShip = new Vector3(0,-2,0);
				cShip = Color.white;

			}else if(i == 3){
				pShip = new Vector3(-2,0,0);
				cShip = Color.white;

			}

			if(PlayerPrefs.GetString(("P"+(i+1)+"Type")) != "None"){
				
				shipPosition[shipIndexFix] = 0;
				shipName[shipIndexFix] = PlayerPrefs.GetString(("P"+(i+1)+"Name"));
				shipPlayerType[shipIndexFix] = PlayerPrefs.GetString(("P"+(i+1)+"Type"));
				shipInUse[shipIndexFix] = Instantiate(PrefabShip) as GameObject;
				shipInUse[shipIndexFix].transform.Find("spaceShip").gameObject.GetComponent<SpriteRenderer>().sprite = ShipModel[i];
				shipInUse[shipIndexFix].transform.SetParent(TTaget[0].transform.Find("Orbit").transform);
//				shipInUse[shipIndexFix].transform.rotation = Quaternion.Euler(0,0,360-(90*i));
				shipInUse[shipIndexFix].transform.localPosition = pShip;
				shipInUse[shipIndexFix].GetComponent<PlayerStatus>().effct = PlayerStatus.StatusEffect.None;
				shipInUse[shipIndexFix].transform.Find("spaceShip").gameObject.GetComponent<SpriteRenderer>().color = cShip;
				shipInUse[shipIndexFix].transform.localRotation = Quaternion.Euler(0,0,360-(90*i));
				shipIndexFix ++;

			}





		}

        QPanel.SetActive(false);
        EffectPanel.SetActive(false);

        TextAsset jString = (TextAsset)Resources.Load("JSON/PositionProperty");
        string jsonString = jString.text;
        PositonInfoDict = JsonMapper.ToObject<Dictionary<string, string[]>>(jsonString);
        /////

        TextStageObj.GetComponent<Text>().text = gameState.ToString();

        //set Camera Position

        tPos = new Vector3(-37, -34, -38);
//        usingSpaceship = SpaceShip1;
		usingSpaceship = shipInUse[turnIndex];

        miniNumObj = new GameObject[PositonInfoDict.Count];
        numberBoxWidth = MinimapContent.GetComponent<RectTransform>().sizeDelta.x;
        MinimapContent.GetComponent<RectTransform>().sizeDelta = new Vector2(MinimapContent.GetComponent<RectTransform>().sizeDelta.x * (PositonInfoDict.Count), MinimapContent.GetComponent<RectTransform>().sizeDelta.y);

        for (int i = 0; i < PositonInfoDict.Count; i++)
        {

            GameObject x = Instantiate(numberPrefab);
            x.transform.SetParent(MinimapContent.transform);
            x.transform.localScale = new Vector3(1f, 1f, 1f);
            x.name = "" + i;
            x.transform.Find("Text").GetComponent<Text>().text = "" + (i + 1);
//			if( i == 0){
            x.transform.Find("Text").transform.localScale = new Vector3(1f, 1f, 1f);

            if(i==0){

            	x.transform.Find("Text").GetComponent<Text>().fontSize = 26;

            }else if (i==1){

				x.transform.Find("Text").GetComponent<Text>().fontSize = 20;

			}else{

				x.transform.Find("Text").GetComponent<Text>().fontSize = 14;

			}

            miniNumObj[i] = x;
			

        }
        


			
//			Vector3 diff = usingSpaceship.transform.position - TTaget[2].transform.position ;
//			diff.Normalize();
//			float rot_z = Mathf.Atan2(diff.y,diff.x) * Mathf.Rad2Deg;
//			usingSpaceship.transform.rotation = Quaternion.Euler(0f,0f,rot_z - 90);
//			print("FF "+ (rot_z -90) + "   " + TTaget[2].name);
        orgMini = new Vector3(MinimapContent.GetComponent<RectTransform>().transform.localPosition.x + 2 * numberBoxWidth, MinimapContent.GetComponent<RectTransform>().transform.localPosition.y, MinimapContent.GetComponent<RectTransform>().transform.localPosition.z);
        MinimapContent.GetComponent<RectTransform>().transform.localPosition = orgMini;
    }


    //update for Stage
    void FixedUpdate()
    {


//		print(MinimapContent.GetComponent<RectTransform>().transform.localPosition);
//		MinimapContent.GetComponent<RectTransform>().transform.position = new Vector3(MinimapContent.GetComponent<RectTransform>().transform.position.x - 100*Time.deltaTime,MinimapContent.GetComponent<RectTransform>().transform.position.y,MinimapContent.GetComponent<RectTransform>().transform.position.z);
		
        switch (gameState)
        {

            case GameState.Start: 

                MinimapContent.GetComponent<RectTransform>().transform.localPosition = Vector3.MoveTowards(MinimapContent.GetComponent<RectTransform>().transform.localPosition, new Vector3(orgMini.x - (usingPos * numberBoxWidth), orgMini.y, orgMini.z), 1000 * Time.deltaTime);
				// Check Whos turn
                if (doItOnce)
                {


                    CamecamecamehaPilot.GetComponent<OverAllCameraLook>().finishFirstMove = false;
                    stopCam = false;
                    ScreenCTR.GetComponent<ScreenScroller>().canSwipe = false;
                    CamecamecamehaPilot.GetComponent<OverAllCameraLook>().KeepDistance2ndStage = false;
//					usingSpaceship.transform.rotation = Quaternion.Euler(0,0,0);
//					usingSpaceship.transform.rotation = Quaternion.Euler(0,0,0);
                    if (true)
                    {

//                        SpaceShip1.GetComponent<TrailRenderer>().enabled = true;
//                        SpaceShip2.GetComponent<TrailRenderer>().enabled = false;
//						openShipTrail(turnIndex);

                        TextStageObj.GetComponent<Text>().text = shipName[turnIndex] +"'S TURN";


//                        usingSpaceship = SpaceShip1;
						usingSpaceship = shipInUse[turnIndex];


//                        usingPos = playerPos;
						usingPos = shipPosition[turnIndex];



                        //		TurnDisplay.GetComponent<Text>().text = "Player1's Turn";
//                        CamecamecamehaPilot.GetComponent<OverAllCameraLook>().spaceShip = SpaceShip1;
						CamecamecamehaPilot.GetComponent<OverAllCameraLook>().spaceShip = usingSpaceship;
					
                    }
                    else if (false)
                    {
//                        CamecamecamehaPilot.GetComponent<OverAllCameraLook>().spaceShip = SpaceShip2;
//                        SpaceShip1.GetComponent<TrailRenderer>().enabled = false;
//                        SpaceShip2.GetComponent<TrailRenderer>().enabled = true;
                        TextStageObj.GetComponent<Text>().text = "ROBOT'S TURN";
                        usingPos = enemyPos;
//                        usingSpaceship = SpaceShip2;
                        //TurnDisplay.GetComponent<Text>().text = "Player2's Turn";

//                        if (modeAI)
//                        {
//                            RollBtn.SetActive(false);
//
//
//                        }
                    }

//					usingSpaceship.transform.SetParent(null);
//                    SpaceShip1.transform.rotation = Quaternion.Euler(0, 0, 0);
//                    SpaceShip2.transform.rotation = Quaternion.Euler(0, 0, 0);


                    for(int i = 0 ; i < playerCount ; i++){

						shipInUse[i].transform.localRotation = Quaternion.Euler(0,0,360-(90*i));

                    }


//					MinimapContent.GetComponent<RectTransform>().transform.localPosition = new Vector3(orgMini.x - (usingPos*numberBoxWidth),MinimapContent.GetComponent<RectTransform>().transform.localPosition.y,MinimapContent.GetComponent<RectTransform>().transform.localPosition.z);
					minCameraDistant = (usingSpaceship.transform.position.x < -25.4f) ? -25.4f : usingSpaceship.transform.position.x;
//					cameraSpeed = Mathf.Abs(CamecamecamehaPilot.transform.position.x - minCameraDistant);
                    cameraSpeed = 25f;



                    for(int i =0; i<miniNumObj.Length ;i++){

						miniNumObj[i].transform.Find("Text").GetComponent<Text>().fontSize = 14;
						miniNumObj[i].transform.Find("Text").GetComponent<Text>().color = oldColor;


                    }

					miniNumObj[usingPos].transform.Find("Text").GetComponent<Text>().fontSize = 26;
					miniNumObj[usingPos].transform.Find("Text").GetComponent<Text>().color = markColor;


					if(usingPos > 1 && usingPos< 28){
						miniNumObj[usingPos-1].transform.Find("Text").GetComponent<Text>().fontSize = 20;
						miniNumObj[usingPos-2].transform.Find("Text").GetComponent<Text>().fontSize = 14;
						miniNumObj[usingPos+1].transform.Find("Text").GetComponent<Text>().fontSize = 20;
						miniNumObj[usingPos+2].transform.Find("Text").GetComponent<Text>().fontSize = 14;
					}else if(usingPos == 1){

						miniNumObj[usingPos-1].transform.Find("Text").GetComponent<Text>().fontSize = 20;
						miniNumObj[usingPos+1].transform.Find("Text").GetComponent<Text>().fontSize = 20;
						miniNumObj[usingPos+2].transform.Find("Text").GetComponent<Text>().fontSize = 14;

					}else if(usingPos == 0 ){

						miniNumObj[usingPos+1].transform.Find("Text").GetComponent<Text>().fontSize = 20;
						miniNumObj[usingPos+2].transform.Find("Text").GetComponent<Text>().fontSize = 14;

					}else if (usingPos == 28){

						miniNumObj[usingPos-1].transform.Find("Text").GetComponent<Text>().fontSize = 20;
						miniNumObj[usingPos-2].transform.Find("Text").GetComponent<Text>().fontSize = 14;
						miniNumObj[usingPos+1].transform.Find("Text").GetComponent<Text>().fontSize = 20;

					}else if (nTarget == 29){

						miniNumObj[usingPos-1].transform.Find("Text").GetComponent<Text>().fontSize = 20;
						miniNumObj[usingPos-2].transform.Find("Text").GetComponent<Text>().fontSize = 14;

					}

                    doItOnce = false;

                }


                if (!stopCam)
                {
					minCameraDistant = (usingSpaceship.transform.position.x < -25.4f) ? -25.4f : usingSpaceship.transform.position.x;
                    CamecamecamehaPilot.transform.position = Vector3.MoveTowards(CamecamecamehaPilot.transform.position, new Vector3(minCameraDistant, usingSpaceship.transform.position.y, tPos.z), cameraSpeed * Time.deltaTime);
                    cameraSpeed += 7 * Time.deltaTime;




//					print(cameraSpeed+"");

                }
                else
                {

//					ScreenCTR.GetComponent<ScreenScroller>().canSwipe = true;
                   

                }


                if (CamecamecamehaPilot.transform.position == new Vector3(minCameraDistant, usingSpaceship.transform.position.y, tPos.z))
                {
               			
			
                    stopCam = true;
                    doItOnce = true;
                    gameState = GameState.Roll;




                }


                break;

            case GameState.Roll:

                if (doItOnce)
                {

                    EffectPanel.SetActive(true);


                    if (usingSpaceship.GetComponent<PlayerStatus>().effct == PlayerStatus.StatusEffect.None)
                    {

						
//                        TextEffect.GetComponent<Text>().text = turnControl.ToUpper() + " TURN!";
						TextEffect.GetComponent<Text>().text = shipName[turnIndex] + " TURN!";
							

                    }
                    else if (usingSpaceship.GetComponent<PlayerStatus>().effct == PlayerStatus.StatusEffect.DoubleDice)
                    {

//                        TextEffect.GetComponent<Text>().text = turnControl.ToUpper() + " IS ON DOUBLE DICE EFFECT!";
						TextEffect.GetComponent<Text>().text = shipName[turnIndex].ToUpper() + " IS ON DOUBLE DICE EFFECT!";


                    }
                    else if (usingSpaceship.GetComponent<PlayerStatus>().effct == PlayerStatus.StatusEffect.BackBack)
                    {

//                        TextEffect.GetComponent<Text>().text = turnControl.ToUpper() + " IS CONFUSING! AND WILL GOING BACKWARD IS TURN";
						TextEffect.GetComponent<Text>().text = shipName[turnIndex].ToUpper() + " IS CONFUSING! AND WILL GOING BACKWARD IS TURN";
                        usingSpaceship.transform.localRotation = Quaternion.Euler(0, 0, 180);


                    }
                    else if (usingSpaceship.GetComponent<PlayerStatus>().effct == PlayerStatus.StatusEffect.Trap)
                    {

//                        TextEffect.GetComponent<Text>().text = turnControl.ToUpper() + " IS TRAPED! AND CAN NOT ROLL THIS TURN";
						TextEffect.GetComponent<Text>().text = shipName[turnIndex].ToUpper() + " IS TRAPED! AND CAN NOT ROLL THIS TURN";


                    }

                    doItOnce = false;

                }


                
					
//					doItOnce = true;
//					gameState = GameState.MoveGame;
//				}

                break;

            case GameState.MoveGame:



				
		
				// Set Camera While Moving
                CamecamecamehaPilot.GetComponent<OverAllCameraLook>().KeepDistance2ndStage = true;

                if (!CamecamecamehaPilot.GetComponent<OverAllCameraLook>().finishFirstMove){
                    return;
                }

                    
                if (!goingBack)
                {
                    nTarget = (usingPos + 1);
                }
                else
                {

                    if (usingPos > 0)
                    {
                        nTarget = (usingPos - 1);
                    }
                }


                if (nTarget >= TTaget.Length)
                {

                    goingBack = true;

                }
			

				

////				print(TTaget[usingPos].transform.Find("Orbit").gameObject.transform.rotation.eulerAngles.z);
////
//				if(TTaget [usingPos].transform.Find("Orbit").gameObject.transform.rotation.eulerAngles.z <= 45 + (90*turnIndex)){
//
//
////					print(TTaget[usingPos].transform.Find("Orbit").gameObject.transform.rotation.eulerAngles.z);
////
//					atSpot = true;
//				}
//
//
//				if(!atSpot){
//
//					return;
//
//				}

                if (nTarget < TTaget.Length)
                {
					

					if(turnIndex == 0){

//							nextStarLocate = new Vector3(TTaget [nTarget].transform.position.x - distBTWShip, TTaget [nTarget].transform.position.y + distBTWShip, TTaget [nTarget].transform.position.z);
							nextStarLocate = TTaget [nTarget].transform.Find("Orbit").gameObject.transform.Find("pos1").transform.position;

						}else if(turnIndex == 1){

//							nextStarLocate = new Vector3(TTaget [nTarget].transform.position.x + distBTWShip, TTaget [nTarget].transform.position.y + distBTWShip, TTaget [nTarget].transform.position.z);
							nextStarLocate = TTaget [nTarget].transform.Find("Orbit").gameObject.transform.Find("pos2").transform.position;

						}else if(turnIndex == 2){


//							nextStarLocate = new Vector3(TTaget [nTarget].transform.position.x - distBTWShip, TTaget [nTarget].transform.position.y - distBTWShip, TTaget [nTarget].transform.position.z);
							nextStarLocate = TTaget [nTarget].transform.Find("Orbit").gameObject.transform.Find("pos3").transform.position;

						}else if(turnIndex == 3){

//							nextStarLocate = new Vector3(TTaget [nTarget].transform.position.x + distBTWShip, TTaget [nTarget].transform.position.y - distBTWShip, TTaget [nTarget].transform.position.z);
							nextStarLocate = TTaget [nTarget].transform.Find("Orbit").gameObject.transform.Find("pos4").transform.position;

						}

                    //rotate
					if (doItMove) {
//						shipInUse[turnIndex].transform.SetParent(TTaget [nTarget].transform.Find("Orbit"));
						shipInUse[turnIndex].transform.SetParent(null);
						CamecamecamehaPilot.GetComponent<OverAllCameraLook>().spaceShip = usingSpaceship;
//						nextStarLocate = new Vector3(TTaget [nTarget].transform.position.x, TTaget [nTarget].transform.position.y + distBTWShip, TTaget [nTarget].transform.position.z);
						doItMove = false;
						FXControl.GetComponent<AudioSource>().clip = ShipMove ;
						FXControl.GetComponent<AudioSource> ().Play ();




						miniNumObj[nTarget].transform.Find("Text").GetComponent<Text>().fontSize = 26;
						miniNumObj[nTarget].transform.Find("Text").GetComponent<Text>().color = markColor;

						if(nTarget > 1 && nTarget< 28){
							miniNumObj[nTarget-1].transform.Find("Text").GetComponent<Text>().fontSize = 20;
							miniNumObj[nTarget-2].transform.Find("Text").GetComponent<Text>().fontSize = 14;
							miniNumObj[nTarget+1].transform.Find("Text").GetComponent<Text>().fontSize = 20;
							miniNumObj[nTarget+2].transform.Find("Text").GetComponent<Text>().fontSize = 14;
							miniNumObj[nTarget-1].transform.Find("Text").GetComponent<Text>().color = oldColor;
							miniNumObj[nTarget+1].transform.Find("Text").GetComponent<Text>().color = oldColor;

						}else if(nTarget == 1){

							miniNumObj[nTarget-1].transform.Find("Text").GetComponent<Text>().fontSize = 20;
							miniNumObj[nTarget+1].transform.Find("Text").GetComponent<Text>().fontSize = 20;
							miniNumObj[nTarget+2].transform.Find("Text").GetComponent<Text>().fontSize = 14;
							miniNumObj[nTarget-1].transform.Find("Text").GetComponent<Text>().color = oldColor;
							miniNumObj[nTarget+1].transform.Find("Text").GetComponent<Text>().color = oldColor;

						}else if(nTarget == 0 ){

							miniNumObj[nTarget+1].transform.Find("Text").GetComponent<Text>().fontSize = 20;
							miniNumObj[nTarget+2].transform.Find("Text").GetComponent<Text>().fontSize = 14;

							miniNumObj[nTarget+1].transform.Find("Text").GetComponent<Text>().color = oldColor;

						}else if (nTarget == 28){

							miniNumObj[nTarget-1].transform.Find("Text").GetComponent<Text>().fontSize = 20;
							miniNumObj[nTarget-2].transform.Find("Text").GetComponent<Text>().fontSize = 14;
							miniNumObj[nTarget+1].transform.Find("Text").GetComponent<Text>().fontSize = 20;
							miniNumObj[nTarget-1].transform.Find("Text").GetComponent<Text>().color = oldColor;
							miniNumObj[nTarget+1].transform.Find("Text").GetComponent<Text>().color = oldColor;

						}else if (nTarget == 29){

							miniNumObj[nTarget-1].transform.Find("Text").GetComponent<Text>().fontSize = 20;
							miniNumObj[nTarget-2].transform.Find("Text").GetComponent<Text>().fontSize = 14;
							miniNumObj[nTarget-1].transform.Find("Text").GetComponent<Text>().color = oldColor;


						}


					}

//                    usingSpaceship.transform.position = Vector3.MoveTowards(usingSpaceship.transform.position, new Vector3(TTaget[nTarget].transform.position.x, TTaget[nTarget].transform.position.y + distBTWShip, TTaget[nTarget].transform.position.z), shipSpeed * Time.deltaTime);
					usingSpaceship.transform.position = Vector3.MoveTowards(usingSpaceship.transform.position, nextStarLocate, shipSpeed * Time.deltaTime);
                    MinimapContent.GetComponent<RectTransform>().transform.localPosition = Vector3.MoveTowards(MinimapContent.GetComponent<RectTransform>().transform.localPosition, new Vector3(orgMini.x - (nTarget * numberBoxWidth), MinimapContent.GetComponent<RectTransform>().transform.localPosition.y, MinimapContent.GetComponent<RectTransform>().transform.localPosition.z), 275 * Time.deltaTime);
					

					Vector3 diff = usingSpaceship.transform.position - nextStarLocate;
					diff.Normalize ();
					float rot_z = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
					usingSpaceship.transform.rotation = Quaternion.Euler (0f, 0f, rot_z + 180);


//					usingSpaceship.transform.rotation = Quaternion.RotateTowards(usingSpaceship.transform.rotation, new Quaternion());
                    //usingSpaceship.transform.LookAt(lookAtPoint);


//					MinimapContent.transform.Find(""+nTarget).transform.localScale = new Vector3 (2,2,2);
//					print(MinimapContent.transform.Find(""+usingPos).name + "    ls  " + 	MinimapContent.transform.Find(""+usingPos).transform.GetComponent<RectTransform>().localScale  );
					

//					usingSpaceship();


					if (usingSpaceship.transform.position.Equals(nextStarLocate) && countDice > 0)
                    {
//						usingSpaceship.transform.SetParent(TTaget [nTarget].transform.Find("Orbit").gameObject.transform);
//						atSpot = false;
						doItMove = true;
                        countDice--;
                        doItOnce = true;

                        if (!goingBack)
                        {
//							if(usingPos<0){
                            usingPos++;
//							}
                        }
                        else
                        {
						
                            usingPos--;
//							print("using pos " + usingPos);

                        }



                    }


					if (usingSpaceship.transform.position.Equals(nextStarLocate) && countDice <= 0)
                    {
//						FXControl.GetComponent<AudioSource> ().Stop();

//						atSpot = false;
						CamecamecamehaPilot.GetComponent<OverAllCameraLook>().spaceShip = TTaget [nTarget];
						doItMove = true;
//                        usingSpaceship.transform.rotation = Quaternion.Euler(0, usingSpaceship.transform.rotation.eulerAngles.y, usingSpaceship.transform.rotation.eulerAngles.z);
						usingSpaceship.transform.SetParent(TTaget [nTarget].transform.Find("Orbit").gameObject.transform);
						shipInUse[turnIndex].transform.localRotation = Quaternion.Euler(0,0,360-(90*turnIndex));

//                        if (turnControl == "User")
//                        {
//
//                            playerPos = usingPos;
//							
//
//                        }
//                        else
//                        {
//
//                            enemyPos = usingPos;
//
//                        }

                        shipPosition[turnIndex] = usingPos;

                        goingBack = false;
                        CamecamecamehaPilot.GetComponent<OverAllCameraLook>().finishFirstMove = false;
                        gameState = GameState.Effect;

                    }

                }



	


                break;

            case GameState.Effect:
				

                if (doItOnce)
                {

//					print(turnControl + " is at " + (usingPos+1));

                    if ((usingPos + 1) == PositonInfoDict.Count)
                    {

//                        TextStageObj.GetComponent<Text>().text = "END!!! " + turnControl.ToString() + " Win!!";
						FXControl.GetComponent<AudioSource>().clip = EndGame ;
						FXControl.GetComponent<AudioSource> ().Play ();
						TextStageObj.GetComponent<Text>().text = "END!!! " + shipName[turnIndex].ToString() + " Win!!";

                        gameState = GameState.EndGame;
                        return;

                    }
                    else
                    {


                        if (PositonInfoDict["" + (usingPos + 1)][0].ToUpper().Equals("YES"))
                        {

							FXControl.GetComponent<AudioSource>().clip = QuesionPop  ;
							FXControl.GetComponent<AudioSource> ().Play ();
//							print("Q TIME");
                            QPanel.SetActive(true);
                            Question.GetComponent<Text>().text = PositonInfoDict["" + (usingPos + 1)][1];
                           				
                        }
                        else if (PositonInfoDict["" + ((usingPos + 1) % TTaget.Length)][0].Equals("Ladder"))
                        {
                   
                            effect2Move = true;
                            EffectPanel.SetActive(true);
							TextEffect.GetComponent<Text>().text = shipName[turnIndex].ToString() + " LADDER to " + PositonInfoDict["" + ((usingPos + 1) % TTaget.Length)][1];
                            countDice = (int.Parse(PositonInfoDict["" + ((usingPos + 1) % TTaget.Length)][1]) - 1) - usingPos;


                            //	gameState = GameState.MoveGame;

                        }
                        else if (PositonInfoDict["" + ((usingPos + 1) % TTaget.Length)][0].Equals("Snake"))
                        {

                            effect2Move = true;
                            EffectPanel.SetActive(true);
							TextEffect.GetComponent<Text>().text = shipName[turnIndex].ToString() + " Snake to " + PositonInfoDict["" + ((usingPos + 1) % TTaget.Length)][1];


                            countDice = usingPos - (int.Parse(PositonInfoDict["" + ((usingPos + 1) % TTaget.Length)][1]) - 1);
                            goingBack = true;



                        }
                        else if (PositonInfoDict["" + ((usingPos + 1) % TTaget.Length)][0].Equals("DoubleDice"))
                        {

                            usingSpaceship.GetComponent<PlayerStatus>().effct = PlayerStatus.StatusEffect.DoubleDice;
                            EffectPanel.SetActive(true);
							TextEffect.GetComponent<Text>().text = shipName[turnIndex].ToString() + " GOT DOUBLE DICE !!!!";
//							finishEffect = true;

                        }
                        else if (PositonInfoDict["" + ((usingPos + 1) % TTaget.Length)][0].Equals("Trap"))
                        {
                            TextStageObj.GetComponent<Text>().text = "TRAP!!!";
                            usingSpaceship.GetComponent<PlayerStatus>().effct = PlayerStatus.StatusEffect.Trap;
                            EffectPanel.SetActive(true);
							TextEffect.GetComponent<Text>().text = shipName[turnIndex].ToString() + " IS TRAPED!!!";
//							print(usingSpaceship.name);
//							finishEffect = true;

                        }
                        else if (PositonInfoDict["" + ((usingPos + 1) % TTaget.Length)][0].Equals("BackWard"))
                        {
                            TextStageObj.GetComponent<Text>().text = "BACKWARD!!!";
                            usingSpaceship.GetComponent<PlayerStatus>().effct = PlayerStatus.StatusEffect.BackBack;
                            EffectPanel.SetActive(true);
							TextEffect.GetComponent<Text>().text = shipName[turnIndex].ToString() + " IS CONFUSING";
//							finishEffect = true;

                        }
                        else if (PositonInfoDict["" + ((usingPos + 1) % TTaget.Length)][0].Equals("Swap"))
                        {
//							print(usingSpaceship.name);
//							CamecamecamehaPilot.GetComponent<OverAllCameraLook>().KeepDistance2ndStage = false;

						//temp pap
//                            tempPos1 = new Vector3(SpaceShip1.transform.position.x, SpaceShip1.transform.position.y, SpaceShip1.transform.position.z);
//                            tempPos2 = new Vector3(SpaceShip2.transform.position.x, SpaceShip2.transform.position.y, SpaceShip2.transform.position.z);
                            TextStageObj.GetComponent<Text>().text = "SWAPPING!!!";

                            int tempPlayerPos = playerPos;
                            playerPos = enemyPos;
                            enemyPos = tempPlayerPos;


                            gameState = GameState.Swapping;

							
                        }
                        else if (PositonInfoDict["" + ((usingPos + 1) % TTaget.Length)][0].Equals("AllRandom"))
                        {
							
                            //gameState = GameState.Swapping ;
							
                        }
                        else
                        {
                            //No FX
//								print("NOFX");
                            finishEffect = true;

                        }



                    }
                    doItOnce = false;
                }

//
                if (finishEffect)
                {
//					usingSpaceship.transform.SetParent(TTaget[usingPos].transform);
                    finishEffect = false;
                    doItOnce = true;
//                    if (turnControl == "User")
//                    {
//
//                        turnControl = "Robot";
//
//                    }
//                    else
//                    {
//
//                        turnControl = "User";
//
//                    }
					turnIndex = (turnIndex+1)%playerCount;
                    gameState = GameState.Start;
                }
				//Take Effect




                break;


            case GameState.Swapping:

//				CamecamecamehaPilot.transform.localPosition = Vector3.MoveTowards(CamecamecamehaPilot.transform.localPosition,new Vector3(usingSpaceship.transform.localPosition.x,usingSpaceship.transform.localPosition.y,tPos.z),20*Time.deltaTime);

//                if (!swapBool)
//                {
//
//                    //SpaceShip1.transform.position = Vector3.MoveTowards(SpaceShip1.transform.position,SpaceShip2.transform.position,10*Time.deltaTime);
//                    if (SpaceShip1.transform.localScale.x > 0)
//                    {
//						
//						
//                        SpaceShip1.transform.localScale = new Vector3(SpaceShip1.transform.localScale.x - 1f * Time.deltaTime, SpaceShip1.transform.localScale.y - 1f * Time.deltaTime, SpaceShip1.transform.localScale.z - 1f * Time.deltaTime);
//                        SpaceShip2.transform.localScale = new Vector3(SpaceShip2.transform.localScale.x - 1f * Time.deltaTime, SpaceShip2.transform.localScale.y - 1f * Time.deltaTime, SpaceShip2.transform.localScale.z - 1f * Time.deltaTime);
//                    }
//
//                }
//                else
//                {
//
//                    if (SpaceShip1.transform.localScale.x < 1 && CamecamecamehaPilot.GetComponent<OverAllCameraLook>().finishFirstMove)
//                    {
//                        SpaceShip1.transform.localScale = new Vector3(SpaceShip1.transform.localScale.x + 1f * Time.deltaTime, SpaceShip1.transform.localScale.y + 1f * Time.deltaTime, SpaceShip1.transform.localScale.z + 1f * Time.deltaTime);
//                        SpaceShip2.transform.localScale = new Vector3(SpaceShip2.transform.localScale.x + 1f * Time.deltaTime, SpaceShip2.transform.localScale.y + 1f * Time.deltaTime, SpaceShip2.transform.localScale.z + 1f * Time.deltaTime);
//                    }
//                    else if (SpaceShip1.transform.localScale.x > 1)
//                    {
//
//                        SpaceShip1.transform.localScale = new Vector3(1, 1, 1);
//                        SpaceShip2.transform.localScale = new Vector3(1, 1, 1);
//
//                    }
//
//                }
//
//                if (SpaceShip1.transform.localScale.x <= 0)
//                {
//
//                    SpaceShip1.transform.position = tempPos2;
//                    SpaceShip2.transform.position = tempPos1;
//					
//                    swapBool = true;
//                    CamecamecamehaPilot.GetComponent<OverAllCameraLook>().finishFirstMove = false;
//
//                }
//
//                if (SpaceShip1.transform.localScale.x == 1 && swapBool && CamecamecamehaPilot.GetComponent<OverAllCameraLook>().finishFirstMove)
//                {

					
//					usingSpaceship.transform.SetParent(TTaget[usingPos].transform);
                    finishEffect = false;
                    doItOnce = true;
                    swapBool = false;


					turnIndex = (turnIndex+1)%playerCount;
                    gameState = GameState.Start;

//                }


                break;


            case GameState.EndGame:


                break;



            case GameState.Waiting:


                if (waitTime > 0)
                {

                    waitTime -= Time.deltaTime;
                    print(waitTime + "");

                }
                else
                {
                    waitTime = 1f;
                    gameState = GameState.Start;

                }


                break;

            default : 

                break;

        }

    }

    public void RollDice()
    {

    	
        RollBtn.SetActive(false);
        dicePoint = ((Random.Range(1, 7) + Random.Range(1,7))%6)+1;

//		dicePoint = 1;

//		dicePoint = 4;
//		if(turnControl == "Robot"){
//
//			dicePoint = 3;
//
//		}


		TextStageObj.GetComponent<Text>().text = shipName[turnIndex].ToString() + " GOT " + dicePoint;

        if (usingSpaceship.GetComponent<PlayerStatus>().effct == PlayerStatus.StatusEffect.DoubleDice)
        {

            dicePoint = dicePoint * 2;
            usingSpaceship.GetComponent<PlayerStatus>().effct = PlayerStatus.StatusEffect.None;
        }

        if (usingSpaceship.GetComponent<PlayerStatus>().effct == PlayerStatus.StatusEffect.BackBack)
        {
			
            goingBack = true;
            usingSpaceship.GetComponent<PlayerStatus>().effct = PlayerStatus.StatusEffect.None;
        }

        countDice = dicePoint;
//		print(dicePoint);
//		nTarget = Random.Range(1,5);;
        doItOnce = true;
        gameState = GameState.MoveGame;

        //nTarget++;
        ScreenCTR.GetComponent<ScreenScroller>().canSwipe = false;
//		CamecamecamehaPilot.GetComponent<OverAllCameraLook>().SetOnShip();
        stopCam = false;


    }

    IEnumerator RobotRollDice()
    {

        yield return new WaitForSeconds(3);
        RollDice();
		
        StopCoroutine(RobotRollDice());

    }

    public void Answer1Clicked()
    {

        QPanel.SetActive(false);

        if (PositonInfoDict["" + (usingPos + 1)][5].Equals("1"))
        {
            countDice = Random.Range(1, 10);
            gameState = GameState.MoveGame;
        }
        else
        {

            finishEffect = true;

        }
        //finishEffect = true;
    }

    public void Answer2Clicked()
    {

        QPanel.SetActive(false);

        if (PositonInfoDict["" + (usingPos + 1)][5].Equals("2"))
        {
            countDice = Random.Range(1, 10);
            gameState = GameState.MoveGame;
        }
        else
        {

            finishEffect = true;

        }
    }

    public void Answer3Clicked()
    {

        QPanel.SetActive(false);

        if (PositonInfoDict["" + (usingPos + 1)][5].Equals("3"))
        {
            countDice = Random.Range(1, 10);
            gameState = GameState.MoveGame;
        }
        else
        {

            finishEffect = true;

        }
    }


    public void CloseEffectPanel()
    {
        if (gameState == GameState.Roll)
        {

                if (usingSpaceship.GetComponent<PlayerStatus>().effct != PlayerStatus.StatusEffect.Trap)
                {
					if(shipPlayerType[turnIndex] == "AI" ||shipPlayerType[turnIndex] == "Robot"){
                  	 	RollDice();
                    }else{

						RollBtn.SetActive(true);
                    }
                }
                else
                {

                    usingSpaceship.GetComponent<PlayerStatus>().effct = PlayerStatus.StatusEffect.None;
					turnIndex = (turnIndex+1)%playerCount;
                    doItOnce = true;
                    gameState = GameState.Start;

                }





//			doItOnce = true;
			ScreenCTR.GetComponent<ScreenScroller>().canSwipe = true;
            EffectPanel.SetActive(false);

        }
        else
        {

            if (effect2Move)
            {
                effect2Move = false;
                gameState = GameState.MoveGame;
                EffectPanel.SetActive(false);
			
            }
            else
            {
                EffectPanel.SetActive(false);
                finishEffect = true;
            }

        }
    }



    public void openShipTrail(int indexShip){

    	for(int i = 0 ; i < playerCount ; i++){

    		
			shipInUse[i].GetComponent<TrailRenderer>().enabled = false;


    	}

    	shipInUse[indexShip].GetComponent<TrailRenderer>().enabled = true;

    }

}
