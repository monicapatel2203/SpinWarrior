using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.Advertisements;

//[RequireComponent(typeof(Button))]
public class UnityAdsButton : MonoBehaviour
{

	void Start ()
	{

	}

	void Update ()
	{

	}


	public void ShowAdPlacement()
	{
		if (Advertisement.IsReady ("rewardedVideo"))
		{
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show ("rewardedVideo", options);
		}
	}


	private void HandleShowResult(UnityEngine.Advertisements.ShowResult result)
	{
		switch (result) 
		{

		case ShowResult.Finished:
		Debug.Log ("The ad was finished.");
			break;

		case ShowResult.Skipped:
			Debug.Log ("The ad was skipped before reaching the end.");
			break;

		case ShowResult.Failed:
			Debug.LogError ("The ad failed to be shown.");
			break;
		}
	}
}