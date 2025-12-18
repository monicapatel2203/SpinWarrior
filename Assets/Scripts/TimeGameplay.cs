using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeGameplay : MonoBehaviour 
{
	public GameObject collectiblesClonePos , clonePos;

	GameObject ShuriWeaponClone ,  collectiblesClone;

	public List<GameObject> timeModeWeapon = new List<GameObject> ();
	public List<GameObject> ShuriClone = new List<GameObject> ();
	public List<Sprite> CrackWallParts = new List<Sprite> ();
	public List<GameObject> TimeModeCollectibles = new List<GameObject> ();
	public List<GameObject> GeneratedPoweerupClone = new List<GameObject> ();

	int wallPrevValue;
	Gameplay _TgamePlay;

	void Start () 
	{
		_TgamePlay = FindObjectOfType<Gameplay> ();
	}
	
	// void Update () 
	// {
		
	// }

	public void OnTimeModeGame()
	{
		StartCoroutine (OnRandomEnimyGen());
		_TgamePlay.isGameStart = true;
		InvokeRepeating ("OnCollectibleGenerate",5f,8f);
	}

	IEnumerator OnRandomEnimyGen()
	{
		if ((PlayerPrefs.GetFloat ("TimeCurrentLevel") >= 1f) && (PlayerPrefs.GetFloat ("TimeCurrentLevel") < 6f)) 
		{
			for (int s = 0; s < 4; s++) 
			{
				yield return new WaitForSeconds (0.8f);

				int TempPH = Random.Range (0, clonePos.transform.childCount);
				ShuriWeaponClone = Instantiate (timeModeWeapon [s], clonePos.transform.GetChild (TempPH).transform.localPosition, Quaternion.identity);
				ShuriWeaponClone.transform.localPosition = clonePos.transform.GetChild (TempPH).transform.localPosition;
				ShuriWeaponClone.AddComponent<TimeWeaponMovement> ();
				ShuriClone.Add (ShuriWeaponClone);
			}
		}

		else if ((PlayerPrefs.GetFloat ("TimeCurrentLevel") > 5f) && (PlayerPrefs.GetFloat ("TimeCurrentLevel") < 11f))
		{
			for (int s = 3; s < 7; s++) 
			{
				yield return new WaitForSeconds (0.8f);

				int TempPH = Random.Range (0, clonePos.transform.childCount);

				wallPrevValue = TempPH;
				ShuriWeaponClone = Instantiate (timeModeWeapon [s], clonePos.transform.GetChild (TempPH).transform.localPosition, Quaternion.identity);
				ShuriWeaponClone.transform.localPosition = clonePos.transform.GetChild (TempPH).transform.localPosition;
				ShuriWeaponClone.AddComponent<TimeWeaponMovement> ();
				ShuriClone.Add (ShuriWeaponClone);
			}
		}

		else if ((PlayerPrefs.GetFloat ("TimeCurrentLevel") > 11f) && (PlayerPrefs.GetFloat ("TimeCurrentLevel") < 16f))
		{
			for (int s = 7; s < 11; s++) 
			{
				yield return new WaitForSeconds (0.8f);

				int TempPH = Random.Range (0, clonePos.transform.childCount);

				wallPrevValue = TempPH;
				ShuriWeaponClone = Instantiate (timeModeWeapon [s], clonePos.transform.GetChild (TempPH).transform.localPosition, Quaternion.identity);
				ShuriWeaponClone.transform.localPosition = clonePos.transform.GetChild (TempPH).transform.localPosition;
				ShuriWeaponClone.AddComponent<TimeWeaponMovement> ();
				ShuriClone.Add (ShuriWeaponClone);
			}
		}

		else
		{
			for (int s = 9; s < 15; s++) 
			{

				Debug.Log ("Level Unlimited");

				yield return new WaitForSeconds (0.8f);

				int TempPH = Random.Range (0, clonePos.transform.childCount);
				ShuriWeaponClone = Instantiate (timeModeWeapon [s], clonePos.transform.GetChild (TempPH).transform.localPosition, Quaternion.identity);
				ShuriWeaponClone.transform.localPosition = clonePos.transform.GetChild (TempPH).transform.localPosition;
				ShuriWeaponClone.AddComponent<TimeWeaponMovement> ();
				ShuriClone.Add (ShuriWeaponClone);
			}
		}
	}

	public void OnCollectibleGenerate()
	{
		int coleectibleNo = Random.Range (0,collectiblesClonePos.transform.childCount);
		collectiblesClone = Instantiate (_TgamePlay.PowerUp[0] , collectiblesClonePos.transform.GetChild(coleectibleNo).transform.localPosition,Quaternion.identity);
		collectiblesClone.transform.localPosition = collectiblesClonePos.transform.GetChild (coleectibleNo).transform.localPosition;

		collectiblesClone.transform.tag = "PowerUp";

		ShuriClone.Add (collectiblesClone);
		GeneratedPoweerupClone.Add(collectiblesClone);
		collectiblesClone.AddComponent<PowerUp> ();
	}

	public void OnStopTimeCollectible()
	{
		CancelInvoke ("OnCollectibleGenerate");
		foreach( GameObject g in GeneratedPoweerupClone )
		{
			Destroy(g);
		}
	}

	public void DesEnemyAttack()
	{
		foreach (GameObject g in ShuriClone) 
		{
			Destroy (g);
		}
	}
}