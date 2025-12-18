using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wall : MonoBehaviour 
{
	int score;
	public GameObject BG_img , Player;
	public List<Color> BGColors = new List<Color> ();
	public List<Sprite> BGSprite = new List<Sprite>();
	Gameplay _wGameplay;
	SoundControl _wSoundControl;
	TimeGameplay _wTimeGameplay;
	GameControllerScript _wGamecontrol;
	GoogleMobileAdsDemoScript _wGoogleAds;
	public GameObject BackGround;
	Vector3 BgCurrentPos;
	static int colorId;
	public int perValue , adsCount, currentRunningLevel;
	float total_timer;
	public bool TotalClaim_stars;

	void Start () 
	{		
		total_timer = 0;
		TotalClaim_stars = false;
		// OnBackGroundChange ();
		BgCurrentPos = BackGround.transform.position;
		_wGameplay = FindObjectOfType<Gameplay> ();
		_wTimeGameplay = FindObjectOfType<TimeGameplay> ();
		_wSoundControl = FindObjectOfType<SoundControl>();
		_wGamecontrol = FindObjectOfType<GameControllerScript>();
		_wGoogleAds = FindObjectOfType<GoogleMobileAdsDemoScript>();
		_wGoogleAds.ClaimReward_star.SetActive(false);
	}
	
	void Update () 
	{
//		if(_wGameplay.isGameStart == false && _wGameplay.isGameOver == false)
//		transform.Rotate (0,0,10f);
		if(_wGameplay.LevelUpPanel.active)
		{
			_wGameplay.StopEnemyAttack();
			_wGameplay.StopGeneratedEnimy ();
			_wGameplay.StopKunaiWeaponCall ();
			_wGameplay.StopCollectible();
			_wGameplay.StopuPKunaiWeaponCall ();
			_wGameplay.StopSpikeWeaponCall ();
			_wGameplay.OnPatternCancle ();
		}
		if(TotalClaim_stars)
		{
			DisplayTotalStarLerp(_wGoogleAds.TotalReward_star);
		}
	}

	public void OnTimerGameLevelUp()
	{
		if (PlayerPrefs.GetString ("isTimeBaseGameRunnig") == "true") 
		{
			if (Gameplay.isTimeBaseFillbarOver == true) 
			{
				// Gameplay.isTimeBaseFillbarOver = false;
				_wTimeGameplay.OnStopTimeCollectible ();
				if (PlayerPrefs.GetInt ("SoundOn") == 0)
				{
					_wGameplay.CanvasRef.transform.GetComponent<AudioSource> ().clip = _wSoundControl.Music [9] as AudioClip;
					_wGameplay.CanvasRef.GetComponent<AudioSource> ().Play ();
					_wGameplay.SoundHandle.transform.GetComponent<AudioSource> ().Stop();
				}
				_wGamecontrol._LevelUp_continue.interactable = true;
				_wGameplay.LevelUpPanel.SetActive(true);
				_wGameplay.LC_Player_image.sprite = _wGameplay.Playersprites[PlayerPrefs.GetInt("SelectedCharacter")];
				_wGameplay.ScorePanel.SetActive(false);
				_wGameplay.LC_Score_Text.text = _wGameplay.ScoreText.text;
				_wGameplay.player.SetActive(false);
				// _wGameplay.Wall_obj.SetActive(false);
				for( int i = 0; i < _wGameplay.Wall_obj.transform.childCount; i++ )
				{
					_wGameplay.Wall_obj.gameObject.transform.GetChild(i).gameObject.SetActive(false);
				}
				_wGameplay.LevelPanel_Anim.SetActive(true);
				StartCoroutine(Level_Subpanel());
				_wGameplay._CurrentPowerUp_count.enabled = false;
                if( _wGameplay.Powerup_count == 0)
                {
                    _wGamecontrol._claimBtn.interactable = false;
		_wGoogleAds.ClaimReward_star.SetActive(false);
                }
                else
                {
                    _wGamecontrol._claimBtn.interactable = true;
					_wGoogleAds.ClaimReward_star.SetActive(false);
                }
				// _wGoogleAds.ClaimReward_star.SetActive(true);
				// _wGameplay.TotalStar_collected.text = _wGameplay.PowerUpTxt.text;
				_wGameplay.TotalStar_collected.text = _wGameplay.Powerup_count.ToString();
				_wGameplay.CurrentLevel_star.text = _wGameplay.Powerup_count.ToString();

// 				Instantiate (_wGameplay.LevelUpPaticle, Player.transform.position, Quaternion.identity);
				Instantiate(_wGameplay.VictoryParticle_1, new Vector3(_wGameplay.VictoryParticle_Pos1.transform.position.x, _wGameplay.VictoryParticle_Pos1.transform.position.y, _wGameplay.VictoryParticle_Pos1.transform.position.z), Quaternion.Euler(_wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.x, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.y, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.z) );
								

// 				// if(PlayerPrefs.GetString ("SelectedPlayerName") == "NinjaPlayer" || PlayerPrefs.GetString("SelectedPlayerName") == "FrogPlayer" || PlayerPrefs.GetString("SelectedPlayerName") == "LionPlayer" || PlayerPrefs.GetString("SelectedPlayerName") == "MonkeyPlayer" || PlayerPrefs.GetString("SelectedPlayerName") == "TigerPlayer")
// 				// {
// 				// 	GameObject.Find(PlayerPrefs.GetString ("SelectedPlayerName")).transform.GetComponent<Animation>().Play("FrogAnimation");
// 				// }
// 				// else if( PlayerPrefs.GetString ("SelectedPlayerName") == "DogPlayer" || PlayerPrefs.GetString ("SelectedPlayerName") == "PigPlayer" || PlayerPrefs.GetString ("SelectedPlayerName") == "SheepPlayer")
// 				// {
// 				// 	GameObject.Find(PlayerPrefs.GetString ("SelectedPlayerName")).transform.GetComponent<Animation>().Play("DogAnimation");
// 				// }
// 				// else if( PlayerPrefs.GetString ("SelectedPlayerName") == "BearPlayer" )
// 				// {
// 				// 	GameObject.Find(PlayerPrefs.GetString ("SelectedPlayerName")).transform.GetComponent<Animation>().Play("BearAnimation");
// 				// }
// 				// else
// 				// {
// 				// 	GameObject.Find(PlayerPrefs.GetString ("SelectedPlayerName")).transform.GetComponent<Animation>().Play("CowAnimation");
// 				// }

// 				// transform.GetComponent<Animation> ().Play ();
// 				_wGameplay.isLevelContinue = true;

// 				OnBackGroundChange ();

// //				for (int i1 = 0; i1 < _wGameplay.player.transform.childCount; i1++)
// //				{
// 					_wGameplay.player.transform.GetChild (0).gameObject.SetActive (false);
// 					//_wGameplay.player.transform.GetChild (0).GetChild (0).gameObject.SetActive (false);
// 					_wGameplay.player.transform.GetChild (3).gameObject.SetActive (true);
// 					_wGameplay.player.transform.GetChild (4).gameObject.SetActive (true);
// //				}

// 				PlayerPrefs.SetFloat ("TimeCurrentLevel", PlayerPrefs.GetFloat ("TimeCurrentLevel") + 1f);
// 				Invoke ("NotificationEnable", 0.2f);
// 				Invoke ("NotificationDisable", 3.0f);			//2.5
// 				Invoke("LevelChangedAnim",4.0f);
				
// 				// LevelChangedAnim ();

				_wTimeGameplay.DesEnemyAttack ();
			}
		}
	}

	public void OnScoreUpdated (int removePiece)
	{
		if ((PlayerPrefs.GetString ("isDoubleGameRunnig") == "true") || (PlayerPrefs.GetString ("isNormalGameRunning") == "true")) 
		{
			perValue = 20;			//20
			if (removePiece % perValue == 0)
			{
				Debug.LogError("SoundOn..." + PlayerPrefs.GetInt ("SoundOn"));
				if (PlayerPrefs.GetInt ("SoundOn") == 0)
				{
					_wGameplay.CanvasRef.transform.GetComponent<AudioSource> ().clip = _wSoundControl.Music [9] as AudioClip;
					_wGameplay.CanvasRef.GetComponent<AudioSource> ().Play ();
					_wGameplay.SoundHandle.transform.GetComponent<AudioSource> ().Stop();
				}
				currentRunningLevel = int.Parse (PlayerPrefs.GetFloat ("CurrentLevel").ToString());
				Debug.LogError("CurrentLevel..." + PlayerPrefs.GetFloat("CurrentLevel") + "  current RunningLevel..." + currentRunningLevel % 5);
				PlayerPrefs.SetFloat ("CurrentLevel",PlayerPrefs.GetFloat("CurrentLevel")+1);
				_wGamecontrol._LevelUp_continue.interactable = true;
				_wGameplay.LevelUpPanel.SetActive(true);
				_wGameplay.LC_Player_image.sprite = _wGameplay.Playersprites[PlayerPrefs.GetInt("SelectedCharacter")];
				_wGameplay.LC_Score_Text.text = _wGameplay.ScoreText.text;
				_wGameplay.player.SetActive(false);
				_wGameplay.ScorePanel.SetActive(false);
				for( int i = 0; i < _wGameplay.Wall_obj.transform.childCount; i++ )
				{
					_wGameplay.Wall_obj.gameObject.transform.GetChild(i).gameObject.SetActive(false);
				}
				_wGameplay.LevelPanel_Anim.SetActive(true);
				StartCoroutine(Level_Subpanel());
				_wGameplay._CurrentPowerUp_count.enabled = false;
                if (_wGameplay.Powerup_count == 0)
                {
                    _wGamecontrol._claimBtn.interactable = false;
		_wGoogleAds.ClaimReward_star.SetActive(false);
                }
                else
                {
                    _wGamecontrol._claimBtn.interactable = true;
					_wGoogleAds.ClaimReward_star.SetActive(false);
                }
                //_wGamecontrol._claimBtn.interactable = true;
				// _wGoogleAds.ClaimReward_star.SetActive(true);
				// _wGameplay.TotalStar_collected.text = _wGameplay.PowerUpTxt.text;
				_wGameplay.TotalStar_collected.text = _wGameplay.Powerup_count.ToString();
				_wGameplay.CurrentLevel_star.text = _wGameplay.Powerup_count.ToString();

				// Debug.LogError (PlayerPrefs.GetInt("GotBonusLevel")+"     ___________    "+PlayerPrefs.GetFloat ("CurrentLevel").ToString());

				if( currentRunningLevel >= 5 )
				{
					if (currentRunningLevel%5 == 4 && PlayerPrefs.GetInt("GotBonusLevel")==0)
					{
						Debug.LogError("Next Bonus level...");
						OnBonusStartDelay();
						// Instantiate (_wGameplay.LevelUpPaticle, Player.transform.position, Quaternion.identity);
						Instantiate(_wGameplay.VictoryParticle_1, new Vector3(_wGameplay.VictoryParticle_Pos1.transform.position.x, _wGameplay.VictoryParticle_Pos1.transform.position.y, _wGameplay.VictoryParticle_Pos1.transform.position.z), Quaternion.Euler(_wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.x, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.y, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.z) );
						// // transform.GetComponent<Animation> ().Play ();

						// // if(PlayerPrefs.GetString ("SelectedPlayerName") == "NinjaPlayer" || PlayerPrefs.GetString("SelectedPlayerName") == "FrogPlayer" || PlayerPrefs.GetString("SelectedPlayerName") == "LionPlayer" || PlayerPrefs.GetString("SelectedPlayerName") == "MonkeyPlayer" || PlayerPrefs.GetString("SelectedPlayerName") == "TigerPlayer")
						// // {
						// // 	GameObject.Find(PlayerPrefs.GetString ("SelectedPlayerName")).transform.GetComponent<Animation>().Play("FrogAnimation");
						// // }
						// // else if( PlayerPrefs.GetString ("SelectedPlayerName") == "DogPlayer" || PlayerPrefs.GetString ("SelectedPlayerName") == "PigPlayer" || PlayerPrefs.GetString ("SelectedPlayerName") == "SheepPlayer")
						// // {
						// // 	GameObject.Find(PlayerPrefs.GetString ("SelectedPlayerName")).transform.GetComponent<Animation>().Play("DogAnimation");
						// // }
						// // else if( PlayerPrefs.GetString ("SelectedPlayerName") == "BearPlayer" )
						// // {
						// // 	GameObject.Find(PlayerPrefs.GetString ("SelectedPlayerName")).transform.GetComponent<Animation>().Play("BearAnimation");
						// // }
						// // else
						// // {
						// // 	GameObject.Find(PlayerPrefs.GetString ("SelectedPlayerName")).transform.GetComponent<Animation>().Play("CowAnimation");
						// // }

						// foreach (GameObject wGo in _wGameplay.GeneratedEnimyClone) 
						// {
						// 	Destroy (wGo.gameObject);
						// }

						// PlayerPrefs.SetInt ("BonusLevelConti",1);

						// Invoke ("OnBonusStartDelay",1.5f);
						_wGameplay.StopEnemyAttack();

						_wGameplay.StopKunaiWeaponCall ();
						_wGameplay.StopGeneratedEnimy ();
						_wGameplay.StopCollectible();
						_wGameplay.StopuPKunaiWeaponCall ();
						_wGameplay.StopSpikeWeaponCall ();
						_wGameplay.OnPatternCancle ();
						// OnBackGroundChange ();
					}

					else
					{
	// 					Instantiate (_wGameplay.LevelUpPaticle, Player.transform.position, Quaternion.identity);
						Instantiate(_wGameplay.VictoryParticle_1, new Vector3(_wGameplay.VictoryParticle_Pos1.transform.position.x, _wGameplay.VictoryParticle_Pos1.transform.position.y, _wGameplay.VictoryParticle_Pos1.transform.position.z), Quaternion.Euler(_wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.x, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.y, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.z) );

						_wGameplay.StopEnemyAttack();
						_wGameplay.StopGeneratedEnimy ();
						_wGameplay.StopKunaiWeaponCall ();
						_wGameplay.StopCollectible();
						_wGameplay.StopuPKunaiWeaponCall ();
						_wGameplay.StopSpikeWeaponCall ();
						_wGameplay.OnPatternCancle ();

	// 					// transform.GetComponent<Animation> ().Play ();

	// 					// if(PlayerPrefs.GetString ("SelectedPlayerName") == "NinjaPlayer" || PlayerPrefs.GetString("SelectedPlayerName") == "FrogPlayer" || PlayerPrefs.GetString("SelectedPlayerName") == "LionPlayer" || PlayerPrefs.GetString("SelectedPlayerName") == "MonkeyPlayer" || PlayerPrefs.GetString("SelectedPlayerName") == "TigerPlayer")
	// 					// {
	// 					// 	GameObject.Find(PlayerPrefs.GetString ("SelectedPlayerName")).transform.GetComponent<Animation>().Play("FrogAnimation");
	// 					// }
	// 					// else if( PlayerPrefs.GetString ("SelectedPlayerName") == "DogPlayer" || PlayerPrefs.GetString ("SelectedPlayerName") == "PigPlayer" || PlayerPrefs.GetString ("SelectedPlayerName") == "SheepPlayer")
	// 					// {
	// 					// 	GameObject.Find(PlayerPrefs.GetString ("SelectedPlayerName")).transform.GetComponent<Animation>().Play("DogAnimation");
	// 					// }
	// 					// else if( PlayerPrefs.GetString ("SelectedPlayerName") == "BearPlayer" )
	// 					// {
	// 					// 	GameObject.Find(PlayerPrefs.GetString ("SelectedPlayerName")).transform.GetComponent<Animation>().Play("BearAnimation");
	// 					// }
	// 					// else
	// 					// {
	// 					// 	GameObject.Find(PlayerPrefs.GetString ("SelectedPlayerName")).transform.GetComponent<Animation>().Play("CowAnimation");
	// 					// }					

	// 					_wGameplay.isLevelContinue = true;
	// 					OnBackGroundChange ();

	// //					for(int i1 = 0; i1 < _wGameplay.player.transform.childCount; i1++)
	// //					{

	// 						_wGameplay.player.transform.GetChild (0).gameObject.SetActive (false);
	// 						//_wGameplay.player.transform.GetChild (0).GetChild (0).gameObject.SetActive (false);
	// 						_wGameplay.player.transform.GetChild (3).gameObject.SetActive (true);
	// 						_wGameplay.player.transform.GetChild (4).gameObject.SetActive (true);
	// //					}

	// 					_wGameplay.isBonusLevelTime = false;
	// 					PlayerPrefs.SetInt ("GotBonusLevel", 0);
	// 					// PlayerPrefs.SetFloat ("CurrentLevel",PlayerPrefs.GetFloat("CurrentLevel")+1f);
						// Invoke ("NotificationEnable",0.3f);
						// Invoke ("NotificationDisable",3.0f);		//2
						// Invoke("LevelChangedAnim",4.0f);		//1					

	// 					if(PlayerPrefs.GetFloat ("CurrentLevel") > 25)
	// 					{
	// 						//PlayerPrefs.SetFloat ("GenUpKunaiWeaponTime", 4f);
	// 						PlayerPrefs.SetFloat ("GenSpikeWeaponTime", 3.5f);
	// 						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//1.5
	// 						_wGameplay.OnPatternCancle ();
	// 						_wGameplay.StopSpikeWeaponCall ();
	// 						_wGameplay.StopuPKunaiWeaponCall ();
	// 						//Invoke("UpKunaiGen",1.5f);
	// 						Invoke("OnPatternGen",4.5f);			//5
	// 						Invoke("SpikeGen",4.5f);			//2
	// 					} 

	// 					else if(PlayerPrefs.GetFloat ("CurrentLevel") > 21)
	// 					{
	// 						Debug.Log("CurrentLevel..." + PlayerPrefs.GetFloat ("CurrentLevel"));
	// 						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//2
	// 						PlayerPrefs.SetFloat ("GenUpKunaiWeaponTime", 4f);
	// 						PlayerPrefs.SetFloat ("GenSpikeWeaponTime", 4f);
	// 						_wGameplay.StopSpikeWeaponCall ();
	// 						_wGameplay.StopuPKunaiWeaponCall ();
	// 						Invoke("UpKunaiGen",4.5f);			//1.5
	// 						Invoke("SpikeGen",4.5f);			//1.5
	// 					} 

	// 					else if (PlayerPrefs.GetFloat ("CurrentLevel") > 15 && PlayerPrefs.GetFloat ("CurrentLevel") < 21)
	// 					{
	// 						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//2.5
	// 						PlayerPrefs.SetFloat ("GenUpKunaiWeaponTime", 3.5f);
	// 						_wGameplay.StopuPKunaiWeaponCall ();
	// 						Invoke("UpKunaiGen",4.5f);			//1.5
	// 					} 

	// 					else if (PlayerPrefs.GetFloat ("CurrentLevel") >= 6 && PlayerPrefs.GetFloat ("CurrentLevel") < 21)
	// 					{
	// 						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//1

	// 						_wGameplay.StopKunaiWeaponCall ();
	// 						Invoke("KunaiGen",4.5f);			//1.5
	// 					}
	// 					else if(PlayerPrefs.GetFloat ("CurrentLevel") < 6)
	// 					{
	// 						PlayerPrefs.SetFloat ("EnimyGenerateTime", 3.5f);
	// 					}

						// _wGameplay.StopEnemyAttack();
						// _wGameplay.StopGeneratedEnimy ();

						// _wGameplay.StopKunaiWeaponCall ();
						// _wGameplay.StopCollectible();
						// _wGameplay.StopuPKunaiWeaponCall ();
						// _wGameplay.StopSpikeWeaponCall ();
						// _wGameplay.OnPatternCancle ();

						// _wGameplay.StopEnemyAttack();
						// _wGameplay.StopGeneratedEnimy ();
						// _wGameplay.StopCollectible();
					}
				}
				else
				{
					if( currentRunningLevel == 4 && PlayerPrefs.GetInt("GotBonusLevel")==0 )
					{
						OnBonusStartDelay();
						Instantiate(_wGameplay.VictoryParticle_1, new Vector3(_wGameplay.VictoryParticle_Pos1.transform.position.x, _wGameplay.VictoryParticle_Pos1.transform.position.y, _wGameplay.VictoryParticle_Pos1.transform.position.z), Quaternion.Euler(_wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.x, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.y, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.z) );
						
						_wGameplay.StopEnemyAttack();

						_wGameplay.StopKunaiWeaponCall ();
						_wGameplay.StopGeneratedEnimy ();
						_wGameplay.StopCollectible();
						_wGameplay.StopuPKunaiWeaponCall ();
						_wGameplay.StopSpikeWeaponCall ();
						_wGameplay.OnPatternCancle ();
					}
					else
					{
						Instantiate(_wGameplay.VictoryParticle_1, new Vector3(_wGameplay.VictoryParticle_Pos1.transform.position.x, _wGameplay.VictoryParticle_Pos1.transform.position.y, _wGameplay.VictoryParticle_Pos1.transform.position.z), Quaternion.Euler(_wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.x, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.y, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.z) );

						_wGameplay.StopEnemyAttack();
						_wGameplay.StopGeneratedEnimy ();
						_wGameplay.StopKunaiWeaponCall ();
						_wGameplay.StopCollectible();
						_wGameplay.StopuPKunaiWeaponCall ();
						_wGameplay.StopSpikeWeaponCall ();
						_wGameplay.OnPatternCancle ();

						// _wGameplay.StopCollectible();
						// CancelInvoke ("GenerateEnemy");
						// _wGameplay.StopGeneratedEnimy();
						// CancelInvoke("StopKunaiWeaponCall");
						// _wGameplay.StopKunaiWeaponCall();
						// CancelInvoke ("UpOnKunaiWeapon");
						// _wGameplay.StopuPKunaiWeaponCall();
						// CancelInvoke ("OnSpikeWeapon");
						// _wGameplay.StopSpikeWeaponCall();
						// CancelInvoke ("OnPatternWeapon");
						// CancelInvoke ("OnLeftPatternWeapon");
						// CancelInvoke ("OnRightPatternWeapon");
						// _wGameplay.OnPatternCancle();
					}
				}
			}
		} 
	}

	IEnumerator Level_Subpanel()
	{
		yield return new WaitForSeconds(2.0f);
		_wGameplay.Level_SubPanel.SetActive(true);
	}

	void NotificationEnable()
	{
		// _wGameplay.LevelCompletePanel.SetActive (true);
	}

	void NotificationDisable()
	{
		transform.GetComponent<Animation> ().Play ();
		if( PlayerPrefs.GetString("SelectedPlayerName") == "FrogPlayer" || PlayerPrefs.GetString("SelectedPlayerName") == "LionPlayer" )
		{
			GameObject.Find(PlayerPrefs.GetString ("SelectedPlayerName")).transform.GetComponent<Animation>().Play("FrogAnimation");
		}
		else if( PlayerPrefs.GetString ("SelectedPlayerName") == "SheepPlayer")
		{
			GameObject.Find(PlayerPrefs.GetString ("SelectedPlayerName")).transform.GetComponent<Animation>().Play("DogAnimation");
		}
		else if( PlayerPrefs.GetString ("SelectedPlayerName") == "NinjaPlayer" || PlayerPrefs.GetString ("SelectedPlayerName") == "BearPlayer" || PlayerPrefs.GetString("SelectedPlayerName") == "MonkeyPlayer" || PlayerPrefs.GetString ("SelectedPlayerName") == "DogPlayer" || PlayerPrefs.GetString ("SelectedPlayerName") == "PigPlayer" || PlayerPrefs.GetString("SelectedPlayerName") == "TigerPlayer" || PlayerPrefs.GetString("SelectedPlayerName") == "OwlPlayer" || PlayerPrefs.GetString("SelectedPlayerName") == "BirdPlayer" )
		{
			GameObject.Find(PlayerPrefs.GetString ("SelectedPlayerName")).transform.GetComponent<Animation>().Play("BearAnimation");
		}
		else
		{
			GameObject.Find(PlayerPrefs.GetString ("SelectedPlayerName")).transform.GetComponent<Animation>().Play("CowAnimation");
		}
		if ((PlayerPrefs.GetString ("isNormalGameRunning") == "true")) 
		{
//			for(int i1 = 0; i1 < _wGameplay.player.transform.childCount; i1++)// (int i1 = 0; i1 < _wGameplay.NinjaEye.Count; i1++)
//			{

				_wGameplay.player.transform.GetChild (0).gameObject.SetActive (true);
				_wGameplay.player.transform.GetChild (0).GetChild (0).gameObject.SetActive (true);
				_wGameplay.player.transform.GetChild (3).gameObject.SetActive (false);
				_wGameplay.player.transform.GetChild (4).gameObject.SetActive (false);
		//	}

			_wGameplay.isLevelContinue = false;

		//	_wGameplay.StartGeneratingEnemy ();
			_wGameplay.CollectibleStart ();
			OnWallRotationSpeed();
			// _wGameplay.LevelCompletePanel.SetActive (false);
		}

		else if(PlayerPrefs.GetString ("isTimeBaseGameRunnig") == "true")
		{
//			for(int i1 = 0; i1 < _wGameplay.player.transform.childCount; i1++)// (int i1 = 0; i1 < _wGameplay.NinjaEye.Count; i1++)
//			{
				_wGameplay.player.transform.GetChild (0).gameObject.SetActive (true);
				_wGameplay.player.transform.GetChild (0).GetChild (0).gameObject.SetActive (true);
				_wGameplay.player.transform.GetChild (3).gameObject.SetActive (false);
				_wGameplay.player.transform.GetChild (4).gameObject.SetActive (false);
		//	}
			_wGameplay.isLevelContinue = false;
			_wTimeGameplay.OnTimeModeGame ();
			OnWallRotationSpeed();
			_wGameplay.LevelCompletePanel.SetActive (false);
		}
	}

	void LevelChangedAnim()
	{
		_wGameplay.LevelChange ();
	}

	public void TaptoContinue()
	{
		if(Application.internetReachability == NetworkReachability.NotReachable)
		{
			_wGamecontrol._LevelUp_continue.interactable = false;
			StartCoroutine(TaptoContinue_delay());
		}
		else
		{
			if( PlayerPrefs.GetInt("NoAd") == 1 )
			{
				_wGamecontrol._LevelUp_continue.interactable = false;
				StartCoroutine(TaptoContinue_delay());
			}
			else
			{
				_wGamecontrol._LevelUp_continue.interactable = false;
				// Camera.main.GetComponent<GoogleMobileAdsDemoScript>().ShowInterstitial();
				StartCoroutine(IntertitialRequest());
			}
		}
		// _wGamecontrol._LevelUp_continue.interactable = false;
		if( _wGamecontrol.Continue_bool == true )
		{

		}
		else
		{
			TotalClaim_stars = true;
			total_timer = 0;
		}	
		
		/*if (PlayerPrefs.GetInt ("SoundOn") == 0)
		{
			_wGameplay.SoundHandle.transform.GetComponent<AudioSource> ().Play ();
		}

		if( _wGamecontrol.Continue_bool == true )
		{
			_wGamecontrol.Continue_bool = false;
		}
		else
		{
			int collectibleCount = PlayerPrefs.GetInt("PowerUpCollect") + _wGameplay.Powerup_count;
			PlayerPrefs.SetInt("PowerUpCollect",collectibleCount);
			_wGameplay.PowerUpTxt.text = collectibleCount.ToString();
		}        		

		_wGameplay.Powerup_count = 0;
		_wGameplay._CurrentPowerUp_count.text = _wGameplay.Powerup_count.ToString();
		
		_wGameplay.LevelUpPanel.SetActive(false);
		_wGameplay._CurrentPowerUp_count.enabled = true;
		_wGameplay._applicationPause = true;
		if (PlayerPrefs.GetString ("isTimeBaseGameRunnig") == "true") 
		{
			if (Gameplay.isTimeBaseFillbarOver == true) 
			{
				Gameplay.isTimeBaseFillbarOver = false;
				_wTimeGameplay.OnStopTimeCollectible ();

				Instantiate (_wGameplay.LevelUpPaticle, Player.transform.position, Quaternion.identity);
				// Instantiate(_wGameplay.VictoryParticle_1, new Vector3(_wGameplay.VictoryParticle_Pos1.transform.position.x, _wGameplay.VictoryParticle_Pos1.transform.position.y, _wGameplay.VictoryParticle_Pos1.transform.position.z), Quaternion.Euler(_wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.x, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.y, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.z) );

				_wGameplay.isLevelContinue = true;

				OnBackGroundChange ();

//				for (int i1 = 0; i1 < _wGameplay.player.transform.childCount; i1++)
//				{
					_wGameplay.player.transform.GetChild (0).gameObject.SetActive (false);
					//_wGameplay.player.transform.GetChild (0).GetChild (0).gameObject.SetActive (false);
					_wGameplay.player.transform.GetChild (3).gameObject.SetActive (true);
					_wGameplay.player.transform.GetChild (4).gameObject.SetActive (true);
//				}

				PlayerPrefs.SetFloat ("TimeCurrentLevel", PlayerPrefs.GetFloat ("TimeCurrentLevel") + 1f);
				Invoke ("NotificationEnable", 0.2f);
				Invoke ("NotificationDisable", 1.0f);			//2.5
				Invoke("LevelChangedAnim",2.0f);

				// _wTimeGameplay.DesEnemyAttack ();
			}
		}
		else
		{
			if( currentRunningLevel >= 5 )
			{
				if (currentRunningLevel%5 == 4 && PlayerPrefs.GetInt("GotBonusLevel")==0)
				{
					foreach (GameObject wGo in _wGameplay.GeneratedEnimyClone) 
					{
						Destroy (wGo.gameObject);
					}

					PlayerPrefs.SetInt ("BonusLevelConti",1);

					Invoke ("OnBonusStartDelay",1.5f);
					_wGameplay.StopEnemyAttack();

					_wGameplay.StopKunaiWeaponCall ();
					_wGameplay.StopGeneratedEnimy ();
					_wGameplay.StopCollectible();
					_wGameplay.StopuPKunaiWeaponCall ();
					_wGameplay.StopSpikeWeaponCall ();
					_wGameplay.OnPatternCancle ();
					OnBackGroundChange ();
				}

				else
				{
					Instantiate (_wGameplay.LevelUpPaticle, Player.transform.position, Quaternion.identity);
					// Instantiate(_wGameplay.VictoryParticle_1, new Vector3(_wGameplay.VictoryParticle_Pos1.transform.position.x, _wGameplay.VictoryParticle_Pos1.transform.position.y, _wGameplay.VictoryParticle_Pos1.transform.position.z), Quaternion.Euler(_wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.x, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.y, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.z) );					

					_wGameplay.isLevelContinue = true;
					OnBackGroundChange ();

					_wGameplay.player.transform.GetChild (0).gameObject.SetActive (false);
					_wGameplay.player.transform.GetChild (3).gameObject.SetActive (true);
					_wGameplay.player.transform.GetChild (4).gameObject.SetActive (true);

					_wGameplay.isBonusLevelTime = false;
					PlayerPrefs.SetInt ("GotBonusLevel", 0);
					// PlayerPrefs.SetFloat ("CurrentLevel",PlayerPrefs.GetFloat("CurrentLevel")+1f);
					Invoke ("NotificationEnable",0.3f);
					Invoke ("NotificationDisable",1.0f);		//2
					Invoke("LevelChangedAnim",2.0f);		//1					

					if(PlayerPrefs.GetFloat ("CurrentLevel") > 25)
					{
						PlayerPrefs.SetFloat ("GenSpikeWeaponTime", 3.5f);
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//1.5
						_wGameplay.OnPatternCancle ();
						_wGameplay.StopSpikeWeaponCall ();
						_wGameplay.StopuPKunaiWeaponCall ();
						Invoke("OnPatternGen",4.5f);			//5
						// OnPatternGen();
						Invoke("SpikeGen",4.5f);			//2
						// SpikeGen();
					} 

					else if(PlayerPrefs.GetFloat ("CurrentLevel") > 21)
					{
						Debug.Log("CurrentLevel..." + PlayerPrefs.GetFloat ("CurrentLevel"));
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//2
						PlayerPrefs.SetFloat ("GenUpKunaiWeaponTime", 4f);
						PlayerPrefs.SetFloat ("GenSpikeWeaponTime", 4f);
						_wGameplay.StopSpikeWeaponCall ();
						_wGameplay.StopuPKunaiWeaponCall ();
						Invoke("UpKunaiGen",4.5f);			//1.5
						// UpKunaiGen();
						Invoke("SpikeGen",4.5f);			//1.5
						// SpikeGen();
					} 

					else if (PlayerPrefs.GetFloat ("CurrentLevel") > 15 && PlayerPrefs.GetFloat ("CurrentLevel") < 21)
					{
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//2.5
						PlayerPrefs.SetFloat ("GenUpKunaiWeaponTime", 3.5f);
						_wGameplay.StopuPKunaiWeaponCall ();
						Invoke("UpKunaiGen",4.5f);			//1.5
						// UpKunaiGen();
					} 

					else if (PlayerPrefs.GetFloat ("CurrentLevel") >= 6 && PlayerPrefs.GetFloat ("CurrentLevel") < 21)
					{
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//1

						_wGameplay.StopKunaiWeaponCall ();
						// Invoke("KunaiGen",4.5f);			//1.5
						Invoke("KunaiGen",4.5f);
						// KunaiGen();
					}
					else if(PlayerPrefs.GetFloat ("CurrentLevel") < 6)
					{
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 3.5f);
					}

					_wGameplay.StopEnemyAttack();
					_wGameplay.StopGeneratedEnimy ();
					_wGameplay.StopCollectible();
				}
			}
			else
			{
				if( currentRunningLevel == 4 && PlayerPrefs.GetInt("GotBonusLevel")==0)
				{
					Debug.LogError("CurrentRunningLevel..." + currentRunningLevel);
					foreach (GameObject wGo in _wGameplay.GeneratedEnimyClone) 
					{
						Destroy (wGo.gameObject);
					}

					PlayerPrefs.SetInt ("BonusLevelConti",1);

					Invoke ("OnBonusStartDelay",1.5f);
					_wGameplay.StopEnemyAttack();

					_wGameplay.StopKunaiWeaponCall ();
					_wGameplay.StopGeneratedEnimy ();
					_wGameplay.StopCollectible();
					_wGameplay.StopuPKunaiWeaponCall ();
					_wGameplay.StopSpikeWeaponCall ();
					_wGameplay.OnPatternCancle ();
					OnBackGroundChange ();
				}
				else
				{
					Instantiate (_wGameplay.LevelUpPaticle, Player.transform.position, Quaternion.identity);
					// Instantiate(_wGameplay.VictoryParticle_1, new Vector3(_wGameplay.VictoryParticle_Pos1.transform.position.x, _wGameplay.VictoryParticle_Pos1.transform.position.y, _wGameplay.VictoryParticle_Pos1.transform.position.z), Quaternion.Euler(_wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.x, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.y, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.z) );					

					_wGameplay.isLevelContinue = true;
					OnBackGroundChange ();

					_wGameplay.player.transform.GetChild (0).gameObject.SetActive (false);
					_wGameplay.player.transform.GetChild (3).gameObject.SetActive (true);
					_wGameplay.player.transform.GetChild (4).gameObject.SetActive (true);

					_wGameplay.isBonusLevelTime = false;
					PlayerPrefs.SetInt ("GotBonusLevel", 0);
					// PlayerPrefs.SetFloat ("CurrentLevel",PlayerPrefs.GetFloat("CurrentLevel")+1f);
					Invoke ("NotificationEnable",0.3f);
					Invoke ("NotificationDisable",1.0f);		//2
					Invoke("LevelChangedAnim",2.0f);		//1					

					if(PlayerPrefs.GetFloat ("CurrentLevel") > 25)
					{
						PlayerPrefs.SetFloat ("GenSpikeWeaponTime", 3.5f);
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//1.5
						_wGameplay.OnPatternCancle ();
						_wGameplay.StopSpikeWeaponCall ();
						_wGameplay.StopuPKunaiWeaponCall ();
						Invoke("OnPatternGen",4.5f);			//5
						// OnPatternGen();
						Invoke("SpikeGen",4.5f);			//2
						// SpikeGen();
					} 

					else if(PlayerPrefs.GetFloat ("CurrentLevel") > 21)
					{
						Debug.Log("CurrentLevel..." + PlayerPrefs.GetFloat ("CurrentLevel"));
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//2
						PlayerPrefs.SetFloat ("GenUpKunaiWeaponTime", 4f);
						PlayerPrefs.SetFloat ("GenSpikeWeaponTime", 4f);
						_wGameplay.StopSpikeWeaponCall ();
						_wGameplay.StopuPKunaiWeaponCall ();
						Invoke("UpKunaiGen",4.5f);			//1.5
						// UpKunaiGen();
						Invoke("SpikeGen",4.5f);			//1.5
						// SpikeGen();
					} 

					else if (PlayerPrefs.GetFloat ("CurrentLevel") > 15 && PlayerPrefs.GetFloat ("CurrentLevel") < 21)
					{
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//2.5
						PlayerPrefs.SetFloat ("GenUpKunaiWeaponTime", 3.5f);
						_wGameplay.StopuPKunaiWeaponCall ();
						Invoke("UpKunaiGen",4.5f);			//1.5
						// UpKunaiGen();
					} 

					else if (PlayerPrefs.GetFloat ("CurrentLevel") >= 6 && PlayerPrefs.GetFloat ("CurrentLevel") < 21)
					{
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//1

						_wGameplay.StopKunaiWeaponCall ();
						// Invoke("KunaiGen",4.5f);			//1.5
						Invoke("KunaiGen",4.5f);
						// KunaiGen();
					}
					else if(PlayerPrefs.GetFloat ("CurrentLevel") < 6)
					{
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 3.5f);
					}

					_wGameplay.StopEnemyAttack();
					_wGameplay.StopGeneratedEnimy ();
					_wGameplay.StopCollectible();
				}
			}
		}*/
	
	}

	IEnumerator TaptoContinue_delay()
	{
		// if(Application.internetReachability == NetworkReachability.NotReachable)
		// {

		// }
		// else
		// {
		// 	if( PlayerPrefs.GetInt("NoAd") == 1 )
		// 	{
			
		// 	}
		// 	else
		// 	{
		// 		Camera.main.GetComponent<GoogleMobileAdsDemoScript>().ShowInterstitial();
		// 	}
		// }
		yield return new WaitForSeconds(2.0f);
		if (PlayerPrefs.GetInt ("SoundOn") == 0)
		{
			_wGameplay.SoundHandle.transform.GetComponent<AudioSource> ().Play ();
		}

		if( _wGamecontrol.Continue_bool == true )
		{
			_wGamecontrol.Continue_bool = false;
		}
		else
		{
			// int collectibleCount = PlayerPrefs.GetInt("PowerUpCollect") + _wGameplay.Powerup_count;
			// PlayerPrefs.SetInt("PowerUpCollect",collectibleCount);
			// _wGameplay.PowerUpTxt.text = collectibleCount.ToString();
		}	

		_wGameplay.Powerup_count = 0;
		_wGameplay._CurrentPowerUp_count.text = _wGameplay.Powerup_count.ToString();
		
		_wGameplay.LevelUpPanel.SetActive(false);
		_wGameplay.player.SetActive(true);
		_wGameplay.ScorePanel.SetActive(true);
		for( int i = 0; i < _wGameplay.Wall_obj.transform.childCount; i++ )
		{
			_wGameplay.Wall_obj.gameObject.transform.GetChild(i).gameObject.SetActive(true);
			_wGameplay.Wall_obj.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
		}
		_wGameplay.LevelPanel_Anim.SetActive(false);
		_wGameplay.Level_SubPanel.SetActive(false);
		_wGameplay._CurrentPowerUp_count.enabled = true;
		_wGameplay._applicationPause = true;
		if (PlayerPrefs.GetString ("isTimeBaseGameRunnig") == "true") 
		{
			if (Gameplay.isTimeBaseFillbarOver == true) 
			{
				Gameplay.isTimeBaseFillbarOver = false;
				_wTimeGameplay.OnStopTimeCollectible ();

				Instantiate (_wGameplay.LevelUpPaticle, _wGameplay.player.transform.position, Quaternion.identity);
				// Instantiate(_wGameplay.VictoryParticle_1, new Vector3(_wGameplay.VictoryParticle_Pos1.transform.position.x, _wGameplay.VictoryParticle_Pos1.transform.position.y, _wGameplay.VictoryParticle_Pos1.transform.position.z), Quaternion.Euler(_wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.x, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.y, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.z) );

				_wGameplay.isLevelContinue = true;

				// OnBackGroundChange ();

//				for (int i1 = 0; i1 < _wGameplay.player.transform.childCount; i1++)
//				{
					_wGameplay.player.transform.GetChild (0).gameObject.SetActive (false);
					//_wGameplay.player.transform.GetChild (0).GetChild (0).gameObject.SetActive (false);
					_wGameplay.player.transform.GetChild (3).gameObject.SetActive (true);
					_wGameplay.player.transform.GetChild (4).gameObject.SetActive (true);
//				}

				PlayerPrefs.SetFloat ("TimeCurrentLevel", PlayerPrefs.GetFloat ("TimeCurrentLevel") + 1f);
				Invoke ("NotificationEnable", 0.2f);
				Invoke ("NotificationDisable", 1.0f);			//2.5
				Invoke("LevelChangedAnim",2.0f);

				// _wTimeGameplay.DesEnemyAttack ();
			}
		}
		else
		{			
			// _wGameplay.CurrentLevel_normal.text = PlayerPrefs.GetFloat("CurrentLevel").ToString();
			// float nextLevel_no = PlayerPrefs.GetFloat("CurrentLevel") + 1;
			// _wGameplay.NextLevel_normal.text = nextLevel_no.ToString();
			if( currentRunningLevel >= 5 )
			{
				if (currentRunningLevel%5 == 4 && PlayerPrefs.GetInt("GotBonusLevel")==0)
				{
					foreach (GameObject wGo in _wGameplay.GeneratedEnimyClone) 
					{
						Destroy (wGo.gameObject);
					}

					PlayerPrefs.SetInt ("BonusLevelConti",1);

					Invoke ("OnBonusStartDelay",1.5f);
					_wGameplay.StopEnemyAttack();

					_wGameplay.StopKunaiWeaponCall ();
					_wGameplay.StopGeneratedEnimy ();
					_wGameplay.StopCollectible();
					_wGameplay.StopuPKunaiWeaponCall ();
					_wGameplay.StopSpikeWeaponCall ();
					_wGameplay.OnPatternCancle ();
					// OnBackGroundChange ();
				}

				else
				{
					Instantiate (_wGameplay.LevelUpPaticle, _wGameplay.player.transform.position, Quaternion.identity);
					// Instantiate(_wGameplay.VictoryParticle_1, new Vector3(_wGameplay.VictoryParticle_Pos1.transform.position.x, _wGameplay.VictoryParticle_Pos1.transform.position.y, _wGameplay.VictoryParticle_Pos1.transform.position.z), Quaternion.Euler(_wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.x, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.y, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.z) );					

					_wGameplay.isLevelContinue = true;
					// OnBackGroundChange ();

					_wGameplay.player.transform.GetChild (0).gameObject.SetActive (false);
					_wGameplay.player.transform.GetChild (3).gameObject.SetActive (true);
					_wGameplay.player.transform.GetChild (4).gameObject.SetActive (true);

					_wGameplay.isBonusLevelTime = false;
					PlayerPrefs.SetInt ("GotBonusLevel", 0);
					// PlayerPrefs.SetFloat ("CurrentLevel",PlayerPrefs.GetFloat("CurrentLevel")+1f);
					Invoke ("NotificationEnable",0.3f);
					Invoke ("NotificationDisable",1.0f);		//2
					Invoke("LevelChangedAnim",2.0f);		//1
					Invoke("Leveltext_delay",2.5f);

					if(PlayerPrefs.GetFloat ("CurrentLevel") > 25)
					{
						PlayerPrefs.SetFloat ("GenSpikeWeaponTime", 3.5f);
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//1.5
						_wGameplay.OnPatternCancle ();
						_wGameplay.StopSpikeWeaponCall ();
						_wGameplay.StopuPKunaiWeaponCall ();
						Invoke("OnPatternGen",4.5f);			//5
						// OnPatternGen();
						Invoke("SpikeGen",4.5f);			//2
						// SpikeGen();
					} 

					else if(PlayerPrefs.GetFloat ("CurrentLevel") > 21)
					{
						Debug.Log("CurrentLevel..." + PlayerPrefs.GetFloat ("CurrentLevel"));
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//2
						PlayerPrefs.SetFloat ("GenUpKunaiWeaponTime", 4f);
						PlayerPrefs.SetFloat ("GenSpikeWeaponTime", 4f);
						_wGameplay.StopSpikeWeaponCall ();
						_wGameplay.StopuPKunaiWeaponCall ();
						Invoke("UpKunaiGen",4.5f);			//1.5
						// UpKunaiGen();
						Invoke("SpikeGen",4.5f);			//1.5
						// SpikeGen();
					} 

					else if (PlayerPrefs.GetFloat ("CurrentLevel") > 15 && PlayerPrefs.GetFloat ("CurrentLevel") < 21)
					{
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//2.5
						PlayerPrefs.SetFloat ("GenUpKunaiWeaponTime", 3.5f);
						_wGameplay.StopuPKunaiWeaponCall ();
						Invoke("UpKunaiGen",4.5f);			//1.5
						// UpKunaiGen();
					} 

					else if (PlayerPrefs.GetFloat ("CurrentLevel") >= 6 && PlayerPrefs.GetFloat ("CurrentLevel") < 21)
					{
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//1

						_wGameplay.StopKunaiWeaponCall ();
						// Invoke("KunaiGen",4.5f);			//1.5
						Invoke("KunaiGen",4.5f);
						// KunaiGen();
					}
					else if(PlayerPrefs.GetFloat ("CurrentLevel") < 6)
					{
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 3.5f);
					}

					_wGameplay.StopEnemyAttack();
					_wGameplay.StopGeneratedEnimy ();
					_wGameplay.StopCollectible();
				}
			}
			else
			{
				if( currentRunningLevel == 4 && PlayerPrefs.GetInt("GotBonusLevel")==0)
				{
					Debug.LogError("CurrentRunningLevel..." + currentRunningLevel);
					foreach (GameObject wGo in _wGameplay.GeneratedEnimyClone) 
					{
						Destroy (wGo.gameObject);
					}

					PlayerPrefs.SetInt ("BonusLevelConti",1);

					Invoke ("OnBonusStartDelay",1.5f);
					_wGameplay.StopEnemyAttack();

					_wGameplay.StopKunaiWeaponCall ();
					_wGameplay.StopGeneratedEnimy ();
					_wGameplay.StopCollectible();
					_wGameplay.StopuPKunaiWeaponCall ();
					_wGameplay.StopSpikeWeaponCall ();
					_wGameplay.OnPatternCancle ();
					// OnBackGroundChange ();
				}
				else
				{
					Instantiate (_wGameplay.LevelUpPaticle, _wGameplay.player.transform.position, Quaternion.identity);
					// Instantiate(_wGameplay.VictoryParticle_1, new Vector3(_wGameplay.VictoryParticle_Pos1.transform.position.x, _wGameplay.VictoryParticle_Pos1.transform.position.y, _wGameplay.VictoryParticle_Pos1.transform.position.z), Quaternion.Euler(_wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.x, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.y, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.z) );					

					_wGameplay.isLevelContinue = true;
					// OnBackGroundChange ();

					_wGameplay.player.transform.GetChild (0).gameObject.SetActive (false);
					_wGameplay.player.transform.GetChild (3).gameObject.SetActive (true);
					_wGameplay.player.transform.GetChild (4).gameObject.SetActive (true);

					_wGameplay.isBonusLevelTime = false;
					PlayerPrefs.SetInt ("GotBonusLevel", 0);
					// PlayerPrefs.SetFloat ("CurrentLevel",PlayerPrefs.GetFloat("CurrentLevel")+1f);
					Invoke ("NotificationEnable",0.3f);
					Invoke ("NotificationDisable",1.0f);		//2
					Invoke("LevelChangedAnim",2.0f);		//1
					Invoke("Leveltext_delay",2.5f);

					if(PlayerPrefs.GetFloat ("CurrentLevel") > 25)
					{
						PlayerPrefs.SetFloat ("GenSpikeWeaponTime", 3.5f);
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//1.5
						_wGameplay.OnPatternCancle ();
						_wGameplay.StopSpikeWeaponCall ();
						_wGameplay.StopuPKunaiWeaponCall ();
						Invoke("OnPatternGen",4.5f);			//5
						// OnPatternGen();
						Invoke("SpikeGen",4.5f);			//2
						// SpikeGen();
					} 

					else if(PlayerPrefs.GetFloat ("CurrentLevel") > 21)
					{
						Debug.Log("CurrentLevel..." + PlayerPrefs.GetFloat ("CurrentLevel"));
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//2
						PlayerPrefs.SetFloat ("GenUpKunaiWeaponTime", 4f);
						PlayerPrefs.SetFloat ("GenSpikeWeaponTime", 4f);
						_wGameplay.StopSpikeWeaponCall ();
						_wGameplay.StopuPKunaiWeaponCall ();
						Invoke("UpKunaiGen",4.5f);			//1.5
						// UpKunaiGen();
						Invoke("SpikeGen",4.5f);			//1.5
						// SpikeGen();
					} 

					else if (PlayerPrefs.GetFloat ("CurrentLevel") > 15 && PlayerPrefs.GetFloat ("CurrentLevel") < 21)
					{
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//2.5
						PlayerPrefs.SetFloat ("GenUpKunaiWeaponTime", 3.5f);
						_wGameplay.StopuPKunaiWeaponCall ();
						Invoke("UpKunaiGen",4.5f);			//1.5
						// UpKunaiGen();
					} 

					else if (PlayerPrefs.GetFloat ("CurrentLevel") >= 6 && PlayerPrefs.GetFloat ("CurrentLevel") < 21)
					{
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//1

						_wGameplay.StopKunaiWeaponCall ();
						// Invoke("KunaiGen",4.5f);			//1.5
						Invoke("KunaiGen",4.5f);
						// KunaiGen();
					}
					else if(PlayerPrefs.GetFloat ("CurrentLevel") < 6)
					{
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 3.5f);
					}

					_wGameplay.StopEnemyAttack();
					_wGameplay.StopGeneratedEnimy ();
					_wGameplay.StopCollectible();
				}
			}
		}
	}

	IEnumerator IntertitialRequest()
	{
		yield return new WaitForSeconds(2.0f);
		PlayerPrefs.SetInt("Continue_btn",1);
		Camera.main.GetComponent<GoogleMobileAdsDemoScript>().ShowInterstitial();
		Camera.main.GetComponent<GoogleMobileAdsDemoScript>().RequestInterstitial();
	}

	public void Leveltext_delay()
	{
		_wGameplay.LevelText.enabled = true;
		_wGameplay.CurrentLevel_text.enabled = true;
	}

	public void DisplayTotalStarLerp(GameObject _referencedObj)
	{		
		total_timer += Time.deltaTime;
		if( _wGameplay.Powerup_count > 0 )
		{
			_wGameplay.CurrentLevel_star.enabled = true;
			_wGoogleAds.TotalReward_star.SetActive(true);
			// _wGoogleAds.ClaimReward_star.SetActive(true);
			_referencedObj.SetActive(true);
			_referencedObj.transform.position = Vector2.Lerp(_referencedObj.transform.position,  _wGoogleAds._finalTargetPosition.transform.position, total_timer / 10.0f);
		}
		if(total_timer > 1)
		{
			_referencedObj.SetActive(false);
			//_referencedObj.transform.position = _uGamePlay.TotalStar_collected.transform.position;
			_referencedObj.transform.position = _wGoogleAds._targetPosition.transform.position;
			int collectibleCount = PlayerPrefs.GetInt("PowerUpCollect") + _wGameplay.Powerup_count;
			PlayerPrefs.SetInt("PowerUpCollect",collectibleCount);
			_wGameplay.PowerUpTxt.text = collectibleCount.ToString();
			TotalClaim_stars = false;
		}
	}

	public void OnWallRotationSpeed()
	{
		if ((PlayerPrefs.GetString ("isNormalGameRunning") == "true"))
		{
			if (PlayerPrefs.GetFloat ("CurrentLevel") >= 1 && PlayerPrefs.GetFloat ("CurrentLevel") <= 10)
			{
				PlayerPrefs.SetFloat ("WallRotationSpeed", 8f);				//6f
			}

			else
			{
				PlayerPrefs.SetFloat ("WallRotationSpeed", 10f);				//9f
			}
		}

		else if(PlayerPrefs.GetString ("isTimeBaseGameRunnig") == "true")
		{
			if (PlayerPrefs.GetFloat ("TimeCurrentLevel") >= 1 && PlayerPrefs.GetFloat ("TimeCurrentLevel") <= 10)
			{
				PlayerPrefs.SetFloat ("WallRotationSpeed", 8f);				//6f
			}

			else
			{
				PlayerPrefs.SetFloat ("WallRotationSpeed", 10f);				//9f
			}
		}
		else
		{
		}
			

//		if (PlayerPrefs.GetFloat ("CurrentLevel") > 20)
//		{
//			PlayerPrefs.SetFloat ("WallRotationSpeed", 9.5f);
//		}
//
//		else if (PlayerPrefs.GetFloat ("CurrentLevel") >= 16 && PlayerPrefs.GetFloat ("CurrentLevel") <= 20)
//		{
//			PlayerPrefs.SetFloat ("WallRotationSpeed", 8.5f);
//		}
//
//		else if (PlayerPrefs.GetFloat ("CurrentLevel") >= 11 && PlayerPrefs.GetFloat ("CurrentLevel") <= 15)
//		{
//			PlayerPrefs.SetFloat ("WallRotationSpeed", 8);
//		}
//
//		else
//		{
//			PlayerPrefs.SetFloat ("WallRotationSpeed", 8);
//		}
	}

	public void OnBackGroundChange()
	{
		if (PlayerPrefs.GetInt ("BgColor") >= 12)
		{
			PlayerPrefs.SetInt ("BgColor", 0);
			colorId = 0;

			OnBackGroundChange ();
		} 
		else
		{
			BackGround.transform.position = BgCurrentPos;
			// BackGround.transform.GetComponent<SpriteRenderer> ().material.SetColor ("_ColorBot", BGColors [PlayerPrefs.GetInt ("BgColor")]);
			BackGround.transform.GetComponent<SpriteRenderer> ().sprite = BGSprite[PlayerPrefs.GetInt ("BgColor")];
			colorId = PlayerPrefs.GetInt ("BgColor") + 1;
			PlayerPrefs.SetInt ("BgColor", colorId);
			// BackGround.transform.GetComponent<SpriteRenderer> ().material.SetColor ("_ColorTop", BGColors [PlayerPrefs.GetInt ("BgColor")]);
		}
	}

	void SpikeGen()
	{
		_wGameplay.SpikeWeaponCall ();
	}

	void KunaiGen()
	{
		_wGameplay.KunaiWeaponCall ();
	}

	void UpKunaiGen()
	{
		_wGameplay.UpKunaiWeaponCall ();
	}

	void OnPatternGen()
	{
		_wGameplay.PatternWeaponCall ();
	}

	void OnBonusStartDelay()
	{
		PlayerPrefs.SetFloat ("OnBonusLevel", 1);
		PlayerPrefs.SetInt ("GotBonusLevel", 1);
		// Invoke("LevelChangedAnim",4.0f);

		// LevelChangedAnim ();
	}
}