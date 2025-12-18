using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTimeCheck : MonoBehaviour 
{
	int count;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

	public void DateCheck()
	{
		System.DateTime dFrom;
		System.DateTime dTo;
	
		string sDateFrom = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
		string sDateTo = PlayerPrefs.GetString("QuitTime");

		if (System.DateTime.TryParse(sDateFrom, out dFrom) && System.DateTime.TryParse(sDateTo, out dTo))
		{
			System.TimeSpan TS = dFrom - dTo;// dTo - dFrom;
			int hour = TS.Hours;
			float mins = TS.Minutes;
			float secs = TS.Seconds;
			int day = TS.Days;
			Debug.Log (day + ": " + hour +" : " + mins + " : "+ secs);

			if (day >= 1)
			{
				PlayerPrefs.SetInt ("Count",1);
			}
			else if (mins >= 3) {
				PlayerPrefs.SetInt ("Count",count++);
			}
			else
				return;
		}
	}
}