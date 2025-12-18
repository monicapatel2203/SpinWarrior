using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
	public List<AudioClip> Music = new List<AudioClip> ();
	Gameplay _sGameplay;

	void Start ()
	{
		_sGameplay = FindObjectOfType<Gameplay> ();
	}
	
	void Update () 
	{
		
	}

	public void OnButtonTapped()
	{
		if (PlayerPrefs.GetInt ("SoundOn") == 0)
		{
			_sGameplay.CanvasRef.transform.GetComponent<AudioSource> ().clip = Music [4] as AudioClip;
			_sGameplay.CanvasRef.GetComponent<AudioSource> ().Play ();
		}
	}

	public void OnSwipePlay ()
	{
		if (PlayerPrefs.GetInt ("SoundOn") == 0)
		{
			_sGameplay.mainCamera.transform.GetComponent<AudioSource> ().clip = Music [5] as AudioClip;
			_sGameplay.mainCamera.transform.GetComponent<AudioSource> ().Play ();
		}
	}

	public void OnStarCollidePlayer()
	{
		if (PlayerPrefs.GetInt ("SoundOn") == 0)
		{
			_sGameplay.CanvasRef.transform.GetComponent<AudioSource> ().clip = Music [8] as AudioClip;
			_sGameplay.CanvasRef.transform.GetComponent<AudioSource> ().Play ();
		}
	}

	public void OnCollideWall()
	{
		if (PlayerPrefs.GetInt ("SoundOn") == 0)
		{
			_sGameplay.CanvasRef.transform.GetComponent<AudioSource> ().clip = Music [6] as AudioClip;
			_sGameplay.CanvasRef.transform.GetComponent<AudioSource> ().Play ();
		}
	}

	public void OnAttackPlayer()
	{
		if (PlayerPrefs.GetInt ("SoundOn") == 0)
		{
			_sGameplay.CanvasRef.transform.GetComponent<AudioSource> ().clip = Music [10] as AudioClip;
			_sGameplay.CanvasRef.transform.GetComponent<AudioSource> ().Play ();
		}
	}

	public void OnCollidePlayer()
	{
		if (PlayerPrefs.GetInt ("SoundOn") == 0)
		{
			_sGameplay.mainCamera.transform.GetComponent<AudioSource> ().clip = Music [3] as AudioClip;
			_sGameplay.mainCamera.transform.GetComponent<AudioSource> ().Play ();
		}
	}

	public void OnGamePlayStart()
	{
		if (PlayerPrefs.GetInt ("SoundOn") == 0)
		{
			if (PlayerPrefs.GetString ("isNormalGameRunning") == "true") 
			{	
				_sGameplay.SoundHandle.transform.GetComponent<AudioSource> ().clip = Music [1] as AudioClip;
				_sGameplay.SoundHandle.transform.GetComponent<AudioSource> ().Play ();
			} 
			else 
			{	
				_sGameplay.SoundHandle.transform.GetComponent<AudioSource> ().clip = Music [2] as AudioClip;
				_sGameplay.SoundHandle.transform.GetComponent<AudioSource> ().Play ();
			}
		}
	}

	public void OnGameOver()
	{
		if (PlayerPrefs.GetInt ("SoundOn") == 0)
		{
			_sGameplay.SoundHandle.transform.GetComponent<AudioSource> ().clip = Music [0] as AudioClip;
			_sGameplay.SoundHandle.transform.GetComponent<AudioSource> ().Play ();
		}
	}
}