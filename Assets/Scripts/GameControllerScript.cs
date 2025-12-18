using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameControllerScript : MonoBehaviour
{
    public Button _dailyAds,_claimBtn,_LevelUp_continue;
	public bool _Adsactive;
	public GameObject _AdsTimer;
	// public Image _AdsImage;
	System.DateTime EndTimer;
	System.DateTime StartTimer;
	
	// public GameObject PausePanel;
	public float AdsTimer_seconds;
	public bool _applicationPause;
	// public GameObject AdsRemoved_Panel;
	public bool dailyAds_reward,star_reward,resumegame,Continue_bool;
	float _TimerValue,_DayValue;
    public GameObject LevelFillBar;
    public GameObject InternetCheckPanel_home;
    public GameObject InternetPanel,QuitPanel,QuitPanel_Ads;
  
    void Awake()
    {
        Camera.main.GetComponent<GoogleMobileAdsDemoScript>().RequestInterstitial();
    }
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        dailyAds_reward = false;
        star_reward = false;
        resumegame = false;
        Continue_bool = false;
        QuitPanel.SetActive(false);
        QuitPanel_Ads.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        AdsTimerFunction();

		if(PlayerPrefs.GetInt("adsActive") == 1)
		{
			// _AdsImage.enabled = true;
			_AdsTimer.SetActive(true);
		}
		else if(PlayerPrefs.GetInt("adsActive") == 2)
		{
			if (_TimerValue > 0 && _TimerValue < 8.0f) 
			{
				if(_TimerValue == 0)
				{
					// _AdsImage.enabled = true;
					_AdsTimer.SetActive(true);
				}
				else
				{
					// _AdsImage.enabled = true;
					_AdsTimer.SetActive(true);
				}
			} 
			else 
			{
				if(_DayValue >= 1)
				{
					// _AdsImage.enabled = false;
					_AdsTimer.SetActive(false);
				}
				else
				{
					if(_TimerValue == 0)
					{
						// _AdsImage.enabled = true;
						_AdsTimer.SetActive(true);
						
					}
					else
					{
						// _AdsImage.enabled = false;
						_AdsTimer.SetActive(false);
					}		
				}
			}
		}		
		
        if(string.IsNullOrEmpty(PlayerPrefs.GetString("EndAdTimers")))
        {
            
        }
        else
        {

            string convertadtime1 = PlayerPrefs.GetString("end_time");                //"10/5/2019 5:06:35 PM";//PlayerPrefs.GetString("EndAdTimer");
            
            StartTimer = System.DateTime.Now;
            string[] dt = convertadtime1.Split(' ');
            System.DateTime da1 = System.DateTime.Parse(convertadtime1);
            System.DateTime endtime;
            endtime = da1;

            System.TimeSpan LeftTime = endtime .Subtract(StartTimer);
            System.String yourString = string.Format("{0}:{1}:{2}", Mathf.Abs(LeftTime.Hours).ToString("00"), Mathf.Abs(LeftTime.Minutes).ToString("00"), Mathf.Abs(LeftTime.Seconds).ToString("00"));
            _AdsTimer.GetComponent<Text>().text = yourString;

        }
    }

    public void ShowAdPlacement()
    {
        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            InternetCheckPanel_home.SetActive(true);
            //StartCoroutine(InternetPanel_resume());
        }
        else
        {
            resumegame = true;
            dailyAds_reward = false;
            star_reward = false;
            PlayerPrefs.SetInt("_dailyads_rewards",0);
            Camera.main.GetComponent<GoogleMobileAdsDemoScript>().ShowRewardedAd();
            Camera.main.GetComponent<GoogleMobileAdsDemoScript>().CreateAndLoadRewardedAd();
        }
    }


    public void HomeBtnAd()
    {
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
        Camera.main.GetComponent<GoogleMobileAdsDemoScript>().ShowInterstitial();
        Camera.main.GetComponent<GoogleMobileAdsDemoScript>().RequestInterstitial();
    }

    public void DailyAds()
	{
		if( Application.internetReachability == NetworkReachability.NotReachable )
		{
            InternetCheckPanel_home.SetActive(true);
            //StartCoroutine(InternetClose_Panel_home(3.0f));
		}
		else
		{
			dailyAds_reward = true;
            star_reward = false;
            resumegame = false;
			_Adsactive = true;
            PlayerPrefs.SetInt("_dailyads_rewards",1);
			// PlayerPrefs.SetInt("adsActive",1);
			// string end_starttime = System.DateTime.Now.ToString();
			// string end_time = System.DateTime.Now.AddHours(8).ToString();
			// PlayerPrefs.SetString("EndAdTimers",end_starttime);
			// PlayerPrefs.SetString("end_time", end_time);
			// PlayerPrefs.SetInt("adsActive",1);
            // Gameplay _ugamePlay = FindObjectOfType<Gameplay>();
            // _ugamePlay.CollectibleCount (3);                                //To get 3 powerup after watching ad

			GoogleMobileAdsDemoScript _googleAds = FindObjectOfType<GoogleMobileAdsDemoScript>();
            Camera.main.GetComponent<GoogleMobileAdsDemoScript>().ShowRewardedAd();
            Camera.main.GetComponent<GoogleMobileAdsDemoScript>().CreateAndLoadRewardedAd();
        }
	}

    	public void AdsTimerFunction()
        {
            float final_value;

            if(string.IsNullOrEmpty(PlayerPrefs.GetString("EndAdTimers")))
            {
                
            }
            else
            {
        
            string convertadtime = PlayerPrefs.GetString("EndAdTimers");				//"10/5/2019 5:06:35 PM";//PlayerPrefs.GetString("EndAdTimer");
            
            
            System.DateTime startTime = System.DateTime.Now;     			//  10/5/2019 4:59:25 PM
            
            string [] dt = convertadtime.Split(' ');		

            System.DateTime da = System.DateTime.Parse(convertadtime);

            System.DateTime endTime = da;								//System.DateTime.Now.AddHours( 5 );

            System.DateTime btnClickTime = new System.DateTime(endTime.Year, endTime.Month, endTime.Day, endTime.Hour, endTime.Minute, endTime.Second);
            System.DateTime currentSystemTime = new System.DateTime(startTime.Year, startTime.Month, startTime.Day, startTime.Hour, startTime.Minute, startTime.Second);
                  
            System.TimeSpan span = currentSystemTime.Subtract ( btnClickTime );
            
            System.String yourString = string.Format("{0}d {1}h {2}m {3}s", span.Days, span.Hours, span.Minutes, span.Seconds);

            
            float seconds_sec = Mathf.Abs(span.Seconds * 1000);
            float minutes_min = Mathf.Abs(span.Minutes * 60000);
            float hours_hour = Mathf.Abs(span.Hours * 3600000);
            float days_day = Mathf.Abs(span.Days * 86400000);		
            
            final_value = seconds_sec + minutes_min + hours_hour + days_day;
            _TimerValue = span.Hours;
            _DayValue = span.Days;
            // Debug.Log ("yourString...." + yourString + "final_value...." + final_value);

            if (span.Hours < 0) {
                _dailyAds.interactable = true;
                PlayerPrefs.SetInt ("adsActive", 2);
            } 
            else 
            {
                if(final_value >= 28800000 )
                {
                    _dailyAds.interactable = true;
                    PlayerPrefs.SetInt("adsActive",2);
                }
                else
                {
                    _dailyAds.interactable = false;
                    _Adsactive = false;
                }
            }
        }

	}

        public void StarEarned()
        {
            if( Application.internetReachability == NetworkReachability.NotReachable )
            {
                InternetCheckPanel_home.SetActive(true);
            }
            else
            {
                star_reward = true;
                dailyAds_reward = false;
                resumegame = false;
                Camera.main.GetComponent<GoogleMobileAdsDemoScript>().ShowRewardedAd();
                Camera.main.GetComponent<GoogleMobileAdsDemoScript>().CreateAndLoadRewardedAd();
                PlayerPrefs.SetInt("star_reward",1);
            }
        }

        IEnumerator InternetClose_Panel_home(float delay)
		{
			yield return new WaitForSeconds(delay);
			InternetCheckPanel_home.SetActive(false);
		}

        IEnumerator InternetPanel_resume()
        {         
            yield return new WaitForSeconds(3.0f);
            InternetPanel.SetActive(false);         
        }

    public void QuitBtn()
    {
        QuitPanel.SetActive(true);
        // if(Application.internetReachability == NetworkReachability.NotReachable)
        // {
        //     QuitPanel.SetActive(true);
        // }
        // else
        // {
        //     if (PlayerPrefs.GetInt ("NoAd") == 1) 
		// 	{
		// 		QuitPanel.SetActive (true);
		// 	}
		// 	else 
		// 	{
		// 		QuitPanel_Ads.SetActive (true);
		// 		Camera.main.GetComponent<GoogleMobileAdsDemoScript> ().RequestBanner ();
		// 	}
        // }        
    }

    public void QuitGame()
    {
        StartCoroutine(ApplicationQuit(1.0f));
    }

    public void GameContinue()
    {
        QuitPanel.SetActive(false);
        Gameplay _gplayScript = FindObjectOfType<Gameplay>();        
        // if(Application.internetReachability == NetworkReachability.NotReachable)
        // {
        //     QuitPanel.SetActive(false);
        // }
        // else
        // {
		// 	if (PlayerPrefs.GetInt ("NoAd") == 1) 
		// 	{
		// 		QuitPanel.SetActive (false);
		// 	}
		// 	else 
		// 	{
		// 		QuitPanel_Ads.SetActive(false);
		// 		Camera.main.GetComponent<GoogleMobileAdsDemoScript>().bannerView.Destroy();
		// 		PlayerPrefs.SetInt("BannerAds", 0);	
		// 	}
        // }        
        _gplayScript.BottomPatternPanel.SetActive(true);
    }

    IEnumerator ApplicationQuit( float delay )
    {
        yield return new WaitForSeconds(delay);
        Application.Quit ();
    }

}
