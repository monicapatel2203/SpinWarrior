using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisText : MonoBehaviour
{
	public GameObject interNetCheck , AdsBtn , iScontinuetxt;
	Gameplay _dGamePlay;
	float AdsTimer;

	void Start()
	{
		_dGamePlay = FindObjectOfType<Gameplay> ();
	}

	void Update()
	{
		if(_dGamePlay.isAdsRunnig)
		{
			_dGamePlay.isAdsRunnig = false;
			if ((PlayerPrefs.GetString ("isNormalGameRunning") == "true")) 
			{
				if (PlayerPrefs.GetFloat ("AdsInNorCurrentLevel") == 0f)
				{
					AdsBtn.SetActive (true);
					iScontinuetxt.SetActive (true);
				}

				else
				{
					AdsBtn.SetActive (false);
					iScontinuetxt.SetActive (false);
				}
			}

			else
			{
				if (PlayerPrefs.GetFloat ("AdsInTimerCurrentLevel") == 0f)
				{
					AdsBtn.SetActive (true);
					iScontinuetxt.SetActive (true);
				}

				else
				{
					AdsBtn.SetActive (false);
					iScontinuetxt.SetActive (false);
				}
			}
		}

		if (PlayerPrefs.GetInt ("isNetNotAvail") == 1)
		{
			AdsTimer = AdsTimer + 0.05f;
			interNetCheck.SetActive (true);
			//Invoke ("NetC", 0.9f);

			if (AdsTimer >= 3)
			{
				AdsTimer = 0;
				interNetCheck.SetActive (false);
				PlayerPrefs.SetInt ("isNetNotAvail", 0);
			}
		}
	}

	void NetC()
	{
		interNetCheck.SetActive (false);
		Debug.LogError ("Netc");
	}
}