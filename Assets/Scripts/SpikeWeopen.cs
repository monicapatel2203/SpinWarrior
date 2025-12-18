using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpikeWeopen : MonoBehaviour
{
	GameObject target;
	Gameplay _SgamePlay;
	SoundControl _SsoundControl;

	bool rightSpike , middleSpike , leftSpike;

	void Start ()
	{
		target = GameObject.Find(PlayerPrefs.GetString ("SelectedPlayerName"));    // GameObject.Find ("Player");
		_SgamePlay = FindObjectOfType<Gameplay> ();
		_SsoundControl = FindObjectOfType<SoundControl> ();

		StartCoroutine (WaitAndShoot());

		if(PlayerPrefs.GetString ("isDoubleGameRunnig") == "true") 
			PlayerPrefs.SetFloat ("SpikeWeaponSpeed",800f); 	
		else
			PlayerPrefs.SetFloat ("SpikeWeaponSpeed",800f); 	

	}
	
	void Update () 
	{
		if (rightSpike) 
		{
			transform.GetChild (0).gameObject.SetActive (true);
			transform.GetComponent<AudioSource> ().Play ();
			transform.position = Vector2.MoveTowards (transform.position, target.transform.position, PlayerPrefs.GetFloat ("SpikeWeaponSpeed") * Time.deltaTime);
		}
		if (middleSpike)
		{
			transform.GetChild (0).gameObject.SetActive (true);
			transform.GetComponent<AudioSource> ().Play ();
			transform.position = Vector2.MoveTowards (transform.position, target.transform.position, PlayerPrefs.GetFloat ("SpikeWeaponSpeed") * Time.deltaTime);
		}

		if (leftSpike) 
		{
			transform.GetChild (0).gameObject.SetActive (true);
			transform.GetComponent<AudioSource> ().Play ();
			transform.position = Vector2.MoveTowards (transform.position, target.transform.position, PlayerPrefs.GetFloat ("SpikeWeaponSpeed") * Time.deltaTime);
		}
	}

	IEnumerator WaitAndShoot()
	{
		if (transform.tag == "RightSpike")
		{
			yield return new WaitForSeconds (1);
			rightSpike = true;
		}

		if (transform.tag == "MiddleSpike")
		{
			yield return new WaitForSeconds(2);
			middleSpike = true;
		}

		if (transform.tag == "LeftSpike")
		{
			yield return new WaitForSeconds(3);
			leftSpike = true;
		}
	}

	void OnCollisionEnter2D(Collision2D objCol) 
	{
		if (objCol.collider.tag == "Player") 
		{
			_SsoundControl.OnAttackPlayer();
			Destroy(this.gameObject);
			// _SgamePlay.PlayerFillBar.transform.GetComponent<Image>().fillAmount += 1 / 50.0f;
			Instantiate (_SgamePlay.PlayerKilledParticle, transform.position, Quaternion.identity);

			// int playerCount = _SgamePlay.player.gameObject.transform.childCount;
			// Color player_tmp = _SgamePlay.player.GetComponent<SpriteRenderer>().color;
			// player_tmp.a = _SgamePlay.player.GetComponent<SpriteRenderer>().color.a - 0.25f;
			// _SgamePlay.player.GetComponent<SpriteRenderer>().color = player_tmp;
			// for(int i = 0; i <= playerCount - 1; i++)
			// {				
			// 	Color tmp = _SgamePlay.player.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color;
			// 	tmp.a = _SgamePlay.player.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color.a - 0.25f;
			// 	_SgamePlay.player.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color = tmp;
			// }

			var rendererComponents = _SgamePlay.player.GetComponentsInChildren<SpriteRenderer>(true);

			foreach (var component in rendererComponents)
			{
				Color tmp = component.color;
				tmp.a = component.color.a - 0.25f;
				component.color = tmp;
			}

			if( _SgamePlay.player.GetComponent<SpriteRenderer>().color.a <= 0.25f )
			{
				_SgamePlay.player.transform.GetComponent<CircleCollider2D> ().enabled = false;

				_SgamePlay.isGameOver = true;
				_SgamePlay.isLevelContinue = true;
			}

			// _SsoundControl.OnCollidePlayer ();
			// _SgamePlay.player.transform.GetComponent<CircleCollider2D> ().enabled = false;			
			// _SgamePlay.isGameOver = true;			
		}

		else if (objCol.collider.tag == "Wall") 
		{

			_SsoundControl.OnCollideWall ();

			Instantiate (_SgamePlay.EnimyKilledParticles, new Vector3(objCol.transform.position.x , objCol.transform.position.y , objCol.transform.position.z-13f), Quaternion.identity);

			_SgamePlay.ScoreCount (1);
			Destroy (transform.gameObject);
		}
	}
}