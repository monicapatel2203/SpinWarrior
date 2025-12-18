using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSelectedChar : MonoBehaviour 
{

	void OnEnable()
	{
		string charName = GameObject.Find(PlayerPrefs.GetString ("SelectedPlayerName")).ToString();
		charName = charName.Substring(0, charName.Length - 31);
		GameObject.Find(charName).transform.GetComponent<Animation>().Play("SelectedCharAnim"); 
		Debug.LogError("OnEnable CharName...." + GameObject.Find(charName).name );
	}

	public void Privacypolicy()
	{
		Application.OpenURL("http://www.invisiblefiction.com/privacy-policy.html");
	}

	// void Start () 
	// {
		
	// }
	
	// void Update () 
	// {
		
	// }
}
