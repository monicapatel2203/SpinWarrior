using System;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// Example script showing how to invoke the Google Mobile Ads Unity plugin.
public class GoogleMobileAdsDemoScript : MonoBehaviour
{
    public BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;
    private float deltaTime = 0.0f;
    private static string outputMessage = string.Empty;
    int count;
	static int AdCount;
	public string zoneId;
	public GameObject StartTimeCounter , GameOverScreen , BgPos, DailyReward_panel, _targetPosition, _finalTargetPosition, ClaimReward_star,claimReward_pos, TotalReward_star;
	Gameplay _uGamePlay;
    GameControllerScript _uGameController;
    TimeGameplay _uTimeGameplay;
    Wall _uWall;
	BackGroundColor _uBGColour;
    public bool _dailyads_rewards,_resumegame_rewards, _claimads_rewards,claimreward_anim, TotalClaim_stars;
	Button m_Button;
    public static GoogleMobileAdsDemoScript Instance;
    float timer,total_timer;
    int collectibleCount;
    public GameObject Wall;

    public static string OutputMessage
    {
        set { outputMessage = value; }
    }

    public void Awake()
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
                this.RequestBanner();
                Debug.Log("request banner");
            }
        }       
    }

    public void Start()
    {
        _dailyads_rewards = false;
        _resumegame_rewards = false;
        _claimads_rewards = false;
        claimreward_anim = false;
        TotalClaim_stars = false;
        DailyReward_panel.SetActive(false);
        Instance = this;
#if UNITY_ANDROID
        string appId = "ca-app-pub-2187784968688148~1126497189";
#elif UNITY_IPHONE
        string appId = "ca-app-pub-3940256099942544~1458002511";
#else
        string appId = "unexpected_platform";
#endif

        MobileAds.SetiOSAppPauseOnBackground(true);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        this.CreateAndLoadRewardedAd();

        _uGamePlay = FindObjectOfType<Gameplay> ();
		_uBGColour = FindObjectOfType<BackGroundColor> ();
        _uGameController = FindObjectOfType<GameControllerScript>();
        _uTimeGameplay = FindObjectOfType<TimeGameplay>();
        _uWall = FindObjectOfType<Wall>();
    }

    public void Update()
    {
        // Calculate simple moving average for time to render screen. 0.1 factor used as smoothing
        // value.
        this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;
        if( _claimads_rewards == true )
        {
            DisplayImageLerp(ClaimReward_star);
            // _claimads_rewards = false;
        }
        if( TotalClaim_stars == true )
        {
            DisplayTotalStarLerp(TotalReward_star);
        }
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            PlayerPrefs.SetInt("BannerAds", 0);
            this.bannerView.Destroy();            
        }
        else
        {
            if( PlayerPrefs.GetInt("NoAd") == 1 )
            {
                this.bannerView.Destroy();
                PlayerPrefs.SetInt("BannerAds", 0);
            }
            else
            {
                if(PlayerPrefs.GetInt("BannerAds") == 1)
                {
                    // Debug.Log("Banner ads 1");
                }
                else
                {
                    // Debug.Log("Banner ads 2");
                    this.RequestBanner();
                    PlayerPrefs.SetInt("BannerAds", 1);
                }
            }            
        }        
    }

    // public void OnGUI()
    // {
    //     GUIStyle style = new GUIStyle();

    //     Rect rect = new Rect(0, 0, Screen.width, Screen.height);
    //     style.alignment = TextAnchor.LowerRight;
    //     style.fontSize = (int)(Screen.height * 0.06);
    //     style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
    //     float fps = 1.0f / this.deltaTime;
    //     string text = string.Format("{0:0.} fps", fps);
    //     GUI.Label(rect, text, style);

    //     // Puts some basic buttons onto the screen.
    //     GUI.skin.button.fontSize = (int)(0.035f * Screen.width);
    //     float buttonWidth = 0.35f * Screen.width;
    //     float buttonHeight = 0.15f * Screen.height;
    //     float columnOnePosition = 0.1f * Screen.width;
    //     float columnTwoPosition = 0.55f * Screen.width;

    //     Rect requestBannerRect = new Rect(
    //         columnOnePosition,
    //         0.05f * Screen.height,
    //         buttonWidth,
    //         buttonHeight);
    //     if (GUI.Button(requestBannerRect, "Request\nBanner"))
    //     {
    //         this.RequestBanner();
    //     }

    //     Rect destroyBannerRect = new Rect(
    //         columnOnePosition,
    //         0.225f * Screen.height,
    //         buttonWidth,
    //         buttonHeight);
    //     if (GUI.Button(destroyBannerRect, "Destroy\nBanner"))
    //     {
    //         this.bannerView.Destroy();
    //     }

    //     Rect requestInterstitialRect = new Rect(
    //         columnOnePosition,
    //         0.4f * Screen.height,
    //         buttonWidth,
    //         buttonHeight);
    //     if (GUI.Button(requestInterstitialRect, "Request\nInterstitial"))
    //     {
    //         this.RequestInterstitial();
    //     }

    //     Rect showInterstitialRect = new Rect(
    //         columnOnePosition,
    //         0.575f * Screen.height,
    //         buttonWidth,
    //         buttonHeight);
    //     if (GUI.Button(showInterstitialRect, "Show\nInterstitial"))
    //     {
    //         this.ShowInterstitial();
    //     }

    //     Rect destroyInterstitialRect = new Rect(
    //         columnOnePosition,
    //         0.75f * Screen.height,
    //         buttonWidth,
    //         buttonHeight);
    //     if (GUI.Button(destroyInterstitialRect, "Destroy\nInterstitial"))
    //     {
    //         this.interstitial.Destroy();
    //     }

    //     Rect requestRewardedRect = new Rect(
    //         columnTwoPosition,
    //         0.05f * Screen.height,
    //         buttonWidth,
    //         buttonHeight);
    //     if (GUI.Button(requestRewardedRect, "Request\nRewarded Ad"))
    //     {
    //         this.CreateAndLoadRewardedAd();
    //     }

    //     Rect showRewardedRect = new Rect(
    //         columnTwoPosition,
    //         0.225f * Screen.height,
    //         buttonWidth,
    //         buttonHeight);
    //     if (GUI.Button(showRewardedRect, "Show\nRewarded Ad"))
    //     {
    //         this.ShowRewardedAd();
    //     }

    //     Rect textOutputRect = new Rect(
    //         columnTwoPosition,
    //         0.925f * Screen.height,
    //         buttonWidth,
    //         0.05f * Screen.height);
    //     GUI.Label(textOutputRect, outputMessage);
    // }

    // Returns an ad request with custom ad targeting.
    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .AddTestDevice(AdRequest.TestDeviceSimulator)
            .AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
            .AddKeyword("game")
            .SetGender(Gender.Male)
            .SetBirthday(new DateTime(1985, 1, 1))
            .TagForChildDirectedTreatment(false)
            .AddExtra("color_bg", "9B30FF")
            .Build();
    }

    public void RequestBanner()
    {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Clean up banner ad before creating a new one.
        if (this.bannerView != null)
        {
            this.bannerView.Destroy();
        }

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);       //AdPosition.Center

        // Register for ad events.
        this.bannerView.OnAdLoaded += this.HandleAdLoaded;
        this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        this.bannerView.OnAdOpening += this.HandleAdOpened;
        this.bannerView.OnAdClosed += this.HandleAdClosed;
        this.bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;

        // Load a banner ad.
        this.bannerView.LoadAd(this.CreateAdRequest());
    }

    public void RequestInterstitial()
    {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Clean up interstitial ad before creating a new one.
        if (this.interstitial != null)
        {
            this.interstitial.Destroy();
        }

        // Create an interstitial.
        this.interstitial = new InterstitialAd(adUnitId);

        // Register for ad events.
        this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
        this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
        this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
        this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
        this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;

        // Load an interstitial ad.
        this.interstitial.LoadAd(this.CreateAdRequest());
    }

    public void CreateAndLoadRewardedAd()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        string adUnitId = "unexpected_platform";
