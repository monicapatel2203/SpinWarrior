using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour 
{
	public List<Sprite> WhichModeRunnig = new List<Sprite> ();  // Displaying Current Selected Mode...
	public List<GameObject> Character = new List<GameObject> (); // For Character Selection...
	public List<GameObject> NinjaEye = new List<GameObject> (); // Change Eye As Per Required...
	public List<GameObject> CollectiblePH = new List<GameObject> (); // Generating Position Of Collectible...
	public List<GameObject> PhEnimy = new List<GameObject> (); // Generating Position Of Shurikon...
	public List<GameObject> kuniaPhList = new List<GameObject> (); // Generating Position Of Kunai...
	public List<GameObject> spikePhList = new List<GameObject> (); // Generating Position Of Spike...
	public List<GameObject> Weapon = new List<GameObject>(); // All Weapon List...
	public List<GameObject> PowerUp = new List<GameObject>(); // Power Up Ref...
	public List<GameObject> GeneratedEnimyClone = new List<GameObject> (); // Store All Enimy In One List...
	public List<GameObject> SideBorderClone = new List<GameObject> (); // Side Bamboo Image...
	public List<GameObject> patternPHList = new List<GameObject> (); // Generating Position Of Pattern...
	public List<Sprite> MusicBtnList = new List<Sprite> (); // Sound On Off Button...
	public List<GameObject> BonusCollectibleStars = new List<GameObject> (); // Store All Enimy In One List...

	// All GameObject For Reference...
	public GameObject destroyPowerUp , collectPowerUp , EnimyKilledParticles , LevelUpPaticle , VictoryParticle_1, PlayerKilledParticle , SoundParent , Wall , LevelCompletePanel , CanvasRef , interNetCheck;
	public GameObject mainCam , player , enemy , CollectiblePos , EnimyPos , kuniaPos , upkuniaPos , spikePos , PowerupPanel , GameOverPanel , LevelFillBar , TapToPlay , SoundHandle , mainCamera, GameContinuePopup;
	public GameObject LastSelectedChar, Countdown_Canvas , StartTimeCounter , ScorePanel , patternPos , tapToPlayPanel , BonusLevelText , BonusLevelMsg , FB_Btn , TimerModeSelect , NormalModeSelect , CurrentModeImage , MusicBtn;
	public GameObject SelectCharStar;
	GameObject CollectibleClone , EnimyClone , kunaiWeaponClone , UpkunaiWeaponClone , spikeClone , patternClone ,patternRightClone , patternLeftClone;
	public Text HighScoreTxt, GOScoreTxt , TotalPowerUp; // Display HighScore , CurrentScore , CurrentStar...
	public Text CurrentLevel_text, LevelText , ScoreText , PowerUpTxt, PowerUpTxt_InApp, LC_Score_Text, GO_Score_Text; // Display CurrentLevel , Score , Current Star On GamePlay Screen...
	float currentLevelNo , AdsTimer;
	public bool isTimeOver;
	public static bool isTimeBaseFillbarOver;
	public bool isGameStart , isGamePause , isBonusLevelTime;
	public bool isGameOver , isLevelContinue , isAdsRunnig;
	int c;
	int score , prevValue , wallPrevValue , kuniaPrevvalue , powerUpNo;
	// References Of Other Classes To Use In This Class...
	Wall Hwall;
	TimeGameplay _gTimeGamePlay;
	CharacterSelection _gCharSelection;
	// UnityAdsButton _gAds;
	GoogleMobileAdsDemoScript _googleAds;
	SoundControl _gSoundControl;
	 
	public GameObject AdsFillBar;
	public GameObject BottomPatternPanel;
	public bool _applicationPause;
	public GameObject PausePanel;
	public GameObject SplashCanvas, LevelUpPanel, LevelPanel_Anim,Level_SubPanel, Wall_obj;
	public List<GameObject> BambooObjects;
	
	public GameObject ModeSelectionPanel,CharacterSelectionPanel,InAppPanel;
	public bool Inappactive;
	public GameObject PlayerFillBar, VictoryParticle_Pos1, LevelNo_Bar,BonusImage, fillbarParent, Powerup_parent;
	public Text CurrentLevel_normal, NextLevel_normal, CurrentLevel_timer, NextLevel_timer, LevelTxt_home, LevelText_title, TotalStar_collected, CurrentLevel_star;
	public List<GameObject> LevelImg_bonus, LevelTxt_bonus, BonusLevel_Lock;
	public int Powerup_count;
	public Text _CurrentPowerUp_count, GO_Powerup_count;
	public List<Sprite> Playersprites, Go_PlayerSprites;
	public Image LC_Player_image, GO_Player_image, BonusLevel_fillbar, Currentimg_normal, Nextimg_normal, Currentimg_timer, Nextimg_timer;

	void Awake()
	{
		if (PlayerPrefs.GetInt ("FirstTime")==0) // First Time Set All PlayerPrefs...
		{
			PlayerPrefs.SetFloat ("EnimyGenerateTime",0.7f); 
			PlayerPrefs.SetFloat ("GenKunaiWeaponTime",5f);
			PlayerPrefs.SetFloat ("GenUpKunaiWeaponTime",6.5f);
			PlayerPrefs.SetFloat ("GenSpikeWeaponTime",6f); 
			PlayerPrefs.SetFloat ("GenPatternWeaponTime",6f);

			PlayerPrefs.SetFloat ("CurrentLevel",1);
			PlayerPrefs.SetFloat ("TimeCurrentLevel",1);
			PlayerPrefs.SetFloat ("OnBonusLevel",0);
			PlayerPrefs.SetInt ("GotBonusLevel", 1);

			PlayerPrefs.SetInt ("LevelUpScore",0);
			PlayerPrefs.SetInt ("FirstTime", 1);
			PlayerPrefs.SetFloat ("WallRotationSpeed" , 10f);				//8f

			PlayerPrefs.SetString ("isNormalGameRunning","true");
			PlayerPrefs.SetString ("isTimeBaseGameRunnig","false");
			PlayerPrefs.SetString ("isDoubleGameRunnig","false");

			PlayerPrefs.SetInt ("PowerUpCollect", 0);

			PlayerPrefs.SetInt ("SelectedCharacter", 0);
			PlayerPrefs.SetString ("SelectedPlayerName", "NinjaPlayer");
		}

		if (PlayerPrefs.GetInt ("BonusLevelConti") == 1)
		{
			Debug.LogError ("BonusLEv....!");
			PlayerPrefs.SetInt ("BonusLevelConti", PlayerPrefs.GetInt("BonusLevelConti"));
			PlayerPrefs.SetInt ("GotBonusLevel", PlayerPrefs.GetInt("GotBonusLevel"));
			PlayerPrefs.SetFloat ("OnBonusLevel", PlayerPrefs.GetFloat("OnBonusLevel"));
			// PlayerPrefs.SetFloat ("CurrentLevel",PlayerPrefs.GetFloat("CurrentLevel")+1f);
		}

		isAdsRunnig = false;
		CheckMusicInStart ();
	}

	void Start ()
	{
		// PlayerPrefs.DeleteAll();
		Inappactive = false;
		// if( PlayerPrefs.GetInt("GamePlay") == 1 )
		// {
		// 	SplashCanvas.SetActive(false);
		// 	PlayerPrefs.SetInt("GamePlay",0);
		// 	// PlayerPrefs.SetInt("ResumeGame",0);
		// }
		// else
		// {
		// // 	if( PlayerPrefs.GetInt("ResumeGame") == 1 )
		// // 	{
		// // 		PlayerPrefs.SetInt("ResumeGame",0);
		// // 		SplashCanvas.SetActive(false);
		// // 		ScorePanel.SetActive(true);
		// // 		BottomPatternPanel.SetActive(false);
		// // 		OnGameStart();				
		// // 	}
		// // 	else
		// // 	{
		// 		SplashCanvas.SetActive(true);
		// 		StartCoroutine(SplashCanvas_Close(4.0f));
		// 		// PlayerPrefs.SetInt("ResumeGame",0);
		// // }
		// }
		
		_applicationPause = false;
		PlayerPrefs.SetInt ("LevelUpScore",0);
		PlayerPrefs.SetInt ("ShowAds", 0);
		player = Character[PlayerPrefs.GetInt("SelectedCharacter")];
		player.SetActive (true);
		LastSelectedChar = player;

		CanvasRef.transform.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
		LevelChange ();

		Hwall = FindObjectOfType<Wall> ();
		_gTimeGamePlay = FindObjectOfType<TimeGameplay> ();
		// _gAds = FindObjectOfType<UnityAdsButton> ();
		_googleAds = FindObjectOfType<GoogleMobileAdsDemoScript>();
		_gCharSelection = FindObjectOfType<CharacterSelection> ();
		_gSoundControl = FindObjectOfType<SoundControl> ();

		isGameOver = false;

		if (PlayerPrefs.GetString ("isNormalGameRunning") == "true")
		{
			if(PlayerPrefs.GetFloat ("OnBonusLevel") == 1)
			{
				LevelFillBar.transform.GetComponent<Image>().fillOrigin = 1;
				// fillbarParent.SetActive(true);
				// Powerup_parent.SetActive(true);
			}
			else
			{
				LevelFillBar.transform.GetComponent<Image>().fillOrigin = 0;
			}			
			for (int n = 0; n < SideBorderClone.Count; n++) 
			{
//				SideBorderClone [2].gameObject.SetActive (false);
//				SideBorderClone [3].gameObject.SetActive (false);
				SideBorderClone [n].gameObject.GetComponent<BoxCollider2D> ().enabled = false;
			}
		}

		else if (PlayerPrefs.GetString ("isTimeBaseGameRunnig") == "true")
		{
			LevelFillBar.transform.GetComponent<Image>().fillOrigin = 1;
			for (int t = 0; t < SideBorderClone.Count; t++) 
			{
				//SideBorderClone [t].gameObject.SetActive (true);
				SideBorderClone [t].gameObject.GetComponent<BoxCollider2D> ().enabled = true;
			}
		}

		Hwall.OnWallRotationSpeed ();
		player.transform.GetComponent<CircleCollider2D> ().enabled = true;

		if( PlayerPrefs.GetFloat ("CurrentLevel") > 0 || PlayerPrefs.GetFloat ("TimeCurrentLevel") > 0 )
		{
			LevelText_title.enabled = true;
			LevelTxt_home.enabled = true;			
			if( PlayerPrefs.GetString ("isNormalGameRunning") == "true" )
			{
				LevelTxt_home.text = PlayerPrefs.GetFloat ("CurrentLevel").ToString();
				LevelNo_Bar.SetActive(true);
				for( int i = 1 ; i <= LevelImg_bonus.Count; i++)
				{
					int count = int.Parse(LevelTxt_home.text);
					Debug.LogError("count..." + count + "  count % 5...." + count % 5);
					if( count % 5 == i )
					{
						LevelImg_bonus[i-1].SetActive(true);
						LevelImg_bonus[i-1].transform.GetChild(0).GetComponent<Text>().text = count.ToString();
						LevelTxt_bonus[i-1].SetActive(false);
						BonusImage.SetActive(false);
					}
					else
					{
						LevelImg_bonus[i-1].SetActive(false);
						LevelTxt_bonus[i-1].SetActive(true);
						BonusImage.SetActive(false);
					}
					// if( count % 5 == 0 )
					// {
					// 	BonusImage.SetActive(true);
					// }
				}
				float tempNo = PlayerPrefs.GetFloat ("CurrentLevel") % 5;
				if(tempNo == 1)
				{
					for( int i = 0; i < LevelTxt_bonus.Count; i++)
					{
						LevelTxt_bonus[i].GetComponent<Text>().text = PlayerPrefs.GetFloat ("CurrentLevel").ToString();
						LevelTxt_bonus[i + 1].GetComponent<Text>().text = (PlayerPrefs.GetFloat ("CurrentLevel") + 1).ToString();
						LevelTxt_bonus[i + 2].GetComponent<Text>().text = (PlayerPrefs.GetFloat ("CurrentLevel") + 2).ToString();
						LevelTxt_bonus[i + 3].GetComponent<Text>().text = (PlayerPrefs.GetFloat ("CurrentLevel") + 3).ToString();
						break;
					}
					for( int j = 0; j < BonusLevel_Lock.Count; j++ )
					{
						BonusLevel_Lock[j].SetActive(false);
						BonusLevel_Lock[j + 1].SetActive(true);
						BonusLevel_Lock[j + 2].SetActive(true);
						BonusLevel_Lock[j + 3].SetActive(true);
						break;
					}
				}
				else if(tempNo == 2)
				{
					BonusLevel_fillbar.fillAmount += 0.25f;
					for( int i = 0; i < LevelTxt_bonus.Count; i++)
					{
						LevelTxt_bonus[i].GetComponent<Text>().text = (PlayerPrefs.GetFloat ("CurrentLevel") - 1).ToString();
						LevelTxt_bonus[i + 1].GetComponent<Text>().text = (PlayerPrefs.GetFloat ("CurrentLevel")).ToString();
						LevelTxt_bonus[i + 2].GetComponent<Text>().text = (PlayerPrefs.GetFloat ("CurrentLevel") + 1).ToString();
						LevelTxt_bonus[i + 3].GetComponent<Text>().text = (PlayerPrefs.GetFloat ("CurrentLevel") + 2).ToString();
						break;
					}
					for( int j = 0; j < BonusLevel_Lock.Count; j++ )
					{
						BonusLevel_Lock[j].SetActive(false);
						BonusLevel_Lock[j + 1].SetActive(false);
						BonusLevel_Lock[j + 2].SetActive(true);
						BonusLevel_Lock[j + 3].SetActive(true);
						break;
					}
				}
				else if(tempNo == 3)
				{
					BonusLevel_fillbar.fillAmount += 0.5f;
					for( int i = 0; i < LevelTxt_bonus.Count; i++)
					{
						LevelTxt_bonus[i].GetComponent<Text>().text = (PlayerPrefs.GetFloat ("CurrentLevel") - 2).ToString();
						LevelTxt_bonus[i + 1].GetComponent<Text>().text = (PlayerPrefs.GetFloat ("CurrentLevel") - 1).ToString();
						LevelTxt_bonus[i + 2].GetComponent<Text>().text = (PlayerPrefs.GetFloat ("CurrentLevel")).ToString();
						LevelTxt_bonus[i + 3].GetComponent<Text>().text = (PlayerPrefs.GetFloat ("CurrentLevel") + 1).ToString();
						break;
					}
					for( int j = 0; j < BonusLevel_Lock.Count; j++ )
					{
						BonusLevel_Lock[j].SetActive(false);
						BonusLevel_Lock[j + 1].SetActive(false);
						BonusLevel_Lock[j + 2].SetActive(false);
						BonusLevel_Lock[j + 3].SetActive(true);
						break;
					}
				}
				else if(tempNo == 4)
				{
					BonusLevel_fillbar.fillAmount += 0.75f;
					for( int i = 0; i < LevelTxt_bonus.Count; i++)
					{
						LevelTxt_bonus[i].GetComponent<Text>().text = (PlayerPrefs.GetFloat ("CurrentLevel") - 3).ToString();
						LevelTxt_bonus[i + 1].GetComponent<Text>().text = (PlayerPrefs.GetFloat ("CurrentLevel") - 2).ToString();
						LevelTxt_bonus[i + 2].GetComponent<Text>().text = (PlayerPrefs.GetFloat ("CurrentLevel") - 1).ToString();
						LevelTxt_bonus[i + 3].GetComponent<Text>().text = (PlayerPrefs.GetFloat ("CurrentLevel")).ToString();
						break;
					}
					for( int j = 0; j < BonusLevel_Lock.Count; j++ )
					{
						BonusLevel_Lock[j].SetActive(false);
						BonusLevel_Lock[j + 1].SetActive(false);
						BonusLevel_Lock[j + 2].SetActive(false);
						BonusLevel_Lock[j + 3].SetActive(false);
						break;
					}
				}
				else if(tempNo == 0)
				{
					BonusLevel_fillbar.fillAmount += 0.95f;
					for( int i = 0; i < LevelTxt_bonus.Count; i++)
					{
						LevelTxt_bonus[i].GetComponent<Text>().text = (PlayerPrefs.GetFloat ("CurrentLevel") - 4).ToString();
						LevelTxt_bonus[i + 1].GetComponent<Text>().text = (PlayerPrefs.GetFloat ("CurrentLevel") - 3).ToString();
						LevelTxt_bonus[i + 2].GetComponent<Text>().text = (PlayerPrefs.GetFloat ("CurrentLevel") - 2).ToString();
						LevelTxt_bonus[i + 3].GetComponent<Text>().text = (PlayerPrefs.GetFloat ("CurrentLevel") -1).ToString();
						break;
					}
					for( int j = 0; j < BonusLevel_Lock.Count; j++ )
					{
						BonusLevel_Lock[j].SetActive(false);
						BonusLevel_Lock[j + 1].SetActive(false);
						BonusLevel_Lock[j + 2].SetActive(false);
						BonusLevel_Lock[j + 3].SetActive(false);
						break;
					}
				}
			}
			else
			{
				LevelTxt_home.text = PlayerPrefs.GetFloat ("TimeCurrentLevel").ToString();
				LevelNo_Bar.SetActive(false);
			}
		}
		else
		{
			LevelText_title.enabled = false;
			LevelTxt_home.enabled = false;			
		}
		// Debug.LogError("BonusLevel..." + PlayerPrefs.GetFloat ("OnBonusLevel"));
		Debug.LogError("CUrrentLevel_txt..." + _CurrentPowerUp_count.text + " PowerUpCollect..." + PlayerPrefs.GetInt("PowerUpCollect"));
	}

	IEnumerator SplashCanvas_Close( float delay )
	{
		yield return new WaitForSeconds(delay);
		SplashCanvas.SetActive(false);
	}

	public void InAppBackBtn()
	{
		Debug.LogError("InApp Panel is active....");
		InAppPanel.SetActive(false);
		CharacterSelectionPanel.SetActive(true);
		CanvasRef.transform.GetComponent<Canvas> ().worldCamera = mainCam.GetComponent<Camera>();
	}

	void Update () 
	{
		// if( PlayerFillBar.transform.GetComponent<Image>().fillAmount >= 0.96f )
		// {
		// 	player.transform.GetComponent<CircleCollider2D> ().enabled = false;

		// 	// Instantiate (PlayerKilledParticle, transform.position, Quaternion.identity);

		// 	isGameOver = true;
		// 	isLevelContinue = true;
		// }
		// if( player.GetComponent<SpriteRenderer>().color.a <= 0.25f )
		// {
		// 	Debug.Log("Player opacity is low...");
		// 	player.transform.GetComponent<CircleCollider2D> ().enabled = false;

		// 	isGameOver = true;
		// 	isLevelContinue = true;
		// }
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Debug.LogError("Escape was pressed.....");
			if(ModeSelectionPanel.activeInHierarchy)
			{
				Debug.LogError("ModeSelection Panel is active....");
				ModeSelectionPanel.SetActive(false);
				BackFormOtherScreen();
			}
			if(CharacterSelectionPanel.activeInHierarchy)
			{
				Debug.LogError("CharacterSelection Panel is active....");
				CharacterSelectionPanel.SetActive(false);
				if(Inappactive == true)
				{
					Debug.LogError("InAppactive...if...." + Inappactive);
					CanvasRef.transform.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
					Inappactive = false;
				}
				else
				{
					Debug.LogError("InAppactive...else...." + Inappactive);
					BackFormOtherScreen();					
				}
			}
			if(InAppPanel.activeInHierarchy)
			{
				Inappactive = true;
				Debug.LogError("InApp Panel is active....");
				InAppPanel.SetActive(false);
				CharacterSelectionPanel.SetActive(true);
				CanvasRef.transform.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
			}
			
		}
		if (isGameOver) // GameOver As Per Curent Mode....
		{
			if (PlayerPrefs.GetString ("isNormalGameRunning") == "true")
			{
				StopEnemyAttack ();
				// Invoke ("OnGameOverScreen",0.5f);			//2f
				if( PlayerPrefs.GetFloat("CurrentLevel") > 3 )
				{
					Invoke ("GameOverPopup",0.5f);
				}
				else
				{
					Invoke("OnGameOverScreen_easyLevel",0.5f);
				}				

				StopKunaiWeaponCall ();
				StopGeneratedEnimy ();
				StopCollectible();
				StopuPKunaiWeaponCall ();
				StopSpikeWeaponCall ();
				OnPatternCancle ();


				for (int w = 0; w < Wall.transform.childCount; w++) 
				{
					Wall.transform.GetChild (w).GetComponent<PolygonCollider2D> ().enabled = false;
				}

				player.transform.GetComponent<CircleCollider2D> ().enabled = false;

				for(int i1 = 0; i1 < player.transform.childCount; i1++)
				{
					if (i1 == 5 || i1 == 6)
					{}
					else
						player.transform.GetChild (i1).gameObject.SetActive (false);
				}

				player.transform.GetChild (5).gameObject.SetActive (true);
				player.transform.GetChild (6).gameObject.SetActive (true);				
				// if( player.gameObject.name != "PigPlayer" || player.gameObject.name != "NinjaPlayer" || player.gameObject.name != "OwlPlayer" || player.gameObject.name != "BirdPlayer" )
				if( player.gameObject.name == "PigPlayer" || player.gameObject.name == "NinjaPlayer" || player.gameObject.name == "OwlPlayer" || player.gameObject.name == "BirdPlayer" || player.gameObject.name == "HippoPlayer" )
				{
					
				}
				else
				{					
					player.transform.GetChild (7).gameObject.SetActive (false);
					player.transform.GetChild (8).gameObject.SetActive (true);
				}

				CurrentModeImage.transform.GetComponent<Image> ().sprite = WhichModeRunnig [0];

				isGameOver = false;
				isGameStart = false;
			}

			else if(PlayerPrefs.GetString ("isTimeBaseGameRunnig") == "true")
			{
				// Invoke ("OnGameOverScreen",0.5f);			//2f
				if( PlayerPrefs.GetFloat("TimeCurrentLevel") > 3 )
				{
					Invoke ("GameOverPopup",0.5f);
				}
				else
				{
					Invoke("OnGameOverScreen_easyLevel",0.5f);
				}

				_gTimeGamePlay.DesEnemyAttack ();

				//player.transform.GetComponent<CircleCollider2D> ().enabled = false;

				for(int i1 = 0; i1 < player.transform.childCount; i1++)
				{
					if (i1 == 5 || i1 == 6)
					{}
					else
						player.transform.GetChild (i1).gameObject.SetActive (false);
				}

				player.transform.GetChild (5).gameObject.SetActive (true);
				player.transform.GetChild (6).gameObject.SetActive (true);
				// if( player.gameObject.name != "PigPlayer" || player.gameObject.name != "NinjaPlayer" || player.gameObject.name != "OwlPlayer" || player.gameObject.name != "BirdPlayer" )
				if( player.gameObject.name == "PigPlayer" || player.gameObject.name == "NinjaPlayer" || player.gameObject.name == "OwlPlayer" || player.gameObject.name == "BirdPlayer" || player.gameObject.name == "HippoPlayer" )
				{
					
				}
				else
				{					
					player.transform.GetChild (7).gameObject.SetActive (false);
					player.transform.GetChild (8).gameObject.SetActive (true);
				}
				

				CurrentModeImage.transform.GetComponent<Image> ().sprite = WhichModeRunnig [1];

				isGameOver = false;
				isGameStart = false;

			}
			isAdsRunnig = true;
			_gSoundControl.OnGameOver ();

		}

		if(PlayerPrefs.GetString ("isTimeBaseGameRunnig") == "true" && isGameStart) // Timer Mode Is Running....................
		{
			if(PlayerPrefs.GetFloat ("TimeCurrentLevel") > 10 && PlayerPrefs.GetFloat ("TimeCurrentLevel") < 21)
			{
				LevelFillBar.transform.GetComponent<Image> ().fillAmount -= 1f/35*Time.deltaTime;

				if (LevelFillBar.transform.GetComponent<Image> ().fillAmount <= 0 && !isTimeOver)
				{
					isGameStart = false;
					isTimeBaseFillbarOver = true;
					isTimeOver = true;
					Hwall.OnTimerGameLevelUp();
				}
			}

			else if(PlayerPrefs.GetFloat ("TimeCurrentLevel") > 20 && PlayerPrefs.GetFloat ("TimeCurrentLevel") < 31)
			{
				LevelFillBar.transform.GetComponent<Image> ().fillAmount -= 1f/38*Time.deltaTime;

				if (LevelFillBar.transform.GetComponent<Image> ().fillAmount <= 0 && !isTimeOver)
				{
					isGameStart = false;
					isTimeBaseFillbarOver = true;
					isTimeOver = true;
					Hwall.OnTimerGameLevelUp();
				}
			}

			else if(PlayerPrefs.GetFloat ("TimeCurrentLevel") > 30)
			{
				LevelFillBar.transform.GetComponent<Image> ().fillAmount -= 1f/40*Time.deltaTime;

				if (LevelFillBar.transform.GetComponent<Image> ().fillAmount <= 0 && !isTimeOver)
				{
					isGameStart = false;
					isTimeBaseFillbarOver = true;
					isTimeOver = true;
					Hwall.OnTimerGameLevelUp();
				}
			}
			else
			{
				LevelFillBar.transform.GetComponent<Image> ().fillAmount -= 1f/30*Time.deltaTime;

				if (LevelFillBar.transform.GetComponent<Image> ().fillAmount <= 0 && !isTimeOver)
				{
					isGameStart = false;
					isTimeBaseFillbarOver = true;
					isTimeOver = true;
					Hwall.OnTimerGameLevelUp();
				}
			}
		}

		if( LevelUpPanel.active )
		{

		}
		else
		{
			if (PlayerPrefs.GetFloat ("OnBonusLevel") == 1) // Bonus Level Running. Extra Star Collectionsss.......
			{
				LevelFillBar.transform.GetComponent<Image> ().fillAmount -= 1f/25*Time.deltaTime;


				// For Stop All Other Functionalities Cancle All Invoke........
				StopEnemyAttack();
				StopKunaiWeaponCall ();
				StopGeneratedEnimy ();
				StopCollectible();
				StopuPKunaiWeaponCall ();
				StopSpikeWeaponCall ();
				OnPatternCancle ();
				// OnBonusLevel ();
				// InvokeRepeating ("OnBonusLevel",1f , 2f);

				if (LevelFillBar.transform.GetComponent<Image> ().fillAmount <= 0 && !isBonusLevelTime)
				{
					if (PlayerPrefs.GetString ("isNormalGameRunning") == "true")
					{
						isBonusLevelTime = true;
						PlayerPrefs.SetFloat ("OnBonusLevel", 0);
						CancelInvoke ("oBonusLevelCatch");
						PlayerPrefs.SetInt ("BonusLevelConti",0);
						ScorePanel.transform.GetChild(0).gameObject.SetActive(true);
						ScorePanel.transform.GetChild(1).gameObject.SetActive(true);
						ScorePanel.transform.GetChild(2).gameObject.SetActive(true);
						ScorePanel.transform.GetChild(3).gameObject.SetActive(true);
						Hwall.OnScoreUpdated (0);
						BonusLevelTextDisable ();

						foreach(GameObject BonusStars in BonusCollectibleStars)
						{
							Destroy (BonusStars);
						}

						OnCancleBonusLevel ();
						LevelText.enabled = false;
						CurrentLevel_text.enabled = false;
						// LevelChange ();
					}
				}			
			}
		}		

		if (PlayerPrefs.GetInt ("BonusCollected") == 1) // Eye Animation On Bonus Level........
		{
			EyeAnimStart ();
			PlayerPrefs.SetInt ("BonusCollected", 0);
		}

		if(PlayerPrefs.GetInt("ShowAds")==1) // Chance To Show Ads And Play From Current Position.....
		{
			AdsFillBar.transform.GetComponent<Image> ().fillAmount -=  0.004f;		//0.01f

			if (AdsFillBar.transform.GetComponent<Image> ().fillAmount <= 0)
			{
				PlayerPrefs.SetInt ("ShowAds", 0);
				AdsFillBar.transform.GetChild(0).GetComponent<Button> ().interactable = false; 
			}

			// if (AdsFillBar.transform.GetComponent<Image> ().fillAmount == 0)
			// {
			// 	Debug.LogError("fill bar 0");
			// 	Time.timeScale = 0;
			// }
		}
	}

	public void OnGameStart() // GamePlay Start........
	{
		_applicationPause = true;
		SoundControl _soundScript = FindObjectOfType<SoundControl>();
		if( PlayerPrefs.GetInt("ResumeGame") == 1 )
		{
			// Debug.LogError("Resume Game prefs..if..");
		}
		else
		{
			// Debug.LogError("Resume Game prefs..else..");			
			_soundScript.OnSwipePlay ();
		}		

		player = Character[PlayerPrefs.GetInt("SelectedCharacter")];
		player.SetActive (true);
		tapToPlayPanel.SetActive (false);
		LastSelectedChar = player;

		PowerUpTxt.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
		isGameStart = true;
		ScorePanel.SetActive (true);
		

		if (PlayerPrefs.GetString ("isNormalGameRunning") == "true") // Normal Mode Handling......
		{
			Debug.LogError("OnBonusLevel..." + PlayerPrefs.GetFloat ("OnBonusLevel"));
			if (PlayerPrefs.GetFloat ("OnBonusLevel") == 1)
			{
				// fillbarParent.SetActive(true);
				// Powerup_parent.SetActive(true);
				// InvokeRepeating ("OnBonusLevel",1f , 2f);
				Debug.LogError("Bonus level Continue...");
				OnBonusLevel();
			}
			else
			{
				StartGeneratingEnemy (); // Enemy Generating......
				CollectibleStart (); // Colllectible Starts......

				if (PlayerPrefs.GetFloat ("CurrentLevel") >= 26)
				{
					PlayerPrefs.SetFloat ("EnimyGenerateTime", 1.5f);
					PlayerPrefs.SetFloat ("GenSpikeWeaponTime", 3.5f);
					PatternWeaponCall ();
					SpikeWeaponCall ();
				} 

				else if (PlayerPrefs.GetFloat ("CurrentLevel") >= 21) 
				{
					PlayerPrefs.SetFloat ("GenUpKunaiWeaponTime", 4f);
					PlayerPrefs.SetFloat ("GenSpikeWeaponTime", 4f);
					PlayerPrefs.SetFloat ("EnimyGenerateTime", 2f);

					UpKunaiWeaponCall ();
					SpikeWeaponCall ();
				}
				else if (PlayerPrefs.GetFloat ("CurrentLevel") >= 16) 
				{
					PlayerPrefs.SetFloat ("GenKunaiWeaponTime", 5f); 
					PlayerPrefs.SetFloat ("GenUpKunaiWeaponTime", 3.5f);

					PlayerPrefs.SetFloat ("EnimyGenerateTime", 2f);
					UpKunaiWeaponCall ();
				} 

				if (PlayerPrefs.GetFloat ("CurrentLevel") >= 6 && PlayerPrefs.GetFloat ("CurrentLevel") <= 20)
				{
					PlayerPrefs.SetFloat ("EnimyGenerateTime", 1.2f);
					PlayerPrefs.SetFloat ("GenKunaiWeaponTime", 5f); 
					PlayerPrefs.SetFloat ("GenUpKunaiWeaponTime", 4.5f);

					KunaiWeaponCall ();
				}
				else if (PlayerPrefs.GetFloat ("CurrentLevel") < 6f)
				{
					PlayerPrefs.SetFloat ("EnimyGenerateTime", 0.9f);
				}
			}			
		} 

		else if (PlayerPrefs.GetString ("isTimeBaseGameRunnig") == "true") // Timer Mode Running........
		{
			_gTimeGamePlay.OnTimeModeGame ();
		}
		//_gSoundControl.OnGamePlayStart (); // GamePlay Start Sound.......
		_soundScript.OnGamePlayStart();
	}

	public void StartPlay() // Once You Finished Watching Ads.........
	{
		
		player.transform.GetComponent<CircleCollider2D> ().enabled = true;
		GameOverPanel.SetActive (false);
		GameContinuePopup.SetActive(false);
		player.SetActive(true);
		ScorePanel.SetActive(true);
		Wall_obj.SetActive(true);
		// var rendererComponents = player.GetComponentsInChildren<SpriteRenderer>(true);

		// foreach (var component in rendererComponents)
		// {
		// 	Color tmp = component.color;
		// 	tmp.a = 1;
		// 	component.color = tmp;
		// }
		
		CancelInvoke ("OnCollectibleGenerate");
		StopCollectible ();
		CancelInvoke ("GenerateEnemy");
		TimeGameplay _timeGamePlayScript = FindObjectOfType<TimeGameplay>();
		_timeGamePlayScript.OnStopTimeCollectible();

		StopEnemyAttack ();
		
		//player.transform.GetComponent<Animation>().Play("PlayerEndAnim");
		StartCoroutine(Countdown(3));
	}

	IEnumerator Countdown(int seconds) // After 3 count game will continue from where you left......
	{
		int count = seconds;

		while (count > 0) 
		{
			// Display something...
			Countdown_Canvas.SetActive(true);
			StartTimeCounter.SetActive(true);
			for(int i = 0; i < BambooObjects.Count; i++)
			{
				BambooObjects[i].SetActive(true);
			}
			StartTimeCounter.transform.GetComponent<Text>().text = count.ToString();
			yield return new WaitForSeconds(1);
			count --;
		}

		// Count down is finished...
		//player.transform.GetComponent<Animation>().Play("PlayerStartAnim");
		StartTimeCounter.SetActive(false);
		Countdown_Canvas.SetActive(false);
		for(int i = 0; i < BambooObjects.Count; i++)
		{
			BambooObjects[i].SetActive(false);
		}
		// _gAds.OnRewardBasePlay ();
		_applicationPause = true;
		Debug.LogError("reward base play started....");
		_googleAds.OnRewardBasePlay();

		if (PlayerPrefs.GetString ("isTimeBaseGameRunnig") == "true") 
		{
			Debug.LogError("Countdown over....");
			_gTimeGamePlay.OnTimeModeGame ();
			//Invoke ("OnPlayerCollider",4f);
		}
	}

