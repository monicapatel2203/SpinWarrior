using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SwipeControl : MonoBehaviour
{
	public GameObject PlayerShield;
	Vector3 fp,lp,currentSwipe,SetPos;
	Gameplay gm;
	float minSwipeLength, movementSpeed;
	bool dragging;

	void Start()
	{
		Debug.Log("Monica called");
		gm = FindObjectOfType<Gameplay> ();
		minSwipeLength = 0.25f;			//0.5f
	}

	void Update ()
	{
		if(gm.isGameOver==false || gm.isLevelContinue==false)
		{
			// foreach (Touch touch in Input.touches)
			// {
			// 	if (touch.phase == TouchPhase.Began) 
			// 	{
			// 		fp = touch.position;
			// 		lp = touch.position;
			// 	}

			// 	if (touch.phase == TouchPhase.Moved)
			// 	{
			// 		lp = touch.position;
				
			// 		if (lp.x > fp.x)//Right move
			// 		{ 
			// 			// PlayerShield.transform.Rotate (0,0,PlayerPrefs.GetFloat ("WallRotationSpeed"));
			// 			PlayerShield.transform.Rotate (0,0,6.0f);
			// 		} 
			// 		else //Left move
			// 		{ 
			// 			// PlayerShield.transform.Rotate (0,0,-PlayerPrefs.GetFloat ("WallRotationSpeed"));
			// 			PlayerShield.transform.Rotate (0,0,-6.0f);
			// 		}
			// 		fp = touch.position;
			// 	}
			// }

			/* On Mouse event swipe control */
			// if (Input.GetMouseButtonDown(0))
			// {
			// 	fp = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			// }
			// if (Input.GetMouseButton(0))
			// {
			// 	lp = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			// 	currentSwipe = new Vector2(lp.x - fp.x, lp.y - fp.y);
			// 	if (currentSwipe.magnitude < minSwipeLength)
			// 	{
			// 		//Debug.Log("Return");
			// 		return;
			// 	}

			// 	currentSwipe.Normalize();				

			// 	if ((currentSwipe.x > 0.3f && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f))
			// 	{
			// 		PlayerShield.transform.Rotate (0,0,PlayerPrefs.GetFloat ("WallRotationSpeed"));
			// 	}
			// 	else if((currentSwipe.x < 0.3f && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f))
			// 	{
			// 		PlayerShield.transform.Rotate (0,0,-PlayerPrefs.GetFloat ("WallRotationSpeed"));
			// 	}
			// 	fp = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			// }
			if( Screen.height > 60 )
			{
				DetectSwipe();
			}			
		}
	}

	public void DetectSwipe ()
	{
		if (Input.touches.Length > 0)
		{
			Touch t = Input.GetTouch(0);


			if (t.phase == TouchPhase.Began) 
			{
				fp = new Vector2(t.position.x, t.position.y);
			}

			if (t.phase == TouchPhase.Moved)
			{
				lp = new Vector2(t.position.x, t.position.y);
				currentSwipe = new Vector3(lp.x - fp.x, lp.y - fp.y);


				Debug.Log ("Position__ "+this.transform.position + " Screen..." + Screen.height);
				if (currentSwipe.magnitude < minSwipeLength) 
				{
					
					return;
				}

				currentSwipe.Normalize();
				//Debug.LogError ("Distance is _= " + Dist);

				// if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) 
				// {
				// 		// Swipe up
				// 		//Vector3 pos = Camera.main.transform.forward;
				// 		this.transform.position += Camera.main.transform.forward * Time.deltaTime * (movementSpeed /2f);
				// 		this.transform.position = new Vector3(this.transform.position.x,SetPos.y,this.transform.position.z);
				// 		//this.transform.position += new Vector3(this.transform.position.x,this.transform.position.y,pos.y * Time.deltaTime * (movementSpeed /2f));
				// 		Debug.Log("UP "+this.transform.position);
				// } 
				// if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
				// {
				// 		// Swipe down	
				// 		//Vector3 pos = Camera.main.transform.forward;
				// 		Debug.Log("Dist  "+Vector3.Distance(this.transform.position,Camera.main.transform.position));
				// 		if(Vector3.Distance(this.transform.position,Camera.main.transform.position) >=2f)
				// 		{
				// 			this.transform.position -= Camera.main.transform.forward * Time.deltaTime * (movementSpeed /2f);
				// 			this.transform.position = new Vector3(this.transform.position.x,SetPos.y,this.transform.position.z);
				// 			//this.transform.position -= new Vector3(this.transform.position.x,this.transform.position.y,pos.y * Time.deltaTime * (movementSpeed /2f));	
				// 			Debug.Log("Down "+this.transform.position);	
				// 		}	
				// }
				if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
				{
					// Swipe left	
					PlayerShield.transform.Rotate (0,0,-PlayerPrefs.GetFloat ("WallRotationSpeed"));				
				}
				if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) 
				{
					// Swipe right
					PlayerShield.transform.Rotate (0,0,PlayerPrefs.GetFloat ("WallRotationSpeed"));
				}

				fp = new Vector2(t.position.x, t.position.y);
			}
		}
		else 
		{

		}
	}
}