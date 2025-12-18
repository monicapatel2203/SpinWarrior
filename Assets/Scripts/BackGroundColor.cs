using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundColor : MonoBehaviour 
{
	public Vector3 StartingPos;
	Gameplay _bGameplay;

	void Start ()
	{
		StartingPos = transform.position;
		_bGameplay = FindObjectOfType<Gameplay> ();
	}

	void Update () 
	{
		if (_bGameplay.isGameStart) 
		{
			transform.position = transform.position + (Vector3.up * 1.7f);
		}
	}
}