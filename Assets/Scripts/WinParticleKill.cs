using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinParticleKill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke ("DestroyParticles",6.0f);
    }

    // Update is called once per frame
    void DestroyParticles()
	{
		Destroy (transform.gameObject);
	}
}
