using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnimyKilledParticles : MonoBehaviour
{

	void Start ()
	{
		Invoke ("DestroyParticles",1.5f);
	}

	void DestroyParticles()
	{
		Destroy (transform.gameObject);
	}
}