//	void OnPlayerCollider()
//	{
//		player.transform.GetComponent<CircleCollider2D> ().enabled = true;
//	}

	public void OnGameOverScreen() // Game Over Screen..... 
	{
		if(Application.internetReachability == NetworkReachability.NotReachable)
		{

		}
		else
		{
			if( PlayerPrefs.GetInt("NoAd") == 1 )
			{
			
			}
			else
			{
				Camera.main.GetComponent<GoogleMobileAdsDemoScript>().ShowInterstitial();
				Camera.main.GetComponent<GoogleMobileAdsDemoScript>().RequestInterstitial();
			}
		}
		
		for (int w = 0; w < Wall.transform.childCount; w++) 
		{
			Wall.transform.GetChild (w).GetComponent<PolygonCollider2D> ().enabled = true;
		}

		player.transform.GetComponent<CircleCollider2D> ().enabled = true;

		PlayerPrefs.SetInt ("ShowAds", 1);

		GameOverPanel.SetActive (true);
		LevelCompletePanel.SetActive (false);
		GameContinuePopup.SetActive(false);

		GOScoreTxt.text = score.ToString ();

		CanvasRef.transform.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
		// GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "GameOver", score);

		if(PlayerPrefs.GetString ("isNormalGameRunning")=="false")
		{
			if (score > PlayerPrefs.GetInt ("HighScore"))
			{
				PlayerPrefs.SetInt("HighScore", score);
				HighScoreTxt.text = PlayerPrefs.GetInt ("HighScore").ToString ();
			}

			else
			{
				HighScoreTxt.text = PlayerPrefs.GetInt ("HighScore").ToString ();
			}
		}

		else if(PlayerPrefs.GetString ("isTimeBaseGameRunnig")=="false")
		{
			if (score > PlayerPrefs.GetInt ("TimeBaseModeHighScore"))
			{
				PlayerPrefs.SetInt("TimeBaseModeHighScore", score);
				HighScoreTxt.text = PlayerPrefs.GetInt ("TimeBaseModeHighScore").ToString ();
			}

			else
			{
				HighScoreTxt.text = PlayerPrefs.GetInt ("TimeBaseModeHighScore").ToString ();
			}

			foreach (GameObject wGo in _gTimeGamePlay.ShuriClone) 
			{
				Destroy (wGo.gameObject);
			}
		}
		// Time.timeScale = 0;
	}

	public void OnGameOverScreen_easyLevel()
	{
		
		for (int w = 0; w < Wall.transform.childCount; w++) 
		{
			Wall.transform.GetChild (w).GetComponent<PolygonCollider2D> ().enabled = true;
		}

		player.transform.GetComponent<CircleCollider2D> ().enabled = true;

		PlayerPrefs.SetInt ("ShowAds", 1);

		GameOverPanel.SetActive (true);
		LevelCompletePanel.SetActive (false);

		GOScoreTxt.text = score.ToString ();

		CanvasRef.transform.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
		// GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "GameOver", score);

		if(PlayerPrefs.GetString ("isNormalGameRunning")=="false")
		{
			if (score > PlayerPrefs.GetInt ("HighScore"))
			{
				PlayerPrefs.SetInt("HighScore", score);
				HighScoreTxt.text = PlayerPrefs.GetInt ("HighScore").ToString ();
			}

			else
			{
				HighScoreTxt.text = PlayerPrefs.GetInt ("HighScore").ToString ();
			}
		}

		else if(PlayerPrefs.GetString ("isTimeBaseGameRunnig")=="false")
		{
			if (score > PlayerPrefs.GetInt ("TimeBaseModeHighScore"))
			{
				PlayerPrefs.SetInt("TimeBaseModeHighScore", score);
				HighScoreTxt.text = PlayerPrefs.GetInt ("TimeBaseModeHighScore").ToString ();
			}

			else
			{
				HighScoreTxt.text = PlayerPrefs.GetInt ("TimeBaseModeHighScore").ToString ();
			}

			foreach (GameObject wGo in _gTimeGamePlay.ShuriClone) 
			{
				Destroy (wGo.gameObject);
			}
		}
		// Time.timeScale = 0;
	}

	void GameOverPopup()
	{
		for (int w = 0; w < Wall.transform.childCount; w++) 
		{
			Wall.transform.GetChild (w).GetComponent<PolygonCollider2D> ().enabled = true;
		}

		player.transform.GetComponent<CircleCollider2D> ().enabled = true;

		PlayerPrefs.SetInt ("ShowAds", 1);
		GO_Score_Text.text = ScoreText.text;
		GO_Powerup_count.text = Powerup_count.ToString();

		GameContinuePopup.SetActive(true);
		player.SetActive(false);
		ScorePanel.SetActive(false);
		Wall_obj.SetActive(false);
		GO_Player_image.sprite = Go_PlayerSprites[PlayerPrefs.GetInt("SelectedCharacter")];
		// CanvasRef.transform.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
	}

	public void ScoreCount(int scoreValue) // Current Score Update.......
	{
		score = PlayerPrefs.GetInt ("LevelUpScore") + scoreValue;
		PlayerPrefs.SetInt ("LevelUpScore",score);
		ScoreText.text = score.ToString ();
		Hwall.OnScoreUpdated (score);

		if (score % 1 == 0)
		{
			if(PlayerPrefs.GetString ("isNormalGameRunning") == "true")
			{
				LevelFillBar.transform.GetComponent<Image> ().fillAmount += 0.05f;			//0.05f
			}

			else 
			{
			}
		}
	}

	void WallDifficulties() // Wall Difficulties As Per Level Up.....
	{
		int tempWallNo = 0;

		do
		{
			tempWallNo = Random.Range (0,Wall.transform.childCount);
		}
		while(Wall.transform.GetChild (tempWallNo).gameObject.activeSelf == false);

		Wall.transform.GetChild (tempWallNo).gameObject.SetActive(false);
	}

	public void LevelChange() // New Level On....
	{
		if (PlayerPrefs.GetFloat ("OnBonusLevel")==1) 
		{
			Debug.LogError("Bonus level...");
			LevelFillBar.transform.GetComponent<Image> ().fillAmount = 1;
			LevelFillBar.transform.GetComponent<Image>().fillOrigin = 1;
			OnBonusLevel ();
			// fillbarParent.SetActive(true);
			// Powerup_parent.SetActive(true);
			CurrentLevel_normal.enabled = true;			
			NextLevel_normal.enabled = true;
			CurrentLevel_timer.enabled = false;
			NextLevel_timer.enabled = false;

			Currentimg_normal.enabled = true;
			Nextimg_normal.enabled = true;
			Currentimg_timer.enabled = false;
			Nextimg_timer.enabled = false;
			CurrentLevel_normal.text = PlayerPrefs.GetFloat("CurrentLevel").ToString();
			float nextLevel_no = PlayerPrefs.GetFloat("CurrentLevel") + 1;
			NextLevel_normal.text = nextLevel_no.ToString();

			for (int a = 0; a < Wall.transform.childCount-1; a++)
			{
				Wall.transform.GetChild (a).gameObject.SetActive(true);
			}

			if (PlayerPrefs.GetFloat ("CurrentLevel") >= 1  && PlayerPrefs.GetFloat ("CurrentLevel") <= 10)
			{
				for (int a = 0; a < 5; a++) 			//3
				{
					WallDifficulties ();
				}
			}

			else if (PlayerPrefs.GetFloat ("CurrentLevel") > 21)
			{
				for (int a = 0; a < 5; a++) 
				{
					WallDifficulties ();
				}
			}

			else if (PlayerPrefs.GetFloat ("CurrentLevel") >= 11  && PlayerPrefs.GetFloat ("CurrentLevel") <= 21)
			{
				for (int a = 0; a < 4; a++) 
				{
					WallDifficulties ();
				}
			}

			else 
			{
			}
		}

		else
		{
			if(PlayerPrefs.GetString ("isNormalGameRunning") == "true")
			{
				Debug.LogError("Normal Mode...");
				LevelFillBar.transform.GetComponent<Image>().fillOrigin = 0;
				CurrentLevel_normal.enabled = true;
				NextLevel_normal.enabled = true;
				CurrentLevel_timer.enabled = false;
				NextLevel_timer.enabled = false;

				Currentimg_normal.enabled = true;
				Nextimg_normal.enabled = true;
				Currentimg_timer.enabled = false;
				Nextimg_timer.enabled = false;
				CurrentLevel_normal.text = PlayerPrefs.GetFloat("CurrentLevel").ToString();
				float nextLevel_no = PlayerPrefs.GetFloat("CurrentLevel") + 1;
				NextLevel_normal.text = nextLevel_no.ToString();
				LevelFillBar.transform.GetComponent<Image> ().fillAmount = 0f;
				LevelText.text = PlayerPrefs.GetFloat ("CurrentLevel").ToString ();

				isTimeOver = false;

				for (int i = 0; i <= Wall.transform.childCount - 1; i++) 
				{
					Wall.transform.GetChild (i).gameObject.SetActive (true);
				}

				currentLevelNo = PlayerPrefs.GetFloat ("CurrentLevel") / 5f + 1;
				int LevelNo = Mathf.CeilToInt (currentLevelNo);

				if (PlayerPrefs.GetFloat ("CurrentLevel") <= 10 && PlayerPrefs.GetFloat ("CurrentLevel") >= 1)
				{
					for (int a = 0; a < 3; a++) 
					{
						WallDifficulties ();
					}
				}

				else if (PlayerPrefs.GetFloat ("CurrentLevel") >= 21)
				{
					for (int a = 0; a < 5; a++) 
					{
						WallDifficulties ();
					}
				}

				else if (PlayerPrefs.GetFloat ("CurrentLevel") >= 11)
				{
					for (int a = 0; a < 4; a++) 
					{
						WallDifficulties ();
					}
				}

				else 
				{
					for (int a = 0; a < 3; a++)
					{
						WallDifficulties ();
					}
				}
			}

			else
			{
				Debug.LogError("TImer Mode...");
				LevelFillBar.transform.GetComponent<Image> ().fillAmount = 1f;
				isTimeOver = false;
				CurrentLevel_normal.enabled = false;
				NextLevel_normal.enabled = false;
				CurrentLevel_timer.enabled = true;
				NextLevel_timer.enabled = true;

				Currentimg_normal.enabled = false;
				Nextimg_normal.enabled = false;
				Currentimg_timer.enabled = true;
				Nextimg_timer.enabled = true;
				LevelText.text = PlayerPrefs.GetFloat ("TimeCurrentLevel").ToString ();
				CurrentLevel_timer.text = PlayerPrefs.GetFloat("TimeCurrentLevel").ToString();
				float nextLevel_no = PlayerPrefs.GetFloat("TimeCurrentLevel") + 1;
				NextLevel_timer.text = nextLevel_no.ToString();

				for (int i = 0; i <= Wall.transform.childCount - 1; i++) 
				{
					Wall.transform.GetChild (i).gameObject.SetActive (true);
					Wall.transform.GetChild (i).GetComponent<SpriteRenderer> ().enabled = true;

					for (int j = 0; j <= Wall.transform.GetChild (i).childCount - 1; j++) 
					{
						Wall.transform.GetChild (i).GetChild(j).gameObject.SetActive (false);
					}
				}

				currentLevelNo = PlayerPrefs.GetFloat ("TimeCurrentLevel") / 5f + 1;
				int LevelNo = Mathf.CeilToInt (currentLevelNo);

				if (PlayerPrefs.GetFloat ("TimeCurrentLevel") < 11 && PlayerPrefs.GetFloat ("TimeCurrentLevel") >= 1)
				{
					for (int a = 0; a < 3; a++) 
					{
						WallDifficulties ();
					}
				}

				else if (PlayerPrefs.GetFloat ("TimeCurrentLevel") > 20)
				{
					for (int a = 0; a < 1; a++) 
					{
						WallDifficulties ();
					}
				}

				else 
				{
					for (int a = 0; a < 2; a++)
					{
						WallDifficulties ();
					}
				}
			}
		}
	}

	void oBonusLevelCatch() // Eye Animation In Bonus Level.......
	{
		if (c == 0) 
		{
			player.transform.GetChild (0).transform.GetComponent<Animation> ().Play ("RightAnim");
			c = 1;
		}

		else if (c == 1) 
		{
			player.transform.GetChild (0).transform.GetComponent<Animation> ().Play ("LeftAnim");
			c = 0;
		}
		else
		{

		}
	}

	void EyeAnimStart()
	{
		player.transform.GetChild (0).gameObject.SetActive (false);
		player.transform.GetChild (3).gameObject.SetActive (true);
		player.transform.GetChild (4).gameObject.SetActive (true);
		player.transform.GetChild (3).transform.GetComponent<Animation> ().Play ("RightEyeHappy");
		player.transform.GetChild (4).transform.GetComponent<Animation> ().Play ("LeftEyeHappy");

		Invoke ("EyeAnimStop", 0.8f);
	}

	void EyeAnimStop()
	{
		player.transform.GetChild (3).gameObject.SetActive (false);
		player.transform.GetChild (4).gameObject.SetActive (false);
		player.transform.GetChild (0).gameObject.SetActive (true);
	}

	public void OnBonusLevel()
	{
		if( ScorePanel.active == true )
		{
			BonusLevelText.SetActive (true);
			BonusLevelMsg.SetActive (true);

			ScorePanel.transform.GetChild(0).gameObject.SetActive(false);
			ScorePanel.transform.GetChild(1).gameObject.SetActive(false);
			ScorePanel.transform.GetChild(2).gameObject.SetActive(false);
			ScorePanel.transform.GetChild(3).gameObject.SetActive(false);

			Invoke ("BonusLevelMsgDisable",3f);
			InvokeRepeating ("oBonusLevelCatch",1f , 2f); // Eye Animation........
			InvokeRepeating ("CollectibelOnBonusLevel" , 1f , 1.5f);
		}		
	}

	void BonusLevelMsgDisable()
	{
		BonusLevelMsg.SetActive (false);
	}

	void BonusLevelTextDisable()
	{
		BonusLevelText.SetActive (false);
	}

	void CollectibelOnBonusLevel()
	{
		if (PlayerPrefs.GetString ("isNormalGameRunning") == "true") 
		{
			int coleectibleNo = Random.Range (0, CollectiblePH.Count);

			CollectibleClone = Instantiate (PowerUp [0], CollectiblePos.transform.GetChild (coleectibleNo).transform.localPosition, Quaternion.identity);
			CollectibleClone.transform.localPosition = CollectiblePos.transform.GetChild (coleectibleNo).transform.localPosition;

			CollectibleClone.transform.tag = "PowerUp";

			BonusCollectibleStars.Add (CollectibleClone);
			CollectibleClone.AddComponent<PowerUp> ();
		}

		else if (PlayerPrefs.GetString ("isTimeBaseGameRunnig") == "true") 
		{
			int coleectibleNo = Random.Range (0, CollectiblePH.Count);

			CollectibleClone = Instantiate (PowerUp [0],_gTimeGamePlay.collectiblesClonePos.transform.GetChild (coleectibleNo).transform.localPosition, Quaternion.identity);
			CollectibleClone.transform.localPosition = _gTimeGamePlay.collectiblesClonePos.transform.GetChild (coleectibleNo).transform.localPosition;

			CollectibleClone.transform.tag = "PowerUp";

			_gTimeGamePlay.ShuriClone.Add (CollectibleClone);
			CollectibleClone.AddComponent<PowerUp> ();
		}
	}

	void OnCancleBonusLevel()
	{
		CancelInvoke ("CollectibelOnBonusLevel");
	}

	public void OnGameOver()
	{
		isGameOver = true;
	}

	public void CollectibleCount(int powerUpCount)
	{
		powerUpNo = PlayerPrefs.GetInt ("PowerUpCollect") + powerUpCount;
		PlayerPrefs.SetInt ("PowerUpCollect",powerUpNo);
		PowerUpTxt.text = powerUpNo.ToString ();
	}

	public void PowerupCount_level(int powerUpCount)
	{
		Powerup_count = Powerup_count + powerUpCount;
		// PowerUpTxt.text = Powerup_count.ToString();
		_CurrentPowerUp_count.text = Powerup_count.ToString();
	}

	public void OnCollectibleGenerate()
	{
		StopGeneratedEnimy ();

		if (PlayerPrefs.GetFloat ("CurrentLevel") >= 15) {
			Invoke ("StartGeneratingEnemy", 2f);
		} else if (PlayerPrefs.GetFloat ("CurrentLevel") >= 5) {
			Invoke ("StartGeneratingEnemy", 1f);
		} else {
			Invoke ("StartGeneratingEnemy", 1f);
		}

		int coleectibleNo = Random.Range (0,CollectiblePH.Count);
	
		CollectibleClone = Instantiate (PowerUp[0] , CollectiblePos.transform.GetChild(coleectibleNo).transform.localPosition,Quaternion.identity);
		CollectibleClone.transform.localPosition = CollectiblePos.transform.GetChild (coleectibleNo).transform.localPosition;

		CollectibleClone.transform.tag = "PowerUp";

		GeneratedEnimyClone.Add (CollectibleClone);
		CollectibleClone.AddComponent<PowerUp> ();
	}

	public void CollectibleStart()
	{
		InvokeRepeating ("OnCollectibleGenerate",3f,9f);
	}

	public void StopCollectible()
	{
		// Debug.LogError("Stop Collectibles.....");
		CancelInvoke ("OnCollectibleGenerate");
	}

	public void GenerateEnemy()
	{
		int TempPH;

		if (PlayerPrefs.GetFloat ("CurrentLevel") >= 21)
		{
			TempPH = Random.Range (17, 27);
		} 
		else
		{
			TempPH = Random.Range (0, PhEnimy.Count);
		}
			

		EnimyClone = Instantiate (Weapon[0] , EnimyPos.transform.GetChild(TempPH).transform.localPosition,Quaternion.identity);
		EnimyClone.transform.localPosition = EnimyPos.transform.GetChild (TempPH).transform.localPosition;

		GeneratedEnimyClone.Add (EnimyClone);
		EnimyClone.AddComponent<Enimy> ();

		if ((TempPH ==0 || TempPH ==1||TempPH ==2||TempPH ==3||TempPH ==4||TempPH ==5||TempPH ==12))// || (Vector3.Distance (player.transform.position, EnimyClone.transform.position) < 250f))
		{
			player.transform.GetChild (0).transform.GetComponent<Animation> ().Play ("LeftAnim");  //NinjaEye [0].transform.GetComponent<Animation> ().Play ("LeftAnim");
		}

		if ((TempPH ==6 || TempPH ==7||TempPH ==8||TempPH ==9||TempPH ==10||TempPH ==11||TempPH ==13))// || (Vector3.Distance (player.transform.position, EnimyClone.transform.position) < 250f)) 
		{
			player.transform.GetChild (0).transform.GetComponent<Animation> ().Play ("RightAnim"); // NinjaEye [0].transform.GetComponent<Animation> ().Play ("RightAnim");
		}

		else
		{
		}
	}

	public void StartGeneratingEnemy()
	{
		InvokeRepeating ("GenerateEnemy",0.5f,PlayerPrefs.GetFloat ("EnimyGenerateTime"));
	}

	public void StopGeneratedEnimy()
	{
		CancelInvoke ("GenerateEnemy");
	}

	public void OnKunaiWeapon()
	{
		kunaiWeaponClone = Instantiate (Weapon[1] , kuniaPos.transform.GetChild(0).transform.localPosition,Quaternion.identity);
		kunaiWeaponClone.transform.localPosition = kuniaPos.transform.GetChild (0).transform.localPosition;
		kunaiWeaponClone.transform.localRotation = kuniaPos.transform.GetChild (0).transform.localRotation;

		GeneratedEnimyClone.Add (kunaiWeaponClone);
		kunaiWeaponClone.AddComponent<Enimy> ();
	}

	public void KunaiWeaponCall()
	{
		InvokeRepeating ("OnKunaiWeapon",1f,PlayerPrefs.GetFloat ("GenKunaiWeaponTime"));
	}

	public void StopKunaiWeaponCall()
	{
		CancelInvoke ("OnKunaiWeapon");
	}

	public void UpOnKunaiWeapon()
	{
		int kuniaPH = Random.Range (0,kuniaPhList.Count);

		kuniaPrevvalue = kuniaPH;
		UpkunaiWeaponClone = Instantiate (Weapon[1] , upkuniaPos.transform.GetChild(kuniaPH).transform.localPosition,Quaternion.identity);
		UpkunaiWeaponClone.transform.localPosition = upkuniaPos.transform.GetChild (kuniaPH).transform.localPosition;
		UpkunaiWeaponClone.transform.localRotation = upkuniaPos.transform.GetChild (kuniaPH).transform.localRotation;

		GeneratedEnimyClone.Add (UpkunaiWeaponClone);
		UpkunaiWeaponClone.AddComponent<Enimy> ();
	}

	public void UpKunaiWeaponCall()
	{
		InvokeRepeating ("UpOnKunaiWeapon",1f,PlayerPrefs.GetFloat ("GenUpKunaiWeaponTime"));
	}

	public void StopuPKunaiWeaponCall()
	{
		CancelInvoke ("UpOnKunaiWeapon");
	}

	public void OnSpikeWeapon()
	{
		int spikePH = Random.Range (0,spikePhList.Count);
	
		spikeClone = Instantiate (Weapon[2] , spikePos.transform.GetChild(spikePH).transform.localPosition,Quaternion.identity);
		spikeClone.transform.localPosition = spikePos.transform.GetChild (spikePH).transform.localPosition;
		spikeClone.transform.localRotation = spikePos.transform.GetChild (spikePH).transform.localRotation;

		GeneratedEnimyClone.Add (spikeClone);

		if (spikePH == 0 || spikePH == 2)
		{
			spikeClone.transform.GetChild (0).transform.localRotation = Quaternion.Euler (0,0,-5);
		}

		for (int a = 0; a <= spikeClone.transform.childCount-1; a++) 
		{
			spikeClone.transform.GetChild(a).gameObject.AddComponent<SpikeWeopen> ();
		}
	}

	public void SpikeWeaponCall()
	{
		InvokeRepeating ("OnSpikeWeapon",1f,PlayerPrefs.GetFloat ("GenSpikeWeaponTime"));
	}

	public void StopSpikeWeaponCall()
	{
		CancelInvoke ("OnSpikeWeapon");
	}

	public void OnPatternWeapon()
	{
		patternClone = Instantiate (Weapon[3] , patternPos.transform.GetChild(0).transform.localPosition,Quaternion.identity);
		patternClone.transform.localPosition = patternPos.transform.GetChild (0).transform.localPosition;
		patternClone.transform.localRotation = patternPos.transform.GetChild (0).transform.localRotation;

		GeneratedEnimyClone.Add (patternClone);
	}

	public void OnRightPatternWeapon()
	{
		patternRightClone = Instantiate (Weapon[4] , patternPos.transform.GetChild(1).transform.localPosition,Quaternion.identity);
		patternRightClone.transform.localPosition = patternPos.transform.GetChild (1).transform.localPosition;
		patternRightClone.transform.localRotation = patternPos.transform.GetChild (1).transform.localRotation;

		GeneratedEnimyClone.Add (patternRightClone);
	}

	public void OnLeftPatternWeapon()
	{
		patternLeftClone = Instantiate (Weapon[5] , patternPos.transform.GetChild(2).transform.localPosition,Quaternion.identity);
		patternLeftClone.transform.localPosition = patternPos.transform.GetChild (2).transform.localPosition;
		patternLeftClone.transform.localRotation = patternPos.transform.GetChild (2).transform.localRotation;

		GeneratedEnimyClone.Add (patternLeftClone);
	}

	public void PatternWeaponCall()
	{
		int randomPattern = Random.Range (0,4);

		if (randomPattern == 0)
			InvokeRepeating ("OnLeftPatternWeapon", 2f, 5f);
		else if (randomPattern == 1)
			InvokeRepeating ("OnPatternWeapon", 2f, 5f);	//,PlayerPrefs.GetFloat ("GenPatternWeaponTime"));
		else if (randomPattern == 3)
			InvokeRepeating ("OnRightPatternWeapon", 2f, 5f);
		else if (randomPattern == 2)
			InvokeRepeating ("OnLeftPatternWeapon", 2f, 5f);
		else if (randomPattern == 4)
			InvokeRepeating ("OnRightPatternWeapon", 2f, 5f);
		else
			Debug.Log ("1");
	}

	public void OnPatternCancle()
	{
		CancelInvoke ("OnPatternWeapon");
		CancelInvoke ("OnLeftPatternWeapon");
		CancelInvoke ("OnRightPatternWeapon");
	}

	public void StopEnemyAttack()
	{
//		if (PlayerPrefs.GetFloat ("OnBonusLevel") == 0) 
//		{
//			Debug.LogError ("In Collectibles...!"+PlayerPrefs.GetFloat ("OnBonusLevel"));
			CancelInvoke ("OnCollectibleGenerate");
			StopCollectible ();
		//}

		CancelInvoke ("GenerateEnemy");

		foreach (GameObject g in GeneratedEnimyClone) 
		{
			Destroy (g);
		}
	}

	public void OnGamePlay()
	{
		// if (isGamePause)
		// {
		// 	Time.timeScale = 0;
		// 	isGamePause = true;
		// }

		// else
		// {
		// 	Time.timeScale = 1;
		// 	isGamePause = false;
		// 	// Wall.SetActive(true);
		// 	// player.SetActive(true);
		// }
		Time.timeScale = 1;
		PausePanel.SetActive(false);
	}

	public void NormalLevel()
	{
		for (int w = 0; w < SideBorderClone.Count; w++) 
		{
//			SideBorderClone [2].gameObject.SetActive (false);
//			SideBorderClone [3].gameObject.SetActive (false);
			SideBorderClone [w].gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		}

		PlayerPrefs.SetString ("isNormalGameRunning","true");
		PlayerPrefs.SetString ("isTimeBaseGameRunnig","false");
		// Application.LoadLevel (0);
		PlayerPrefs.SetInt("GamePlay",1);
		SceneManager.LoadScene(0);
	}

	public void OnTimeBaseLevel()
	{
		for (int w = 0; w < SideBorderClone.Count; w++) 
		{
			//SideBorderClone [w].gameObject.SetActive (true);
			SideBorderClone [w].gameObject.GetComponent<BoxCollider2D> ().enabled = true;
		}

		PlayerPrefs.SetString ("isNormalGameRunning","false");
		PlayerPrefs.SetString ("isTimeBaseGameRunnig","true");
		// Application.LoadLevel (0);
		PlayerPrefs.SetInt("GamePlay",1);
		SceneManager.LoadScene(0);
	}

	public void OnModeSelection()
	{
		if (PlayerPrefs.GetString ("isNormalGameRunning") == "true")
		{
			NormalModeSelect.SetActive (true);
			TimerModeSelect.SetActive (false);
		}

		else
		{
			NormalModeSelect.SetActive (false);
			TimerModeSelect.SetActive (true);
		}

		CanvasRef.transform.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
	}

	public void OnCharacterSelection()
	{
		SelectCharStar.transform.GetComponent<Text> ().text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();

		_gCharSelection.AlreadyUnlockedChar ();
		CanvasRef.transform.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
	}


	public void BackFormOtherScreen()
	{
		CanvasRef.transform.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
		CanvasRef.transform.GetComponent<Canvas> ().worldCamera = mainCam.GetComponent<Camera>();
	}

	public void LoadCurrentLevel()
	{
		int collectibleCount = PlayerPrefs.GetInt("PowerUpCollect") + Powerup_count;
		PlayerPrefs.SetInt("PowerUpCollect",collectibleCount);
		PowerUpTxt.text = collectibleCount.ToString();
        PlayerPrefs.SetInt("GamePlay",1);
        Time.timeScale = 1;

		if ((PlayerPrefs.GetString ("isNormalGameRunning") == "true")) 
		{
			PlayerPrefs.SetFloat ("AdsInNorCurrentLevel",0f);
		}

		else
		{
			PlayerPrefs.SetFloat ("AdsInTimerCurrentLevel",0f);
		}

		LevelFillBar.transform.GetComponent<Image> ().fillAmount = 0f;
		PlayerPrefs.SetInt ("LevelUpScore",0);
		// Application.LoadLevel (0);
		SceneManager.LoadScene(0);
		if( PlayerPrefs.GetInt("NoAd") == 1 )
		{

		}
		else
		{
			Camera.main.GetComponent<GoogleMobileAdsDemoScript>().ShowInterstitial();
			Camera.main.GetComponent<GoogleMobileAdsDemoScript>().RequestInterstitial();
		}        
	}

	public void OnSoundOnOff()
	{
		if (PlayerPrefs.GetInt ("SoundOn") == 0) 
		{
			PlayerPrefs.SetInt ("SoundOn", 1);
			MusicBtn.transform.GetComponent<Image> ().sprite = MusicBtnList [1];
			SoundHandle.transform.GetComponent<AudioSource> ().Stop ();
		}
		else
		{
			MusicBtn.transform.GetComponent<Image> ().sprite = MusicBtnList [0];
			SoundHandle.transform.GetComponent<AudioSource> ().Play ();
			PlayerPrefs.SetInt ("SoundOn", 0);
		}
	}

	void CheckMusicInStart()
	{
		if (PlayerPrefs.GetInt ("SoundOn") == 1) 
		{
			PlayerPrefs.SetInt ("SoundOn", 1);
			MusicBtn.transform.GetComponent<Image> ().sprite = MusicBtnList [1];
			SoundHandle.transform.GetComponent<AudioSource> ().Stop ();
		}

		else
		{
			MusicBtn.transform.GetComponent<Image> ().sprite = MusicBtnList [0];
			SoundHandle.transform.GetComponent<AudioSource> ().Play ();
			PlayerPrefs.SetInt ("SoundOn", 0);
		}
	}

	public void OnReset()
	{
		PlayerPrefs.DeleteAll ();
		// Application.LoadLevel (0);
		SceneManager.LoadScene(0);
	}

	void OnApplicationPause()
	{
		Debug.LogError("_applicationPause...." + _applicationPause);
		if(_applicationPause)
		{
			// Debug.LogError("inside Application pause...");
			PausePanel.SetActive(true);
			// CancelInvoke ("GenerateEnemy");
			// StopGeneratedEnimy();
			// CancelInvoke("StopKunaiWeaponCall");
			// StopKunaiWeaponCall();
			// CancelInvoke ("UpOnKunaiWeapon");
			// StopuPKunaiWeaponCall();
			// CancelInvoke ("OnSpikeWeapon");
			// StopSpikeWeaponCall();
			// CancelInvoke ("OnPatternWeapon");
			// CancelInvoke ("OnLeftPatternWeapon");
			// CancelInvoke ("OnRightPatternWeapon");
			// OnPatternCancle();		
			Time.timeScale = 0;
		}
	}

	public void PauseGame()
	{
		Debug.LogError("_applicationPause....." + _applicationPause);
		if(_applicationPause)
		{
			Debug.LogError("inside Application pause...");
			PausePanel.SetActive(true);
			CancelInvoke ("GenerateEnemy");
			StopGeneratedEnimy();
			CancelInvoke("StopKunaiWeaponCall");
			StopKunaiWeaponCall();
			CancelInvoke ("UpOnKunaiWeapon");
			StopuPKunaiWeaponCall();
			CancelInvoke ("OnSpikeWeapon");
			StopSpikeWeaponCall();
			CancelInvoke ("OnPatternWeapon");
			CancelInvoke ("OnLeftPatternWeapon");
			CancelInvoke ("OnRightPatternWeapon");
			OnPatternCancle();		
			Time.timeScale = 0;
			// Wall.SetActive(false);
			// player.SetActive(false);
		}
	}

	//  public void ShowAdPlacement()
    // {
    //     if(Application.internetReachability == NetworkReachability.NotReachable)
    //     {
    //         Debug.Log("Net not available");
	// 		GameControllerScript _gcScript = FindObjectOfType<GameControllerScript>();
    //         _gcScript.InternetPanel.SetActive(true);
    //         StartCoroutine(InternetPanel_resume());
    //         Debug.Log("Startcoroutine is called....");
    //     }
    //     else
    //     {
    //         Camera.main.GetComponent<GoogleMobileAdsDemoScript>().ShowRewardedAd();
    //         Camera.main.GetComponent<GoogleMobileAdsDemoScript>().CreateAndLoadRewardedAd();
    //     }
    // }

	//  IEnumerator InternetPanel_resume(/*float delay*/)
	// {
	// 	GameControllerScript _gcScript = FindObjectOfType<GameControllerScript>();
	// 	yield return new WaitForSeconds(1.0f);
	// 	Debug.Log("Internet panel above...");
	// 	_gcScript.InternetPanel.SetActive(false);
	// 	Debug.Log("Internet panel below...");
	// }

}