using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Learning : MonoBehaviour 
{
	public static string materialColour;
	public List<Color> colorName = new List<Color> ();
	public List<Color> colorValue = new List<Color> ();
	Color Green , Red , Yellow , Blue;
	float Speed;
	static int f , l=1;
	public Color top, mid, bottom;

	Color startColor , EndColor;

	void Start ()
	{
	}
	
	void Update ()
	{
		if (Input.GetMouseButtonDown (0))
		{
			Debug.Log ("Tapped");
			InvokeRepeating ("TestingIR" , 3f , 8f);
		}
	}

	void TestingIR()
	{
		Debug.Log ("TestigIR Called");
	}

}