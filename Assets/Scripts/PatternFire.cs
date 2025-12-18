using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatternFire : MonoBehaviour {

	GameObject target;
	Gameplay _PgamePlay;
	SoundControl _PsoundControl;

	void Start () 
	{
		target = GameObject.Find(PlayerPrefs.GetString ("SelectedPlayerName"));
		_PgamePlay = FindObjectOfType<Gameplay> ();
		_PsoundControl = FindObjectOfType<SoundControl> ();
	}
	
	void Update () 
	{
		transform.position = Vector2.MoveTowards (transform.position, target.transform.position, 900f * Time.deltaTime);
	}

	void OnCollisionEnter2D(Collision2D objCol) 
	{
		if (objCol.collider.tag == "Player") 
		{
			_PsoundControl.OnAttackPlayer();
			Destroy(this.gameObject);
			Instantiate (_PgamePlay.PlayerKilledParticle, transform.position, Quaternion.identity);
			// _PgamePlay.PlayerFillBar.transform.GetComponent<Image>().fillAmount += 1 / 50.0f;

			// int playerCount = _PgamePlay.player.gameObject.transform.childCount;
			// Color player_tmp = _PgamePlay.player.GetComponent<SpriteRenderer>().color;
			// player_tmp.a = _PgamePlay.player.GetComponent<SpriteRenderer>().color.a - 0.25f;
			// _PgamePlay.player.GetComponent<SpriteRenderer>().color = player_tmp;
			// for(int i = 0; i <= playerCount - 1; i++)
			// {				
			// 	Color tmp = _PgamePlay.player.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color;
			// 	tmp.a = _PgamePlay.player.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color.a - 0.25f;
			// 	_PgamePlay.player.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color = tmp;
			// }

			var rendererComponents = _PgamePlay.player.GetComponentsInChildren<SpriteRenderer>(true);

			foreach (var component in rendererComponents)
			{
				Color tmp = component.color;
				tmp.a = component.color.a - 0.25f;
				component.color = tmp;
			}

			if( _PgamePlay.player.GetComponent<SpriteRenderer>().color.a <= 0.25f )
			{
				_PgamePlay.player.transform.GetComponent<CircleCollider2D> ().enabled = false;

				_PgamePlay.isGameOver = true;
				_PgamePlay.isLevelContinue = true;
			}

			// _PsoundControl.OnCollidePlayer ();
			// _PgamePlay.player.transform.GetComponent<CircleCollider2D> ().enabled = false;		
			// transform.gameObject.SetActive (false);
			// _PgamePlay.isGameOver = true;
		}
		else if (objCol.collider.tag == "Wall") 
		{
			_PsoundControl.OnCollideWall ();

			Instantiate (_PgamePlay.EnimyKilledParticles, new Vector3(objCol.transform.position.x , objCol.transform.position.y , objCol.transform.position.z-13f), Quaternion.identity);

			_PgamePlay.ScoreCount (1);
			transform.gameObject.SetActive (false);
		}
	}
}