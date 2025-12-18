using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enimy : MonoBehaviour 
{
	GameObject target;
	float Speed;
	bool isCollideDone;
	Gameplay _EgamePlay;
	SoundControl _EsoundControl;

	void Awake()
	{
		target = GameObject.Find(PlayerPrefs.GetString ("SelectedPlayerName"));
		_EgamePlay = FindObjectOfType<Gameplay> ();
		_EsoundControl = FindObjectOfType<SoundControl> ();
	}

	void Start () 
	{
		if((PlayerPrefs.GetString ("isNormalGameRunning") == "true") || (PlayerPrefs.GetString ("isTimeBaseGameRunnig") == "true"))
		{
			if (transform.tag == "Kunai") 
			{
				PlayerPrefs.SetFloat ("KunaiWeaponSpeed",500f);
			} 
			else 
			{
			}

		switch (int.Parse(PlayerPrefs.GetFloat ("CurrentLevel").ToString()))
		{
			case 1:
				PlayerPrefs.SetFloat ("LevelSpeed", 350f);
				break;

			case 2:
				PlayerPrefs.SetFloat ("LevelSpeed", 350f);
				break;

			case 3:
				PlayerPrefs.SetFloat ("LevelSpeed", 355f);
				break;

			case 4:
				PlayerPrefs.SetFloat ("LevelSpeed", 355f);
				break;

			case 5:
				PlayerPrefs.SetFloat ("LevelSpeed", 360f);
				break;

			case 6:
				PlayerPrefs.SetFloat ("LevelSpeed", 365f);
				break;

			case 7:
				PlayerPrefs.SetFloat ("LevelSpeed", 370f);
				break;

			case 8:
				PlayerPrefs.SetFloat ("LevelSpeed", 385f);
				break;

			case 9:
				PlayerPrefs.SetFloat ("LevelSpeed", 400f);
				break;

			case 10:
				PlayerPrefs.SetFloat ("LevelSpeed", 415f);
				break;

			case 11:
				PlayerPrefs.SetFloat ("LevelSpeed", 430f);
				break;

			case 12:
				PlayerPrefs.SetFloat ("LevelSpeed", 445f);
				break;

			case 13:
				PlayerPrefs.SetFloat ("LevelSpeed", 450f);
				break;

			case 14:
				PlayerPrefs.SetFloat ("LevelSpeed", 450f);
				break;

			case 15:
				PlayerPrefs.SetFloat ("LevelSpeed", 450f);
				break;

			case 16:
				PlayerPrefs.SetFloat ("LevelSpeed", 450f);
				break;

			case 17:
				PlayerPrefs.SetFloat ("LevelSpeed", 450f);
				break;

			case 18:
				PlayerPrefs.SetFloat ("LevelSpeed", 450f);
				break;

			case 19:
				PlayerPrefs.SetFloat ("LevelSpeed", 450f);
				break;

			case 20:
				PlayerPrefs.SetFloat ("LevelSpeed", 450f);
				break;

			case 21:
				PlayerPrefs.SetFloat ("LevelSpeed", 450f);
				break;

			case 22:
				PlayerPrefs.SetFloat ("LevelSpeed", 450f);
				break;

			case 23:
				PlayerPrefs.SetFloat ("LevelSpeed", 450f);
				break;

			case 24:
				PlayerPrefs.SetFloat ("LevelSpeed", 450f);
				break;

			case 25:
				PlayerPrefs.SetFloat ("LevelSpeed", 450f);
				break;

			case 26:
				PlayerPrefs.SetFloat ("LevelSpeed", 450f);
				break;

			case 27:
				PlayerPrefs.SetFloat ("LevelSpeed", 450f);
				break;

			case 28:
				PlayerPrefs.SetFloat ("LevelSpeed", 450f);
				break;

			case 29:
				PlayerPrefs.SetFloat ("LevelSpeed", 450f);
				break;

			case 30:
				PlayerPrefs.SetFloat ("LevelSpeed", 450f);
				break;

			}
		}

		else
		{
			PlayerPrefs.SetFloat ("LevelSpeed", 800f);
			PlayerPrefs.SetFloat ("KunaiWeaponSpeed",800f);
		}
	}
	
	void Update ()
	{		
		// if(_EgamePlay.PausePanel.activeInHierarchy || _EgamePlay.GameContinuePopup.activeInHierarchy)
		if(_EgamePlay.GameContinuePopup.activeInHierarchy)
		{
			Debug.LogError("Panel is active...");
			CancelInvoke ("GenerateEnemy");
			_EgamePlay.StopGeneratedEnimy();
			CancelInvoke("StopKunaiWeaponCall");
			_EgamePlay.StopKunaiWeaponCall();
			_EgamePlay.StopEnemyAttack();

			_EgamePlay.StopCollectible();
			_EgamePlay.StopuPKunaiWeaponCall ();
			_EgamePlay.StopSpikeWeaponCall ();
			_EgamePlay.OnPatternCancle ();
			this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
		}
		else
		{
			this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
			// Debug.LogError("Enemy update called...");
			if (Vector3.Distance (target.transform.position, transform.position) < 250f) 
			{
				_EgamePlay.player.transform.GetChild (0).gameObject.SetActive (false);
				_EgamePlay.player.transform.GetChild (0).GetChild (0).gameObject.SetActive (false);
				_EgamePlay.player.transform.GetChild (1).gameObject.SetActive (true);
				_EgamePlay.player.transform.GetChild (2).gameObject.SetActive (true);
			}

			if(transform.tag == "Shuricon")
			{
				// Debug.LogError("enemy if called....");
				transform.Rotate (0,0,25f);
				transform.position = Vector2.MoveTowards (transform.position, target.transform.position, PlayerPrefs.GetFloat ("LevelSpeed") * Time.deltaTime);
			}

			else
			{
				// Debug.LogError("enemy else called....");
				transform.position = Vector2.MoveTowards (transform.position, target.transform.position, PlayerPrefs.GetFloat ("KunaiWeaponSpeed") * Time.deltaTime);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D objCol) 
	{
		if (objCol.collider.tag == "Player") 
		{
			_EsoundControl.OnAttackPlayer();
			Destroy(this.gameObject);
			// _EgamePlay.PlayerFillBar.transform.GetComponent<Image>().fillAmount += 1 / 50.0f;

			// int playerCount = _EgamePlay.player.gameObject.transform.childCount;
			// Color player_tmp = _EgamePlay.player.GetComponent<SpriteRenderer>().color;
			// player_tmp.a = _EgamePlay.player.GetComponent<SpriteRenderer>().color.a - 0.25f;
			// _EgamePlay.player.GetComponent<SpriteRenderer>().color = player_tmp;
			// for(int i = 0; i <= playerCount - 1; i++)
			// {				
			// 	Color tmp = _EgamePlay.player.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color;
			// 	tmp.a = _EgamePlay.player.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color.a - 0.25f;
			// 	_EgamePlay.player.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color = tmp;
			// 	// Debug.LogError("Character aplha..." + _EgamePlay.player.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color.a + " Name..." + _EgamePlay.player.gameObject.transform.GetChild(i).name);
			// }

			var rendererComponents = _EgamePlay.player.GetComponentsInChildren<SpriteRenderer>(true);

			foreach (var component in rendererComponents)
			{
				Color tmp = component.color;
				tmp.a = component.color.a - 0.25f;
				component.color = tmp;
			}

			Instantiate (_EgamePlay.PlayerKilledParticle, transform.position, Quaternion.identity);

			if( _EgamePlay.player.GetComponent<SpriteRenderer>().color.a <= 0.25f )
			{
				_EgamePlay.player.transform.GetComponent<CircleCollider2D> ().enabled = false;

				_EgamePlay.isGameOver = true;
				_EgamePlay.isLevelContinue = true;
			}

			// _EsoundControl.OnCollidePlayer ();
			// _EgamePlay.player.transform.GetComponent<CircleCollider2D> ().enabled = false;
			// _EgamePlay.isGameOver = true;
			// _EgamePlay.isLevelContinue = true;
		}

		else if (objCol.collider.tag == "Wall") 
		{
			_EsoundControl.OnCollideWall ();

			Instantiate (_EgamePlay.EnimyKilledParticles, new Vector3(objCol.transform.position.x , objCol.transform.position.y , objCol.transform.position.z-13f), Quaternion.identity);

			_EgamePlay.ScoreCount (1);
			Destroy (transform.gameObject);

			if (!_EgamePlay.isLevelContinue) 
			{
				_EgamePlay.player.transform.GetChild (0).gameObject.SetActive (true);
				_EgamePlay.player.transform.GetChild (0).GetChild (0).gameObject.SetActive (true);
			}

			_EgamePlay.player.transform.GetChild (1).gameObject.SetActive (false);
			_EgamePlay.player.transform.GetChild (2).gameObject.SetActive (false);
		}
	}
}