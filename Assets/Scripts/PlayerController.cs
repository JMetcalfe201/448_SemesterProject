using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	// [SerializeField] exposes the variable in the Unity Editor
	[SerializeField] protected PlayerModel model;
	[SerializeField] protected PlayerView view;
	
	// Use this for initialization (inherated from MonoBehavior)
	protected void Start () 
	{

	}
	
	public void OnMoveRight(float amount)
	{
		model.MoveRight(amount);
	}
	
	public void OnJump()
	{
		model.Jump();
	}
	
	public void OnUpdateHealth(int health)
	{
		view.UpdateHealth(health);
	}
	
	public void OnDie()
	{
		view.SwitchToDeathSprite();
		view.SetMovementEnabled(false);

		view.DisplayRespawnButton ();
	}

	public void onRespawn ()
	{
		view.HideRespawnButton ();
	}
	
	public void NotifyGrounding(bool grounded)
	{
		if(grounded)
		{
			view.ResetSpriteSortDepth();
		}else{
			view.BringSpriteForward();
		}
	}
}
