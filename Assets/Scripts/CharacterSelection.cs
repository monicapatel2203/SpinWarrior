using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompleteProject;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour 
{
	public List<Sprite> CharSprite = new List<Sprite>();

	public List<Sprite> UnLockedCharacter = new List<Sprite>();

	public GameObject CharacterSelectPanel , InAPpurchasepanel;
	Gameplay _cGameplay;
	string selectedPlayerName_1;
	//Purchaser _cPurchaser;

	void Start () 
	{
		_cGameplay = FindObjectOfType<Gameplay> ();
		//_cPurchaser = FindObjectOfType<Purchaser> ();
	}
	
	// void Update ()
	// {
	// }

	public void AlreadyUnlockedChar()
	{

		if (PlayerPrefs.GetString ("BearUnlocked") == "Bear" && GameObject.Find (PlayerPrefs.GetString ("BearUnlocked")).transform.childCount == 1)
		{
			Destroy (GameObject.Find (PlayerPrefs.GetString ("BearUnlocked")).transform.GetChild (0).gameObject);
		}

		// if (PlayerPrefs.GetString ("CowUnlocked") == "Cow" && GameObject.Find (PlayerPrefs.GetString ("CowUnlocked")).transform.childCount == 1)
		// {
		// 	Destroy (GameObject.Find (PlayerPrefs.GetString ("CowUnlocked")).transform.GetChild (0).gameObject);
		// }

		if (PlayerPrefs.GetString ("OwlUnlocked") == "Owl" && GameObject.Find (PlayerPrefs.GetString ("OwlUnlocked")).transform.childCount == 1)
		{
			Destroy (GameObject.Find (PlayerPrefs.GetString ("OwlUnlocked")).transform.GetChild (0).gameObject);
		}		

		if (PlayerPrefs.GetString ("DogUnlocked") == "Dog" && GameObject.Find (PlayerPrefs.GetString ("DogUnlocked")).transform.childCount == 1)
		{
			Destroy (GameObject.Find (PlayerPrefs.GetString ("DogUnlocked")).transform.GetChild (0).gameObject);
		}

		// if (PlayerPrefs.GetString ("FrogUnlocked") == "Frog" && GameObject.Find (PlayerPrefs.GetString ("FrogUnlocked")).transform.childCount == 1)
		// {
		// 	Destroy (GameObject.Find (PlayerPrefs.GetString ("FrogUnlocked")).transform.GetChild (0).gameObject);
		// }
		
		// if (PlayerPrefs.GetString ("BirdUnlocked") == "Bird" && GameObject.Find (PlayerPrefs.GetString ("BirdUnlocked")).transform.childCount == 1)
		// {
		// 	Destroy (GameObject.Find (PlayerPrefs.GetString ("BirdUnlocked")).transform.GetChild (0).gameObject);
		// }		

		if (PlayerPrefs.GetString ("LionUnlocked") == "Lion" && GameObject.Find (PlayerPrefs.GetString ("LionUnlocked")).transform.childCount == 1)
		{
			Destroy (GameObject.Find (PlayerPrefs.GetString ("LionUnlocked")).transform.GetChild (0).gameObject);
		}

		if (PlayerPrefs.GetString ("MonkeyUnlocked") == "Monkey" && GameObject.Find (PlayerPrefs.GetString ("MonkeyUnlocked")).transform.childCount == 1)
		{
			Destroy (GameObject.Find (PlayerPrefs.GetString ("MonkeyUnlocked")).transform.GetChild (0).gameObject);
		}

		if (PlayerPrefs.GetString ("PigUnlocked") == "Pig" && GameObject.Find (PlayerPrefs.GetString ("PigUnlocked")).transform.childCount == 1)
		{
			Destroy (GameObject.Find (PlayerPrefs.GetString ("PigUnlocked")).transform.GetChild (0).gameObject);
		}

		// if (PlayerPrefs.GetString ("SheepUnlocked") == "Sheep" && GameObject.Find (PlayerPrefs.GetString ("SheepUnlocked")).transform.childCount == 1)
		// {
		// 	Destroy (GameObject.Find (PlayerPrefs.GetString ("SheepUnlocked")).transform.GetChild (0).gameObject);
		// }

		if (PlayerPrefs.GetString ("HippoUnlocked") == "Hippo" && GameObject.Find (PlayerPrefs.GetString ("HippoUnlocked")).transform.childCount == 1)
		{
			Destroy (GameObject.Find (PlayerPrefs.GetString ("HippoUnlocked")).transform.GetChild (0).gameObject);
		}

		if (PlayerPrefs.GetString ("TigerUnlocked") == "Tiger" && GameObject.Find (PlayerPrefs.GetString ("TigerUnlocked")).transform.childCount == 1)
		{
			Destroy (GameObject.Find (PlayerPrefs.GetString ("TigerUnlocked")).transform.GetChild (0).gameObject);
		}
	}


	public void OnCharacterSelection(int charValue)
	{
		
		string charName = GameObject.Find (PlayerPrefs.GetString ("SelectedPlayerName")).ToString ();
		Debug.LogError("Charname..." + PlayerPrefs.GetString ("SelectedPlayerName"));
		charName = charName.Substring (0, charName.Length - 31);

		GameObject.Find(charName).GetComponent<Animation>().Stop();
		GameObject.Find (charName).transform.localScale = new Vector3 (1, 1, 1);
	
		// _cGameplay.CanvasRef.transform.GetComponent<Canvas> ().renderMode = RenderMode.ScreenSpaceCamera;
		_cGameplay.LastSelectedChar.SetActive (false);
		PlayerPrefs.SetInt ("SelectedCharacter", charValue);

		_cGameplay.Character [PlayerPrefs.GetInt ("SelectedCharacter")].SetActive (true);
		_cGameplay.LastSelectedChar = _cGameplay.Character [PlayerPrefs.GetInt ("SelectedCharacter")];

		// CharacterSelectPanel.SetActive (false);
	}

	public void OnSelectedPlayerName(string playerName)
	{
		PlayerPrefs.SetString ("SelectedPlayerName", playerName);
		selectedPlayerName_1 = GameObject.Find (PlayerPrefs.GetString ("SelectedPlayerName")).ToString ();
		selectedPlayerName_1 = selectedPlayerName_1.Substring(0, selectedPlayerName_1.Length - 31);
		GameObject.Find(selectedPlayerName_1).GetComponent<Animation>().Play("SelectedCharAnim");
	}

	public void OnUnlockChar(string LockedChar)
	{
		switch (LockedChar)
		{
		case "Bear":
			{

				//PlayerPrefs.SetString ("UnLockingChar" , LockedChar);

				if (PlayerPrefs.GetInt ("PowerUpCollect") >= 300) 		//100
				{
					PlayerPrefs.SetInt ("PowerUpCollect", PlayerPrefs.GetInt ("PowerUpCollect") - 300);	//100

					_cGameplay.SelectCharStar.GetComponent<Text> ().text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();

					_cGameplay.PowerUpTxt.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					PlayerPrefs.SetString ("BearUnlocked", LockedChar);
					Destroy (GameObject.Find (LockedChar).transform.GetChild (0).gameObject);
					Debug.LogError ("Bear Char.............!"+PlayerPrefs.GetString ("BearUnlocked"));
					//GameObject.Find (LockedChar).transform.GetChild (0).gameObject.SetActive (false);
				} 
				else 
				{
					_cGameplay.PowerUpTxt_InApp.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					InAPpurchasepanel.SetActive (true);
				}
				break;
			}

		// case "Cow":
		// 	{
		// 		if (PlayerPrefs.GetInt ("PowerUpCollect") >= 100)			//10
		// 		{
		// 			PlayerPrefs.SetInt ("PowerUpCollect", PlayerPrefs.GetInt ("PowerUpCollect") - 100);	//10
		// 			_cGameplay.SelectCharStar.GetComponent<Text> ().text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
		// 			_cGameplay.PowerUpTxt.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
		// 			PlayerPrefs.SetString ("CowUnlocked", LockedChar);
		// 			Destroy (GameObject.Find (LockedChar).transform.GetChild (0).gameObject);
		// 			//GameObject.Find (LockedChar).transform.GetChild (0).gameObject.SetActive (false);
		// 		}
		// 		else 
		// 		{
		// 			_cGameplay.PowerUpTxt_InApp.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
		// 			InAPpurchasepanel.SetActive (true);
		// 		}
		// 		break;
		// 	}

		case "Owl":
			{
				if (PlayerPrefs.GetInt ("PowerUpCollect") >= 300)			//100
				{
					PlayerPrefs.SetInt ("PowerUpCollect", PlayerPrefs.GetInt ("PowerUpCollect") - 300);	//100
					_cGameplay.SelectCharStar.GetComponent<Text> ().text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					_cGameplay.PowerUpTxt.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					PlayerPrefs.SetString ("OwlUnlocked", LockedChar);
					Destroy (GameObject.Find (LockedChar).transform.GetChild (0).gameObject);
					//GameObject.Find (LockedChar).transform.GetChild (0).gameObject.SetActive (false);
				}
				else 
				{
					_cGameplay.PowerUpTxt_InApp.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					InAPpurchasepanel.SetActive (true);
				}
				break;
			}

		case "Dog":
			{
				if (PlayerPrefs.GetInt ("PowerUpCollect") >= 300)			//100
				{
					PlayerPrefs.SetInt ("PowerUpCollect", PlayerPrefs.GetInt ("PowerUpCollect") - 300);	//100
					_cGameplay.SelectCharStar.GetComponent<Text> ().text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					_cGameplay.PowerUpTxt.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					PlayerPrefs.SetString ("DogUnlocked", LockedChar);
					Destroy (GameObject.Find (LockedChar).transform.GetChild (0).gameObject);
					//GameObject.Find (LockedChar).transform.GetChild (0).gameObject.SetActive (false);
				}

				else 
				{
					_cGameplay.PowerUpTxt_InApp.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					InAPpurchasepanel.SetActive (true);
				}
				break;
			}

		// case "Frog":
		// 	{
		// 		if (PlayerPrefs.GetInt ("PowerUpCollect") >= 200)			//50
		// 		{
		// 			PlayerPrefs.SetInt ("PowerUpCollect", PlayerPrefs.GetInt ("PowerUpCollect") - 200);	//50
		// 			_cGameplay.SelectCharStar.GetComponent<Text> ().text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
		// 			_cGameplay.PowerUpTxt.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
		// 			PlayerPrefs.SetString ("FrogUnlocked", LockedChar);
		// 			Destroy (GameObject.Find (LockedChar).transform.GetChild (0).gameObject);
		// 			//GameObject.Find (LockedChar).transform.GetChild (0).gameObject.SetActive (false);
		// 		}

		// 		else 
		// 		{
		// 			_cGameplay.PowerUpTxt_InApp.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
		// 			InAPpurchasepanel.SetActive (true);
		// 		}

		// 		break;
		// 	}

		case "Bird":
			{
				if (PlayerPrefs.GetInt ("PowerUpCollect") >= 300)			//200
				{
					PlayerPrefs.SetInt ("PowerUpCollect", PlayerPrefs.GetInt ("PowerUpCollect") - 300);	//200
					_cGameplay.SelectCharStar.GetComponent<Text> ().text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					_cGameplay.PowerUpTxt.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					PlayerPrefs.SetString ("BirdUnlocked", LockedChar);
					Destroy (GameObject.Find (LockedChar).transform.GetChild (0).gameObject);
					//GameObject.Find (LockedChar).transform.GetChild (0).gameObject.SetActive (false);
				}

				else 
				{
					_cGameplay.PowerUpTxt_InApp.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					InAPpurchasepanel.SetActive (true);
				}

				break;
			}

		case "Lion":
			{
				if (PlayerPrefs.GetInt ("PowerUpCollect") >= 300)			//200
				{
					PlayerPrefs.SetInt ("PowerUpCollect", PlayerPrefs.GetInt ("PowerUpCollect") - 300);	//200
					_cGameplay.SelectCharStar.GetComponent<Text> ().text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					_cGameplay.PowerUpTxt.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					PlayerPrefs.SetString ("LionUnlocked", LockedChar);
					Destroy (GameObject.Find (LockedChar).transform.GetChild (0).gameObject);
					//GameObject.Find (LockedChar).transform.GetChild (0).gameObject.SetActive (false);
				}

				else 
				{
					_cGameplay.PowerUpTxt_InApp.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					InAPpurchasepanel.SetActive (true);
				}

				break;
			}

		case "Monkey":
			{
				if (PlayerPrefs.GetInt ("PowerUpCollect") >= 300)			//200
				{
					PlayerPrefs.SetInt ("PowerUpCollect", PlayerPrefs.GetInt ("PowerUpCollect") - 300);	//200
					_cGameplay.SelectCharStar.GetComponent<Text> ().text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					_cGameplay.PowerUpTxt.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					PlayerPrefs.SetString ("MonkeyUnlocked", LockedChar);
					Destroy (GameObject.Find (LockedChar).transform.GetChild (0).gameObject);
					//GameObject.Find (LockedChar).transform.GetChild (0).gameObject.SetActive (false);
				}

				else
				{
					_cGameplay.PowerUpTxt_InApp.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					InAPpurchasepanel.SetActive (true);
				}
				break;
			}

		case "Pig":
			{
				if (PlayerPrefs.GetInt ("PowerUpCollect") >= 300)
				{
					PlayerPrefs.SetInt ("PowerUpCollect", PlayerPrefs.GetInt ("PowerUpCollect") - 300);
					_cGameplay.SelectCharStar.GetComponent<Text> ().text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					_cGameplay.PowerUpTxt.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					PlayerPrefs.SetString ("PigUnlocked", LockedChar);
					Destroy (GameObject.Find (LockedChar).transform.GetChild (0).gameObject);
					//GameObject.Find (LockedChar).transform.GetChild (0).gameObject.SetActive (false);
				}

				else 
				{
					_cGameplay.PowerUpTxt_InApp.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					InAPpurchasepanel.SetActive (true);
				}
				break;
			}

		// case "Sheep":
		// 	{
		// 		if (PlayerPrefs.GetInt ("PowerUpCollect") >= 300)			//300
		// 		{
		// 			PlayerPrefs.SetInt ("PowerUpCollect", PlayerPrefs.GetInt ("PowerUpCollect") - 300);	//80
		// 			_cGameplay.SelectCharStar.GetComponent<Text> ().text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
		// 			_cGameplay.PowerUpTxt.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
		// 			//GameObject.Find (LockedChar).transform.GetChild (0).gameObject.SetActive (false);
		// 			PlayerPrefs.SetString ("SheepUnlocked", LockedChar);
		// 			Destroy (GameObject.Find (LockedChar).transform.GetChild (0).gameObject);
		// 		}

		// 		else 
		// 		{
		// 			_cGameplay.PowerUpTxt_InApp.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
		// 			InAPpurchasepanel.SetActive (true);
		// 		}
		// 		break;
		// 	}

		case "Hippo":
			{
				if (PlayerPrefs.GetInt ("PowerUpCollect") >= 300)			//300
				{
					PlayerPrefs.SetInt ("PowerUpCollect", PlayerPrefs.GetInt ("PowerUpCollect") - 300);	//80
					_cGameplay.SelectCharStar.GetComponent<Text> ().text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					_cGameplay.PowerUpTxt.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					//GameObject.Find (LockedChar).transform.GetChild (0).gameObject.SetActive (false);
					PlayerPrefs.SetString ("HippoUnlocked", LockedChar);
					Destroy (GameObject.Find (LockedChar).transform.GetChild (0).gameObject);
				}

				else 
				{
					_cGameplay.PowerUpTxt_InApp.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					InAPpurchasepanel.SetActive (true);
				}
				break;
			}

		case "Tiger":
			{
				if (PlayerPrefs.GetInt ("PowerUpCollect") >= 300)
				{
					PlayerPrefs.SetInt ("PowerUpCollect", PlayerPrefs.GetInt ("PowerUpCollect") - 300);
					_cGameplay.SelectCharStar.GetComponent<Text> ().text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					_cGameplay.PowerUpTxt.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					Destroy (GameObject.Find (LockedChar).transform.GetChild (0).gameObject);
					PlayerPrefs.SetString ("TigerUnlocked", LockedChar);
					//GameObject.Find (LockedChar).transform.GetChild (0).gameObject.SetActive (false);
				}

				else
				{
					_cGameplay.PowerUpTxt_InApp.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
					InAPpurchasepanel.SetActive (true);
				}
				break;
			}
		}
	}
}