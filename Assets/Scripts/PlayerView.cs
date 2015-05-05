using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerView : MonoBehaviour 
{
	[SerializeField] protected PlayerController controller;
	protected bool movementEnabled;
	
	protected Canvas canvas;
	protected GameObject heartLayout;
	[SerializeField] protected GameObject heartPrefab;
	
	protected bool alreadySentAxisJump;
	
	[SerializeField] protected Sprite deathSprite;
	
	protected List<GameObject> healthHearts;

	public GameObject respawnButtonGameObject;
	public Button respawnButton;

	// Use this for initialization (inherated from MonoBehavior)
	protected void Awake () 
	{
		// Store highly accessed components
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		heartLayout = GameObject.Find ("Heart Layout");
		
		// Initialize hearts list
		healthHearts = new List<GameObject>();
	
		movementEnabled = true;
		
		alreadySentAxisJump = false;
	}
	
	// Update is called once per frame (inherated from MonoBehavior)
	private void Update () 
	{
		HandleInput ();
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
		}else if(verticalInput < 0.8f)
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
					OnPlayerStartHurt();
				}
			}
			
			// Increase health
			while(newHealth > healthHearts.Count)
			{
				// Create a new heart and put it in the layout
				GameObject newHeart = Instantiate(heartPrefab, Vector3.zero, Quaternion.identity) as GameObject;
				newHeart.GetComponent<Image>().enabled = false;
				newHeart.transform.SetParent(heartLayout.transform);
				healthHearts.Add(newHeart);
			}
		}
	}
	
	public void NotifyDestroy()
	{
		UpdateHealth(0);
	}
	
	protected void OnPlayerStartHurt()
	{
		// Changes player's color to red for a half second to notify damage to user
		GetComponent<SpriteRenderer>().color = new Color(.5f, .04f, .1f);
		TimerManager.GetWorldTimerManager().AddTimer(0.2f, this.OnPlayerStopHurt);
	}
	
	protected void OnPlayerStopHurt()
	{
		// Resets player's color to white
		GetComponent<SpriteRenderer>().color = Color.white;
	}
	
	public void BringSpriteForward()
	{
		GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
	}
	
	public void ResetSpriteSortDepth()
	{
		GetComponent<SpriteRenderer>().sortingLayerName = "Default";
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

	public void DisplayRespawnButton()
	{
		respawnButtonGameObject = GameObject.Find ("RespawnButton");
		respawnButton = respawnButtonGameObject.GetComponent<Button> ();
		respawnButton.GetComponent<UIGroup>().ShowUI();
	}
}
