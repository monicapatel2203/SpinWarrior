using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeWeaponMovement : MonoBehaviour
{
	GameObject target;
	Vector2 speedball , startVelocity , oneVelocity;
	Gameplay _TgamePlay;
	TimeGameplay _tTimeGamePlay;
	SoundControl _TWsoundControl;
	int WallTouchCount;
	int c;

	void Start ()
	{
		target = GameObject.Find(PlayerPrefs.GetString ("SelectedPlayerName"));
		InvokeRepeating ("oNEnimyAttack",1f , 2f);
		_TgamePlay = FindObjectOfType<Gameplay> ();
		_tTimeGamePlay = FindObjectOfType<TimeGameplay> ();
		_TWsoundControl = FindObjectOfType<SoundControl> ();

		if (transform.tag == "TimeWeapon1")
		{
			transform.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (12000, -7000));
		}

		else if (transform.tag == "TimeWeapon2")
		{
			transform.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-15000, -7500));
		}

		else if (transform.tag == "TimeWeapon3")
		{
			transform.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-11000, 8000));
		}

		else if (transform.tag == "TimeWeapon4")
		{
			transform.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-12500, -8500));
		}

		//----------------------------------------------------------------------------------
		else if (transform.tag == "TimeWeapon5")
		{
			transform.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-15500, 9000));
		}

		else if (transform.tag == "TimeWeapon6")
		{
			transform.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-14500, -10500));
		}

		else if (transform.tag == "TimeWeapon7")
		{
			transform.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-13500, 9000));
		}

		else if (transform.tag == "TimeWeapon8")
		{
			transform.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-14000, -9800));
		}

		else if (transform.tag == "TimeWeapon9")
		{
			transform.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-14500, 10000));
		}

		else if (transform.tag == "TimeWeapon10")
		{
			transform.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-15000, -9500));
		}

		//------------------------------------------------------------------------------------
		else if (transform.tag == "TimeWeapon11")
		{
			transform.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-16500, 9500));
		}

		else if (transform.tag == "TimeWeapon12")
		{
			transform.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-17000, -10000));
		}

		else if (transform.tag == "TimeWeapon13")
		{
			transform.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-16500, 10000));
		}

		else if (transform.tag == "TimeWeapon14")
		{
			transform.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-17500, -11500));
		}

		else if (transform.tag == "TimeWeapon15")
		{
			transform.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-18000, 11000));
		}

		else
		{
			transform.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-17000, -10000));
		}

		startVelocity = new Vector2 (11000, -7000);

	}
	
	void Update ()
	{
		if(_TgamePlay.PausePanel.activeInHierarchy)
		{

		}
		else
		{
			transform.Rotate (0,0,15f);

			if (transform.GetComponent<Rigidbody2D> ().velocity.y == 0 || transform.GetComponent<Rigidbody2D> ().velocity.x == 0)
			{
				transform.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (11000, -7000));
			}
		}
	}

	void OnCollisionEnter2D(Collision2D objCol)
	{
		if (objCol.collider.tag == "Wall") 
		{
			speedball = transform.GetComponent<Rigidbody2D> ().velocity;
		}
	}

	void OnCollisionExit2D(Collision2D objCol)
	{
		if (objCol.collider.tag == "Player") 
		{
			_TWsoundControl.OnAttackPlayer();
			Destroy(this.gameObject);
			Instantiate (_TgamePlay.PlayerKilledParticle, transform.position, Quaternion.identity);
			// _TgamePlay.PlayerFillBar.transform.GetComponent<Image>().fillAmount += 1 / 50.0f;

			// int playerCount = _TgamePlay.player.gameObject.transform.childCount;
			// Color player_tmp = _TgamePlay.player.GetComponent<SpriteRenderer>().color;
			// player_tmp.a = _TgamePlay.player.GetComponent<SpriteRenderer>().color.a - 0.25f;
			// _TgamePlay.player.GetComponent<SpriteRenderer>().color = player_tmp;
			// for(int i = 0; i <= playerCount - 1; i++)
			// {				
			// 	Color tmp = _TgamePlay.player.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color;
			// 	tmp.a = _TgamePlay.player.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color.a - 0.25f;
			// 	_TgamePlay.player.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color = tmp;
			// }

			var rendererComponents = _TgamePlay.player.GetComponentsInChildren<SpriteRenderer>(true);

			foreach (var component in rendererComponents)
			{
				Color tmp = component.color;
				tmp.a = component.color.a - 0.25f;
				component.color = tmp;
			}

			if( _TgamePlay.player.GetComponent<SpriteRenderer>().color.a <= 0.25f )
			{
				_TgamePlay.player.transform.GetComponent<CircleCollider2D> ().enabled = false;

				_TgamePlay.isGameOver = true;
				_TgamePlay.isLevelContinue = true;
			}

			// _TWsoundControl.OnCollidePlayer ();
			// _TgamePlay.player.transform.GetComponent<CircleCollider2D> ().enabled = false;			
			// _TgamePlay.isGameOver = true;
			// _TgamePlay.isLevelContinue = true;
		}

		else if (objCol.collider.tag == "Wall") 
		{

			_TWsoundControl.OnCollideWall ();


			transform.GetComponent<Rigidbody2D> ().velocity =  speedball;
			// Debug.LogError("OnCollision exit..wall.." + transform.GetComponent<Rigidbody2D> ().velocity);
			Instantiate (_TgamePlay.EnimyKilledParticles, new Vector3(objCol.transform.position.x , objCol.transform.position.y , objCol.transform.position.z-13f), Quaternion.identity);

			_TgamePlay.ScoreCount (1);

			if (!_TgamePlay.isLevelContinue) 
			{
				_TgamePlay.player.transform.GetChild (0).gameObject.SetActive (true);
				_TgamePlay.player.transform.GetChild (0).GetChild (0).gameObject.SetActive (true);
			}

			_TgamePlay.player.transform.GetChild (1).gameObject.SetActive (false);
			_TgamePlay.player.transform.GetChild (2).gameObject.SetActive (false);

			if(PlayerPrefs.GetFloat ("TimeCurrentLevel") >= 1 && PlayerPrefs.GetFloat ("TimeCurrentLevel") < 11)
			{
				if (objCol.transform.name == "1") 
				{
					if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);

						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "2") 
				{
					if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "3") 
				{
					if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "4") 
				{
					if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "5") 
				{
					if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "6") 
				{
					if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "7") 
				{
					if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "8") 
				{
					if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "9") 
				{
					if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "10") 
				{
					if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "11") 
				{
					if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}
			}












			else if(PlayerPrefs.GetFloat ("TimeCurrentLevel") > 10 && PlayerPrefs.GetFloat ("TimeCurrentLevel") < 21)
			{
				if (objCol.transform.name == "1") 
				{

					if (objCol.transform.GetChild (2).gameObject.activeSelf)
					{
						objCol.transform.GetChild (3).gameObject.SetActive (true);
						objCol.transform.GetChild (2).gameObject.SetActive (false);

						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "2") 
				{

					if (objCol.transform.GetChild (2).gameObject.activeSelf)
					{
						objCol.transform.GetChild (3).gameObject.SetActive (true);
						objCol.transform.GetChild (2).gameObject.SetActive (false);

						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "3") 
				{

					if (objCol.transform.GetChild (2).gameObject.activeSelf)
					{
						objCol.transform.GetChild (3).gameObject.SetActive (true);
						objCol.transform.GetChild (2).gameObject.SetActive (false);

						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "4") 
				{

					if (objCol.transform.GetChild (2).gameObject.activeSelf)
					{
						objCol.transform.GetChild (3).gameObject.SetActive (true);
						objCol.transform.GetChild (2).gameObject.SetActive (false);

						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "5") 
				{

					if (objCol.transform.GetChild (2).gameObject.activeSelf)
					{
						objCol.transform.GetChild (3).gameObject.SetActive (true);
						objCol.transform.GetChild (2).gameObject.SetActive (false);

						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "6") 
				{

					if (objCol.transform.GetChild (2).gameObject.activeSelf)
					{
						objCol.transform.GetChild (3).gameObject.SetActive (true);
						objCol.transform.GetChild (2).gameObject.SetActive (false);

						objCol.transform.gameObject.SetActive (false);
					}


					else if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "7") 
				{
					if (objCol.transform.GetChild (2).gameObject.activeSelf)
					{
						objCol.transform.GetChild (3).gameObject.SetActive (true);
						objCol.transform.GetChild (2).gameObject.SetActive (false);
						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "8") 
				{

					if (objCol.transform.GetChild (2).gameObject.activeSelf)
					{
						objCol.transform.GetChild (3).gameObject.SetActive (true);
						objCol.transform.GetChild (2).gameObject.SetActive (false);
						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "9") 
				{
					if (objCol.transform.GetChild (2).gameObject.activeSelf)
					{
						objCol.transform.GetChild (3).gameObject.SetActive (true);
						objCol.transform.GetChild (2).gameObject.SetActive (false);
						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "10") 
				{

					if (objCol.transform.GetChild (2).gameObject.activeSelf)
					{
						objCol.transform.GetChild (3).gameObject.SetActive (true);
						objCol.transform.GetChild (2).gameObject.SetActive (false);
						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "11") 
				{

					if (objCol.transform.GetChild (2).gameObject.activeSelf)
					{
						objCol.transform.GetChild (3).gameObject.SetActive (true);
						objCol.transform.GetChild (2).gameObject.SetActive (false);
						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}
			}









			else if(PlayerPrefs.GetFloat ("TimeCurrentLevel") > 20)
			{
				if (objCol.transform.name == "1") 
				{

					if (objCol.transform.GetChild (3).gameObject.activeSelf)
					{
						objCol.transform.GetChild (4).gameObject.SetActive (true);
						objCol.transform.GetChild (3).gameObject.SetActive (false);

						objCol.transform.gameObject.SetActive (false);
					}


					else if (objCol.transform.GetChild (2).gameObject.activeSelf)
					{
						objCol.transform.GetChild (3).gameObject.SetActive (true);
						objCol.transform.GetChild (2).gameObject.SetActive (false);

					}

					else if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "2") 
				{

					if (objCol.transform.GetChild (3).gameObject.activeSelf)
					{
						objCol.transform.GetChild (4).gameObject.SetActive (true);
						objCol.transform.GetChild (3).gameObject.SetActive (false);

						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (2).gameObject.activeSelf)
					{
						objCol.transform.GetChild (3).gameObject.SetActive (true);
						objCol.transform.GetChild (2).gameObject.SetActive (false);

					}

					else if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "3") 
				{

					if (objCol.transform.GetChild (3).gameObject.activeSelf)
					{
						objCol.transform.GetChild (4).gameObject.SetActive (true);
						objCol.transform.GetChild (3).gameObject.SetActive (false);

						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (2).gameObject.activeSelf)
					{
						objCol.transform.GetChild (3).gameObject.SetActive (true);
						objCol.transform.GetChild (2).gameObject.SetActive (false);

					}

					else if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "4") 
				{
					if (objCol.transform.GetChild (3).gameObject.activeSelf)
					{
						objCol.transform.GetChild (4).gameObject.SetActive (true);
						objCol.transform.GetChild (3).gameObject.SetActive (false);
						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (2).gameObject.activeSelf)
					{
						objCol.transform.GetChild (3).gameObject.SetActive (true);
						objCol.transform.GetChild (2).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "5") 
				{

					if (objCol.transform.GetChild (3).gameObject.activeSelf)
					{
						objCol.transform.GetChild (4).gameObject.SetActive (true);
						objCol.transform.GetChild (3).gameObject.SetActive (false);
						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (2).gameObject.activeSelf)
					{
						objCol.transform.GetChild (3).gameObject.SetActive (true);
						objCol.transform.GetChild (2).gameObject.SetActive (false);

					}

					else if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "6") 
				{

					if (objCol.transform.GetChild (3).gameObject.activeSelf)
					{
						objCol.transform.GetChild (4).gameObject.SetActive (true);
						objCol.transform.GetChild (3).gameObject.SetActive (false);
						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (2).gameObject.activeSelf)
					{
						objCol.transform.GetChild (3).gameObject.SetActive (true);
						objCol.transform.GetChild (2).gameObject.SetActive (false);
					}


					else if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "7") 
				{

					if (objCol.transform.GetChild (3).gameObject.activeSelf)
					{
						objCol.transform.GetChild (4).gameObject.SetActive (true);
						objCol.transform.GetChild (3).gameObject.SetActive (false);
						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (2).gameObject.activeSelf)
					{
						objCol.transform.GetChild (3).gameObject.SetActive (true);
						objCol.transform.GetChild (2).gameObject.SetActive (false);
						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "8") 
				{
					if (objCol.transform.GetChild (3).gameObject.activeSelf)
					{
						objCol.transform.GetChild (4).gameObject.SetActive (true);
						objCol.transform.GetChild (3).gameObject.SetActive (false);
						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (2).gameObject.activeSelf)
					{
						objCol.transform.GetChild (3).gameObject.SetActive (true);
						objCol.transform.GetChild (2).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "9") 
				{

					if (objCol.transform.GetChild (3).gameObject.activeSelf)
					{
						objCol.transform.GetChild (4).gameObject.SetActive (true);
						objCol.transform.GetChild (3).gameObject.SetActive (false);
						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (2).gameObject.activeSelf)
					{
						objCol.transform.GetChild (3).gameObject.SetActive (true);
						objCol.transform.GetChild (2).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "10") 
				{
					if (objCol.transform.GetChild (3).gameObject.activeSelf)
					{
						objCol.transform.GetChild (4).gameObject.SetActive (true);
						objCol.transform.GetChild (3).gameObject.SetActive (false);
						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (2).gameObject.activeSelf)
					{
						objCol.transform.GetChild (3).gameObject.SetActive (true);
						objCol.transform.GetChild (2).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}

				else if (objCol.transform.name == "11") 
				{
					if (objCol.transform.GetChild (3).gameObject.activeSelf)
					{
						objCol.transform.GetChild (4).gameObject.SetActive (true);
						objCol.transform.GetChild (3).gameObject.SetActive (false);
						objCol.transform.gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (2).gameObject.activeSelf)
					{
						objCol.transform.GetChild (3).gameObject.SetActive (true);
						objCol.transform.GetChild (2).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (1).gameObject.activeSelf)
					{
						objCol.transform.GetChild (2).gameObject.SetActive (true);
						objCol.transform.GetChild (1).gameObject.SetActive (false);
					}

					else if (objCol.transform.GetChild (0).gameObject.activeSelf)
					{
						objCol.transform.GetChild (1).gameObject.SetActive (true);
						objCol.transform.GetChild (0).gameObject.SetActive (false);
					}

					else
					{
						objCol.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
						objCol.transform.GetChild (0).gameObject.SetActive (true);
					}
				}
			}
		}
	}


	void oNEnimyAttack()
	{
		if (c == 0) 
		{
			target.transform.GetChild (0).transform.GetComponent<Animation> ().Play ("RightAnim");
			c = 1;
		}

		else if (c == 1) 
		{
			target.transform.GetChild (0).transform.GetComponent<Animation> ().Play ("LeftAnim");
			c = 0;
		}
		else
		{
		}
	}
}