#endif
        // Create new rewarded ad instance.
        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = this.CreateAdRequest();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    public void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
        else
        {
            MonoBehaviour.print("Interstitial is not ready yet");
            if( PlayerPrefs.GetInt("Continue_btn") == 1 )
            {
                InterstitialClose_fun();
                PlayerPrefs.SetInt("Continue_btn",0);
            }
            
        }
    }

    public void ShowRewardedAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
        else
        {
            MonoBehaviour.print("Rewarded ad is not ready yet");
        }
    }

    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeftApplication event received");
    }

    #endregion

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialLoaded event received");
        // _uGameController._LevelUp_continue.interactable = false;
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialOpened event received");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialClosed event received");
        if( PlayerPrefs.GetInt("Continue_btn") == 1 )
        {
            InterstitialClose_fun();
            PlayerPrefs.SetInt("Continue_btn",0);
        }       
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialLeftApplication event received");
    }

    #endregion

    #region RewardedAd callback handlers

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: " + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
        GameControllerScript _gcScript = FindObjectOfType<GameControllerScript>();
        if(PlayerPrefs.GetInt("_dailyads_rewards") == 1)
        {
            if(_dailyads_rewards == true)
            {
                /* Ad Timer */
                string end_starttime = System.DateTime.Now.ToString();
                string end_time = System.DateTime.Now.AddHours(8).ToString();
                PlayerPrefs.SetString("EndAdTimers",end_starttime);
                PlayerPrefs.SetString("end_time", end_time);
                PlayerPrefs.SetInt("adsActive",1);
                _gcScript._dailyAds.interactable = false;
                PlayerPrefs.SetInt("_dailyads_rewards",0);

                /*Ad Reward */
                _uGamePlay.CollectibleCount (5);        //3

                /* Daily reward popup */
                DailyReward_panel.SetActive(true);
                // StartCoroutine(RewardPanelNotify());
                _dailyads_rewards = false;
            }
        }
        else if( PlayerPrefs.GetInt("star_reward") == 1 )
        {
            if(claimreward_anim == true)
            {
                /*Ad Reward */
                // _uGamePlay.CollectibleCount (5);
                // _uGamePlay.TotalStar_collected.text = _uGamePlay.PowerUpTxt.text;
                _dailyads_rewards = false;
                _claimads_rewards = true;
                timer = 0;
                total_timer = 0;
                _gcScript._claimBtn.interactable = false;
                PlayerPrefs.SetInt("star_reward",0);
                claimreward_anim = false;
            }
        }
        else
        {
            Debug.LogError("For resume game from same");
            if(_resumegame_rewards == true)
            {
                Debug.LogError("REward_ads.." + _resumegame_rewards);
                AfterAdsStartPlay ();
                _resumegame_rewards = false;
                _gcScript._applicationPause = true;
                _dailyads_rewards = false;
                _gcScript.resumegame = false;
            }
        }
    }


    public void HandleUserEarnedReward(object sender, Reward args)
    {
        GameControllerScript _gcScript = FindObjectOfType<GameControllerScript>();
        if( _gcScript.dailyAds_reward == true )
        {
            _dailyads_rewards = true;
        }
        else if( _gcScript.star_reward == true )
        {
            claimreward_anim = true;
            _gcScript.Continue_bool = true;
        }
        else if( _gcScript.resumegame == true )
        {
            // _dailyads_rewards = true;
            _resumegame_rewards = true;
            Debug.LogError("resume game..." + _gcScript.resumegame);
        }
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
    }

    #endregion


    public void InterstitialClose_fun()
    {
         if (PlayerPrefs.GetInt ("SoundOn") == 0)
		{
			_uGamePlay.SoundHandle.transform.GetComponent<AudioSource> ().Play ();
		}

		if( _uGameController.Continue_bool == true )
		{
			_uGameController.Continue_bool = false;
		}
		else
		{
			// int collectibleCount = PlayerPrefs.GetInt("PowerUpCollect") + _wGameplay.Powerup_count;
			// PlayerPrefs.SetInt("PowerUpCollect",collectibleCount);
			// _wGameplay.PowerUpTxt.text = collectibleCount.ToString();
		}	

		_uGamePlay.Powerup_count = 0;
		_uGamePlay._CurrentPowerUp_count.text = _uGamePlay.Powerup_count.ToString();
		
		_uGamePlay.LevelUpPanel.SetActive(false);
        _uGamePlay.ScorePanel.SetActive(true);
        // _uGamePlay.Wall_obj.SetActive(true);
        for( int i = 0; i < _uGamePlay.Wall_obj.transform.childCount; i++ )
        {
            _uGamePlay.Wall_obj.gameObject.transform.GetChild(i).gameObject.SetActive(true);
            _uGamePlay.Wall_obj.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;            
        }
        _uGamePlay.player.SetActive(true);

		_uGamePlay.LevelPanel_Anim.SetActive(false);
		_uGamePlay.Level_SubPanel.SetActive(false);
		_uGamePlay._CurrentPowerUp_count.enabled = true;
		_uGamePlay._applicationPause = true;
		if (PlayerPrefs.GetString ("isTimeBaseGameRunnig") == "true") 
		{
			if (Gameplay.isTimeBaseFillbarOver == true) 
			{
				Gameplay.isTimeBaseFillbarOver = false;
				_uTimeGameplay.OnStopTimeCollectible ();

				Instantiate (_uGamePlay.LevelUpPaticle, _uGamePlay.player.transform.position, Quaternion.identity);
				// Instantiate(_wGameplay.VictoryParticle_1, new Vector3(_wGameplay.VictoryParticle_Pos1.transform.position.x, _wGameplay.VictoryParticle_Pos1.transform.position.y, _wGameplay.VictoryParticle_Pos1.transform.position.z), Quaternion.Euler(_wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.x, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.y, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.z) );

				_uGamePlay.isLevelContinue = true;

				// _uWall.OnBackGroundChange ();

//				for (int i1 = 0; i1 < _wGameplay.player.transform.childCount; i1++)
//				{
					_uGamePlay.player.transform.GetChild (0).gameObject.SetActive (false);
					//_wGameplay.player.transform.GetChild (0).GetChild (0).gameObject.SetActive (false);
					_uGamePlay.player.transform.GetChild (3).gameObject.SetActive (true);
					_uGamePlay.player.transform.GetChild (4).gameObject.SetActive (true);
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
			if( _uWall.currentRunningLevel >= 5 )
			{
				if (_uWall.currentRunningLevel%5 == 4 && PlayerPrefs.GetInt("GotBonusLevel")==0)
				{
					foreach (GameObject wGo in _uGamePlay.GeneratedEnimyClone) 
					{
						Destroy (wGo.gameObject);
					}

					PlayerPrefs.SetInt ("BonusLevelConti",1);

					Invoke ("OnBonusStartDelay",1.5f);
					_uGamePlay.StopEnemyAttack();

					_uGamePlay.StopKunaiWeaponCall ();
					_uGamePlay.StopGeneratedEnimy ();
					_uGamePlay.StopCollectible();
					_uGamePlay.StopuPKunaiWeaponCall ();
					_uGamePlay.StopSpikeWeaponCall ();
					_uGamePlay.OnPatternCancle ();
					// _uWall.OnBackGroundChange ();
				}

				else
				{
					Instantiate (_uGamePlay.LevelUpPaticle, _uGamePlay.player.transform.position, Quaternion.identity);
					// Instantiate(_wGameplay.VictoryParticle_1, new Vector3(_wGameplay.VictoryParticle_Pos1.transform.position.x, _wGameplay.VictoryParticle_Pos1.transform.position.y, _wGameplay.VictoryParticle_Pos1.transform.position.z), Quaternion.Euler(_wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.x, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.y, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.z) );					

					_uGamePlay.isLevelContinue = true;
					// _uWall.OnBackGroundChange ();

					_uGamePlay.player.transform.GetChild (0).gameObject.SetActive (false);
					_uGamePlay.player.transform.GetChild (3).gameObject.SetActive (true);
					_uGamePlay.player.transform.GetChild (4).gameObject.SetActive (true);

					_uGamePlay.isBonusLevelTime = false;
					PlayerPrefs.SetInt ("GotBonusLevel", 0);
					// PlayerPrefs.SetFloat ("CurrentLevel",PlayerPrefs.GetFloat("CurrentLevel")+1f);
					Invoke ("NotificationEnable",0.3f);
					Invoke ("NotificationDisable",1.0f);		//2
					Invoke("LevelChangedAnim",2.0f);		//1
					Invoke("Leveltext_delay",2.5f);

					if(PlayerPrefs.GetFloat ("CurrentLevel") > 25)
					{
                        Debug.LogError("currentLevel.." + PlayerPrefs.GetFloat ("CurrentLevel"));
						PlayerPrefs.SetFloat ("GenSpikeWeaponTime", 3.5f);
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//1.5
						_uGamePlay.OnPatternCancle ();
						_uGamePlay.StopSpikeWeaponCall ();
						_uGamePlay.StopuPKunaiWeaponCall ();
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
						_uGamePlay.StopSpikeWeaponCall ();
						_uGamePlay.StopuPKunaiWeaponCall ();
						Invoke("UpKunaiGen",4.5f);			//1.5
						// UpKunaiGen();
						Invoke("SpikeGen",4.5f);			//1.5
						// SpikeGen();
					} 

					else if (PlayerPrefs.GetFloat ("CurrentLevel") > 15 && PlayerPrefs.GetFloat ("CurrentLevel") < 21)
					{
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//2.5
						PlayerPrefs.SetFloat ("GenUpKunaiWeaponTime", 3.5f);
						_uGamePlay.StopuPKunaiWeaponCall ();
						Invoke("UpKunaiGen",4.5f);			//1.5
						// UpKunaiGen();
					} 

					else if (PlayerPrefs.GetFloat ("CurrentLevel") >= 6 && PlayerPrefs.GetFloat ("CurrentLevel") < 21)
					{
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//1

						_uGamePlay.StopKunaiWeaponCall ();
						// Invoke("KunaiGen",4.5f);			//1.5
						Invoke("KunaiGen",4.5f);
						// KunaiGen();
					}
					else if(PlayerPrefs.GetFloat ("CurrentLevel") < 6)
					{
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 3.5f);
					}

					_uGamePlay.StopEnemyAttack();
					_uGamePlay.StopGeneratedEnimy ();
					_uGamePlay.StopCollectible();
				}
			}
			else
			{
				if( _uWall.currentRunningLevel == 4 && PlayerPrefs.GetInt("GotBonusLevel")==0)
				{
					foreach (GameObject wGo in _uGamePlay.GeneratedEnimyClone) 
					{
						Destroy (wGo.gameObject);
					}

					PlayerPrefs.SetInt ("BonusLevelConti",1);

					Invoke ("OnBonusStartDelay",1.5f);
					_uGamePlay.StopEnemyAttack();

					_uGamePlay.StopKunaiWeaponCall ();
					_uGamePlay.StopGeneratedEnimy ();
					_uGamePlay.StopCollectible();
					_uGamePlay.StopuPKunaiWeaponCall ();
					_uGamePlay.StopSpikeWeaponCall ();
					_uGamePlay.OnPatternCancle ();
					// _uWall.OnBackGroundChange ();
				}
				else
				{
					Instantiate (_uGamePlay.LevelUpPaticle, _uGamePlay.player.transform.position, Quaternion.identity);
					// Instantiate(_wGameplay.VictoryParticle_1, new Vector3(_wGameplay.VictoryParticle_Pos1.transform.position.x, _wGameplay.VictoryParticle_Pos1.transform.position.y, _wGameplay.VictoryParticle_Pos1.transform.position.z), Quaternion.Euler(_wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.x, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.y, _wGameplay.VictoryParticle_Pos1.transform.localEulerAngles.z) );					

					_uGamePlay.isLevelContinue = true;
					// _uWall.OnBackGroundChange ();

					_uGamePlay.player.transform.GetChild (0).gameObject.SetActive (false);
					_uGamePlay.player.transform.GetChild (3).gameObject.SetActive (true);
					_uGamePlay.player.transform.GetChild (4).gameObject.SetActive (true);

					_uGamePlay.isBonusLevelTime = false;
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
						_uGamePlay.OnPatternCancle ();
						_uGamePlay.StopSpikeWeaponCall ();
						_uGamePlay.StopuPKunaiWeaponCall ();
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
						_uGamePlay.StopSpikeWeaponCall ();
						_uGamePlay.StopuPKunaiWeaponCall ();
						Invoke("UpKunaiGen",4.5f);			//1.5
						// UpKunaiGen();
						Invoke("SpikeGen",4.5f);			//1.5
						// SpikeGen();
					} 

					else if (PlayerPrefs.GetFloat ("CurrentLevel") > 15 && PlayerPrefs.GetFloat ("CurrentLevel") < 21)
					{
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//2.5
						PlayerPrefs.SetFloat ("GenUpKunaiWeaponTime", 3.5f);
						_uGamePlay.StopuPKunaiWeaponCall ();
						Invoke("UpKunaiGen",4.5f);			//1.5
						// UpKunaiGen();
					} 

					else if (PlayerPrefs.GetFloat ("CurrentLevel") >= 6 && PlayerPrefs.GetFloat ("CurrentLevel") < 21)
					{
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 4.0f);		//1

						_uGamePlay.StopKunaiWeaponCall ();
						// Invoke("KunaiGen",4.5f);			//1.5
						Invoke("KunaiGen",4.5f);
						// KunaiGen();
					}
					else if(PlayerPrefs.GetFloat ("CurrentLevel") < 6)
					{
						PlayerPrefs.SetFloat ("EnimyGenerateTime", 3.5f);
					}

					_uGamePlay.StopEnemyAttack();
					_uGamePlay.StopGeneratedEnimy ();
					_uGamePlay.StopCollectible();
				}
			}
		}
    }

    public void Leveltext_delay()
	{
		_uGamePlay.LevelText.enabled = true;
		_uGamePlay.CurrentLevel_text.enabled = true;
	}

    void SpikeGen()
	{
		_uGamePlay.SpikeWeaponCall ();
	}

	void KunaiGen()
	{
		_uGamePlay.KunaiWeaponCall ();
	}

	void UpKunaiGen()
	{
		_uGamePlay.UpKunaiWeaponCall ();
	}

	void OnPatternGen()
	{
		_uGamePlay.PatternWeaponCall ();
	}
    
    public void DisplayImageLerp(GameObject _referencedObj)
	{
        _referencedObj.SetActive(true);
        timer += Time.deltaTime;
		_referencedObj.transform.position = Vector2.Lerp (_referencedObj.transform.position, _targetPosition.transform.position, timer / 10.0f);
		// if(_referencedObj.transform.position == _targetPosition.transform.position)
		if(timer > 1)
        {
            _referencedObj.SetActive(false);
            _referencedObj.transform.position = claimReward_pos.transform.position;
            // _uGamePlay.CollectibleCount (5);
            _uGamePlay.Powerup_count = _uGamePlay.Powerup_count * 5;
            collectibleCount = PlayerPrefs.GetInt("PowerUpCollect") + _uGamePlay.Powerup_count;
            PlayerPrefs.SetInt("PowerUpCollect",collectibleCount);
            // _uGamePlay.PowerUpTxt.text = collectibleCount.ToString();
            _uGamePlay.TotalStar_collected.text = _uGamePlay.Powerup_count.ToString();
            _uGamePlay.CurrentLevel_star.text = _uGamePlay.TotalStar_collected.text;
            // _uGamePlay.TotalStar_collected.text = _uGamePlay.PowerUpTxt.text;
            _claimads_rewards = false;
            TotalClaim_stars = true;
		}
	}

    public void DisplayTotalStarLerp(GameObject _referencedObj)
    {
        //Debug.LogError("_referencedObj...."+ _referencedObj.transform.position + " _finalTargetPosition..." + _finalTargetPosition.transform.position);
        _uGamePlay.CurrentLevel_star.enabled = true;
        TotalReward_star.SetActive(true);
        _referencedObj.SetActive(true);
        total_timer += Time.deltaTime;
        _referencedObj.transform.position = Vector2.Lerp(_referencedObj.transform.position, _finalTargetPosition.transform.position, total_timer / 10.0f);
        if(total_timer > 1)
        {
            _referencedObj.SetActive(false);
            //_referencedObj.transform.position = _uGamePlay.TotalStar_collected.transform.position;
            _referencedObj.transform.position = _targetPosition.transform.position;
            _uGamePlay.PowerUpTxt.text = collectibleCount.ToString();
            TotalClaim_stars = false;
        }
    }

    void NotificationEnable()
	{
		// _wGameplay.LevelCompletePanel.SetActive (true);
	}

	void NotificationDisable()
	{
        Debug.LogError("Notification Disable called...");
		Wall.transform.GetComponent<Animation> ().Play ();
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

				_uGamePlay.player.transform.GetChild (0).gameObject.SetActive (true);
				_uGamePlay.player.transform.GetChild (0).GetChild (0).gameObject.SetActive (true);
				_uGamePlay.player.transform.GetChild (3).gameObject.SetActive (false);
				_uGamePlay.player.transform.GetChild (4).gameObject.SetActive (false);
		//	}

			_uGamePlay.isLevelContinue = false;

		//	_wGameplay.StartGeneratingEnemy ();
			_uGamePlay.CollectibleStart ();
			_uWall.OnWallRotationSpeed();
			// _wGameplay.LevelCompletePanel.SetActive (false);
		}

		else if(PlayerPrefs.GetString ("isTimeBaseGameRunnig") == "true")
		{
//			for(int i1 = 0; i1 < _wGameplay.player.transform.childCount; i1++)// (int i1 = 0; i1 < _wGameplay.NinjaEye.Count; i1++)
//			{
				_uGamePlay.player.transform.GetChild (0).gameObject.SetActive (true);
				_uGamePlay.player.transform.GetChild (0).GetChild (0).gameObject.SetActive (true);
				_uGamePlay.player.transform.GetChild (3).gameObject.SetActive (false);
				_uGamePlay.player.transform.GetChild (4).gameObject.SetActive (false);
		//	}
			_uGamePlay.isLevelContinue = false;
			_uTimeGameplay.OnTimeModeGame ();
			_uWall.OnWallRotationSpeed();
			_uGamePlay.LevelCompletePanel.SetActive (false);
		}
	}

	void LevelChangedAnim()
	{
        Debug.LogError("LevelChanged Anim Panel Called...");
		_uGamePlay.LevelChange ();
	}

    public void demobtn()
    {
        GameControllerScript _gcScript = FindObjectOfType<GameControllerScript>();
        _claimads_rewards = true;
        timer = 0;
        total_timer = 0;
        _gcScript.Continue_bool = true;
    }

    IEnumerator RewardPanelNotify()
    {
        yield return new WaitForSeconds(4.0f);
        DailyReward_panel.SetActive(false);
    }

    public void AfterAdsStartPlay()
	{
		Time.timeScale = 1;
        Debug.LogError("Game Over..." + _uGamePlay.isGameOver + "  isLevelContinue..." + _uGamePlay.isLevelContinue);
        var rendererComponents = _uGamePlay.player.GetComponentsInChildren<SpriteRenderer>(true);

		foreach (var component in rendererComponents)
		{
			Color tmp = component.color;
			tmp.a = 1;
			component.color = tmp;
		}
        Debug.LogError("GAme Over 1");
		// _uGamePlay.GameOverPanel.SetActive (false);
        _uGamePlay.GameContinuePopup.SetActive (false);
        _uGamePlay.player.SetActive(true);
		_uGamePlay.ScorePanel.SetActive(true);
		_uGamePlay.Wall_obj.SetActive(true);
        Debug.LogError("GAme Over 2");
		_uGamePlay.StartPlay ();
	}

	public void OnRewardBasePlay()
	{
		if (PlayerPrefs.GetString ("isNormalGameRunning") == "true") 
		{

			for (int i1 = 0; i1 < _uGamePlay.player.transform.childCount; i1++) // (int i1 = 0; i1 < _uGamePlay.NinjaEye.Count; i1++)
			{
				_uGamePlay.player.transform.GetChild (0).gameObject.SetActive (true);
				_uGamePlay.player.transform.GetChild (0).GetChild (0).gameObject.SetActive (true);
				_uGamePlay.player.transform.GetChild (1).gameObject.SetActive (false);
				_uGamePlay.player.transform.GetChild (2).gameObject.SetActive (false);
				_uGamePlay.player.transform.GetChild (3).gameObject.SetActive (false);
				_uGamePlay.player.transform.GetChild (4).gameObject.SetActive (false);
				_uGamePlay.player.transform.GetChild (5).gameObject.SetActive (false);
				_uGamePlay.player.transform.GetChild (6).gameObject.SetActive (false);
                // if( _uGamePlay.player.gameObject.name != "PigPlayer" || _uGamePlay.player.gameObject.name != "NinjaPlayer" || _uGamePlay.player.gameObject.name != "OwlPlayer" || _uGamePlay.player.gameObject.name != "BirdPlayer" )
                if( _uGamePlay.player.gameObject.name != "PigPlayer" || _uGamePlay.player.gameObject.name != "NinjaPlayer" || _uGamePlay.player.gameObject.name != "OwlPlayer" || _uGamePlay.player.gameObject.name != "BirdPlayer" || _uGamePlay.player.gameObject.name == "HippoPlayer" )
				{
					
				}
				else
				{					
					_uGamePlay.player.transform.GetChild (7).gameObject.SetActive (false);
					_uGamePlay.player.transform.GetChild (8).gameObject.SetActive (true);
				}
                
			}

			_uGamePlay.CanvasRef.transform.GetComponent<Canvas> ().renderMode = RenderMode.ScreenSpaceCamera;
			_uGamePlay.isLevelContinue = false;
			// BgPos.transform.position = _uBGColour.StartingPos;
			_uGamePlay.GameOverPanel.SetActive (false);
			_uGamePlay.OnGameStart ();

			_uGamePlay.AdsFillBar.GetComponent<Image> ().fillAmount = 1;
			_uGamePlay.AdsFillBar.transform.GetChild (0).GetComponent<Button> ().interactable = true;
			PlayerPrefs.SetInt ("ShowAds", 0);

		}

		else if (PlayerPrefs.GetString ("isTimeBaseGameRunnig") == "true") 
		{
			_uGamePlay.player.transform.GetChild (0).gameObject.SetActive (true);
			_uGamePlay.player.transform.GetChild (0).GetChild (0).gameObject.SetActive (true);
			_uGamePlay.player.transform.GetChild (5).gameObject.SetActive (false);
			_uGamePlay.player.transform.GetChild (6).gameObject.SetActive (false);
            // if( _uGamePlay.player.gameObject.name != "PigPlayer" || _uGamePlay.player.gameObject.name != "NinjaPlayer" || _uGamePlay.player.gameObject.name != "OwlPlayer" || _uGamePlay.player.gameObject.name != "BirdPlayer" )
            if( _uGamePlay.player.gameObject.name != "PigPlayer" || _uGamePlay.player.gameObject.name != "NinjaPlayer" || _uGamePlay.player.gameObject.name != "OwlPlayer" || _uGamePlay.player.gameObject.name != "BirdPlayer" || _uGamePlay.player.gameObject.name == "HippoPlayer" )
            {
                
            }
            else
            {					
                _uGamePlay.player.transform.GetChild (7).gameObject.SetActive (false);
                _uGamePlay.player.transform.GetChild (8).gameObject.SetActive (true);
            }         

			_uGamePlay.CanvasRef.transform.GetComponent<Canvas> ().renderMode = RenderMode.ScreenSpaceCamera;
			_uGamePlay.isLevelContinue = false;
			// BgPos.transform.position = _uBGColour.StartingPos;
			_uGamePlay.GameOverPanel.SetActive (false);
			_uGamePlay.isGameStart = true;

			_uGamePlay.AdsFillBar.GetComponent<Image> ().fillAmount = 1;
			_uGamePlay.AdsFillBar.transform.GetChild (0).GetComponent<Button> ().interactable = true;
			PlayerPrefs.SetInt ("ShowAds", 0);
		}

		else
		{
		}
	}   
    

}
