using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerView : MonoBehaviour 
{
	[SerializeField] protected PlayerController controller;
	
	[SerializeField] protected Canvas canvas;
	[SerializeField] protected GameObject heartLayout;
	[SerializeField] protected GameObject heartPrefab;
	
	[SerializeField] protected List<GameObject> healthHearts;

	// Use this for initialization (inherated from MonoBehavior)
	protected void Start () 
	{
	
	}
	
	// Update is called once per frame (inherated from MonoBehavior)
	private void Update () 
	{
		HandleInput ();
		
		
		//For testing////
		if(Input.GetKeyDown(KeyCode.KeypadPlus))
		{
			UpdateHealth(healthHearts.Count+1);
		}
		
		if(Input.GetKeyUp(KeyCode.KeypadMinus))
		{
			UpdateHealth(healthHearts.Count-1);
		}
		////////////////
	}
	
	private void HandleInput()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");
		bool jumpInput = Input.GetButtonDown("Jump");
		
		if(horizontalInput != 0)
		{
			controller.OnMoveRight(horizontalInput);
		}
		
		if(verticalInput >= 0.8f)
		{
			controller.OnJump();
		}
		
		if(jumpInput)
		{
			controller.OnJump();
		}
	}
	
	public void UpdateHealth(int newHealth)
	{
		if(newHealth >= 0)
		{
			while(newHealth < healthHearts.Count)
			{
				if(healthHearts.Count > 0)
				{
					GameObject heart = healthHearts[healthHearts.Count-1];
					healthHearts.Remove(heart);
					Destroy(heart);
				}
			}
			
			while(newHealth > healthHearts.Count)
			{
				GameObject newHeart = Instantiate(heartPrefab, Vector3.zero, Quaternion.identity) as GameObject;
				newHeart.transform.SetParent(heartLayout.transform);
				healthHearts.Add(newHeart);
			}
		}
	}
}
