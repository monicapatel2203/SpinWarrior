using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGControl : MonoBehaviour
{
	float offset;
	Gameplay _bgGameplay;

	void Start () 
	{
		_bgGameplay = FindObjectOfType<Gameplay> ();
	}
	
	void Update () 
	{
		if ((PlayerPrefs.GetString ("isNormalGameRunning") == "true")) 
		{
			if (_bgGameplay.isGameStart) 
			{
				offset += Time.deltaTime;
				transform.GetComponent<Image> ().material.SetTextureOffset ("_MainTex", new Vector2 (0, offset * 0.08f));
			}
		}
	}
}