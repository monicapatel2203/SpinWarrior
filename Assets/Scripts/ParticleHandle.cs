using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandle : MonoBehaviour
{

	void Start ()
	{
		Debug.LogError (""+transform.name);
	}
	
	void Update ()
	{
		transform.Rotate (50f,50f,50f);
	}
}
