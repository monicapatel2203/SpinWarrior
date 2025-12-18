using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatternControl : MonoBehaviour
{
	int t = 0;

	void Start ()
	{
		StartCoroutine (FirePattern());
	}

	// void Update()
	// {
		
	// }

	IEnumerator FirePattern()
	{
		for (t = 0; t <= transform.childCount-1; t++)
		{
			transform.GetChild (t).transform.GetComponent<PatternFire> ().enabled = true;

			if (t >= 16) 
			{
				Invoke ("DestroyParentObj",1);
			}

			yield return new WaitForSeconds (0.3f);
		}
	}

	void DestroyParentObj()
	{
		Destroy (transform.gameObject);
	}
}