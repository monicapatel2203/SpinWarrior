using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Objectswipe : MonoBehaviour 
{	
public RaycastHit _Hit;
public LayerMask _RaycastCollidableLayers ; //Set this in inspector, makes you able to say which layers should be collided with and which not.
public float _CheckDistance = 100f;


	public static Objectswipe ins;
	public float movementSpeed ;
	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;
	public float minSwipeLength;
	
	public bool MoveStart;
	
	public Vector3 SetPos;
	 Vector3 touchPosWorld;
	
	void Start()
	{	
		Debug.Log("Monic call");
		MoveStart = false;
		movementSpeed = 10;
		minSwipeLength = 2;
	}

   	private bool IsPointerOverUIObject()
	{
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}
	void Update()
	{
		// if (IsPointerOverUIObject ()) 
		// {
		// 	return;
		// }
		int fingersOnScreen = 0;
		foreach (Touch touch in Input.touches)
		{
			fingersOnScreen++;
			if (fingersOnScreen == 2) 
			{
				return;
			}
		}
		if(MoveStart)		
		{		
			if (Application.loadedLevel == 2)
			{
				if (Input.touchCount == 1 )
        		{
					if(this.gameObject.name.Contains("Image") ||this.gameObject.name.Contains("imgGallary") ||this.gameObject.name.Contains("LinkWeb_New") )
					{
						DetectSwipe();
					}
					else
					{							   
						if(Input.GetTouch(0).position.y <= Screen.height/4)
						{
						//	Debug.Log("not move Return");
							return;
						}
						else
						{
							//Debug.Log("move Return");
							DetectSwipe();
						}
					}
				}				
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
				firstPressPos = new Vector2(t.position.x, t.position.y);
			}

			if (t.phase == TouchPhase.Moved)
			{
				secondPressPos = new Vector2(t.position.x, t.position.y);
				currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);


				Debug.Log ("Position__ "+this.transform.position);
				if (currentSwipe.magnitude < minSwipeLength) 
				{
					
					return;
				}

				currentSwipe.Normalize();
				//Debug.LogError ("Distance is _= " + Dist);

				if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) 
				{
						// Swipe up
						//Vector3 pos = Camera.main.transform.forward;
						this.transform.position += Camera.main.transform.forward * Time.deltaTime * (movementSpeed /2f);
						this.transform.position = new Vector3(this.transform.position.x,SetPos.y,this.transform.position.z);
						//this.transform.position += new Vector3(this.transform.position.x,this.transform.position.y,pos.y * Time.deltaTime * (movementSpeed /2f));
						Debug.Log("UP "+this.transform.position);
				} 
				if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
				{
						// Swipe down	
						//Vector3 pos = Camera.main.transform.forward;
						Debug.Log("Dist  "+Vector3.Distance(this.transform.position,Camera.main.transform.position));
						if(Vector3.Distance(this.transform.position,Camera.main.transform.position) >=2f)
						{
							this.transform.position -= Camera.main.transform.forward * Time.deltaTime * (movementSpeed /2f);
							this.transform.position = new Vector3(this.transform.position.x,SetPos.y,this.transform.position.z);
							//this.transform.position -= new Vector3(this.transform.position.x,this.transform.position.y,pos.y * Time.deltaTime * (movementSpeed /2f));	
							Debug.Log("Down "+this.transform.position);	
						}	
				}
				if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
				{
						// Swipe left	
						this.transform.position -= Camera.main.transform.right * Time.deltaTime * (movementSpeed /2f);
						this.transform.position = new Vector3(this.transform.position.x,SetPos.y,this.transform.position.z);				
				}
				if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) 
				{
						// Swipe right
						this.transform.position += Camera.main.transform.right * Time.deltaTime * (movementSpeed /2f);
						this.transform.position = new Vector3(this.transform.position.x,SetPos.y,this.transform.position.z);
				}

				firstPressPos = new Vector2(t.position.x, t.position.y);
			}
		}
		else 
		{

		}
	}
}
