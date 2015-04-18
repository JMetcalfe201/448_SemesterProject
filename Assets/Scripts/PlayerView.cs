using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerView : MonoBehaviour 
{
	[SerializeField] protected PlayerController controller;
	protected bool movementEnabled;
	
	[SerializeField] protected Canvas canvas;
	[SerializeField] protected GameObject heartLayout;
	[SerializeField] protected GameObject heartPrefab;
	
	protected bool alreadySentAxisJump;
	
	[SerializeField] protected Sprite deathSprite;
	
	[SerializeField] protected List<GameObject> healthHearts;

	// Use this for initialization (inherated from MonoBehavior)
	protected void Start () 
	{
		movementEnabled = true;
		
		alreadySentAxisJump = false;
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
		// Store all inputs in local variables
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");
		bool jumpInput = Input.GetButtonDown("Jump");
		
		// If the player is giving horizontal input and movement is enabled
		if(horizontalInput != 0 && movementEnabled)
		{
			// Pass down the call
			controller.OnMoveRight(horizontalInput);
		}
		
		// If the player has pressed the up joystick or key enough
		if(verticalInput >= 0.8f && movementEnabled && !alreadySentAxisJump)
		{
			// Pass down the call
			controller.OnJump();
			
			alreadySentAxisJump = true;
		}else if(alreadySentAxisJump)
		{
			alreadySentAxisJump = false;
		}
		
		// If the jump button has been pressed
		if(jumpInput && movementEnabled)
		{
			// Pass down the call
			controller.OnJump();
		}
	}
	
	public void UpdateHealth(int newHealth)
	{
		// Make sure it is a valid health value
		if(newHealth >= 0)
		{	
			// Decrease health
			while(newHealth < healthHearts.Count)
			{
				// If there is at least one heart left
				if(healthHearts.Count > 0)
				{
					// Find the last heart and delete it
					GameObject heart = healthHearts[healthHearts.Count-1];
					healthHearts.Remove(heart);
					Destroy(heart);
				}
			}
			
			// Increase health
			while(newHealth > healthHearts.Count)
			{
				// Create a new heart and put it in the layout
				GameObject newHeart = Instantiate(heartPrefab, Vector3.zero, Quaternion.identity) as GameObject;
				newHeart.transform.SetParent(heartLayout.transform);
				healthHearts.Add(newHeart);
			}
		}
	}
	
	public void SwitchToDeathSprite()
	{
		// Swap the player sprite
		GetComponentInChildren<SpriteRenderer>().sprite = deathSprite;
	}
	
	public void SetMovementEnabled(bool b)
	{
		// Set movment
		movementEnabled = b;
	}
}
