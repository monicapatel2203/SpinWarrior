using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour 
{
	float Speed;
	GameObject target;
	Gameplay _PgamePlay;
	Vector2 speedball;
	SoundControl _pSoundControl;

	void Awake()
	{
		target = GameObject.FindGameObjectWithTag ("Player");

		_PgamePlay = FindObjectOfType<Gameplay> ();
		_pSoundControl = FindObjectOfType<SoundControl> ();

		if (PlayerPrefs.GetString ("isTimeBaseGameRunnig") == "true") 
		{
			transform.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (12000, -7000));
		}
	}

	void Start () 
	{
		if ((PlayerPrefs.GetString ("isNormalGameRunning") == "true")) 
		{
			if (transform.tag == "PowerUp") 
			{
				if (PlayerPrefs.GetFloat ("CurrentLevel") >= 1 && PlayerPrefs.GetFloat ("CurrentLevel") <= 5)
				{

					if (PlayerPrefs.GetFloat ("OnBonusLevel") == 1) 
						Speed = 300f;

					else
					Speed = 300f;
				}

				else if (PlayerPrefs.GetFloat ("CurrentLevel") >= 6 && PlayerPrefs.GetFloat ("CurrentLevel") <= 10)
				{
					if (PlayerPrefs.GetFloat ("OnBonusLevel") == 1) 
						Speed = 400f;

					else
					Speed = 400f;
				}

				else if (PlayerPrefs.GetFloat ("CurrentLevel") >= 11 && PlayerPrefs.GetFloat ("CurrentLevel") <= 15)
				{

					if (PlayerPrefs.GetFloat ("OnBonusLevel") == 1) 
						Speed = 500f;

					else
					Speed = 400f;
				}

				else if (PlayerPrefs.GetFloat ("CurrentLevel") >= 16 && PlayerPrefs.GetFloat ("CurrentLevel") <= 20)
				{

					if (PlayerPrefs.GetFloat ("OnBonusLevel") == 1) 
						Speed = 500f;

					else
					Speed = 400f;
				}

				else
				{

					if (PlayerPrefs.GetFloat ("OnBonusLevel") == 1) 
						Speed = 540;

					else
					Speed = 400f;
				}
			}
		}

		else if ((PlayerPrefs.GetString ("isTimeBaseGameRunnig") == "true")) 
		{
			if (transform.tag == "PowerUp") 
			{
				if (PlayerPrefs.GetFloat ("TimeCurrentLevel") >= 1 && PlayerPrefs.GetFloat ("TimeCurrentLevel") < 6)
				{
					Speed = 300f;
				}

				else if (PlayerPrefs.GetFloat ("TimeCurrentLevel") >= 6 && PlayerPrefs.GetFloat ("TimeCurrentLevel") <11)
				{
					Speed = 400f;
				}

				else if (PlayerPrefs.GetFloat ("TimeCurrentLevel") >= 11 && PlayerPrefs.GetFloat ("TimeCurrentLevel") <16)
				{
					Speed = 450f;
				}

				else if (PlayerPrefs.GetFloat ("TimeCurrentLevel") >= 16 && PlayerPrefs.GetFloat ("TimeCurrentLevel") <21)
				{
					Speed = 500f;
				}

				else
				{
					Speed = 550f;
				}
			}
		}
	}
	
	void Update ()
	{
		if(_PgamePlay.PausePanel.activeInHierarchy)
		{

		}
		else
		{
			if (PlayerPrefs.GetString ("isTimeBaseGameRunnig") == "true") 
			{
				transform.Rotate (0, 0, 350f);
			}

			else
			{
				if (!_PgamePlay.isLevelContinue) 
				{
					transform.Rotate (0, 0, 350f);
					transform.position = Vector2.MoveTowards (transform.position, target.transform.position, Speed * Time.deltaTime);
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D objCol) 
	{
		if (objCol.collider.tag == "Player") 
		{
			if (PlayerPrefs.GetInt ("GotBonusLevel") == 1)
			{
				PlayerPrefs.SetInt("BonusCollected",1);
			} 
			else
			{
			}

			_pSoundControl.OnStarCollidePlayer ();

//			_PgamePlay.CanvasRef.transform.GetComponent<AudioSource>().clip = _pSoundControl.Music [8] as AudioClip;
//			_PgamePlay.CanvasRef.transform.GetComponent<AudioSource>().Play ();

			Instantiate (_PgamePlay.collectPowerUp, transform.position, Quaternion.identity);
			// _PgamePlay.PlayerFillBar.transform.GetComponent<Image>().fillAmount -= 1 / 50.0f;

			// int playerCount = _PgamePlay.player.gameObject.transform.childCount;
			// Color player_tmp = _PgamePlay.player.GetComponent<SpriteRenderer>().color;
			// player_tmp.a = _PgamePlay.player.GetComponent<SpriteRenderer>().color.a + 0.25f;
			// _PgamePlay.player.GetComponent<SpriteRenderer>().color = player_tmp;
			// for(int i = 0; i <= playerCount - 1; i++)
			// {				
			// 	Color tmp = _PgamePlay.player.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color;
			// 	tmp.a = _PgamePlay.player.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color.a + 0.25f;
			// 	_PgamePlay.player.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color = tmp;
			// }

			var rendererComponents = _PgamePlay.player.GetComponentsInChildren<SpriteRenderer>(true);

			foreach (var component in rendererComponents)
			{
				Color tmp = component.color;
				if( tmp.a < 1.0f )
				{
					tmp.a = component.color.a + 0.25f;
					component.color = tmp;
				}				
				
			}

			// _PgamePlay.CollectibleCount (1);
			_PgamePlay.PowerupCount_level(1);
			Destroy (transform.gameObject);
		}

		else if (objCol.collider.tag == "Wall") 
		{

			_pSoundControl.OnCollideWall ();

//			_PgamePlay.CanvasRef.transform.GetComponent<AudioSource>().clip = _pSoundControl.Music [6] as AudioClip;
//			_PgamePlay.CanvasRef.transform.GetComponent<AudioSource> ().Play ();

			if (PlayerPrefs.GetString ("isTimeBaseGameRunnig") == "true") 
			{
					Instantiate (_PgamePlay.destroyPowerUp, transform.position, Quaternion.identity);
					Destroy (transform.gameObject);
			} 
			else 
			{
				//_PgamePlay.SoundParent.transform.GetChild(0).GetComponent<AudioSource> ().Play ();
				Instantiate (_PgamePlay.destroyPowerUp, transform.position, Quaternion.identity);
				Destroy (transform.gameObject);
			}


		}
	}

	void OnCollisionExit2D(Collision2D objCol)
	{
		if (PlayerPrefs.GetString ("isTimeBaseGameRunnig") == "true") 
		{
			if (objCol.collider.tag == "Wall")
			{
				transform.GetComponent<Rigidbody2D> ().velocity = speedball;
			}
		}
	}
}