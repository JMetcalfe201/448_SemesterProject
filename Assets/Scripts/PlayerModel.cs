using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerModel : MonoBehaviour 
{
	[SerializeField] PlayerController controller;

	[SerializeField] protected float jumpForce;
	[SerializeField] protected float moveVelocity;
	[SerializeField] protected int health;

	protected Rigidbody2D physics;

	// Use this for initialization (inherated from MonoBehavior)
	protected void Start () 
	{
		physics = GetComponent<Rigidbody2D>();
		
		controller.OnUpdateHealth(health);
	}
	
	public void MoveRight(float amount)
	{
		Vector2 wantedVelocity = new Vector2(moveVelocity * amount, physics.velocity.y);

		//want right
		if(amount > 0)
		{
			if(physics.velocity.x > 0)//is moving right
			{
				if(wantedVelocity.x > physics.velocity.x)//if wanted speed is more than current
				{
					physics.velocity = wantedVelocity; //set the player's speed
				}else if(wantedVelocity.x < physics.velocity.x)//if wanted speed is less than current
				{
					//dont set the speed to lower than current unless switching directions (this keeps inertia if the player keeps the stick neutral)
				}
			}else if(physics.velocity.x <= 0)//is moving left
			{
				physics.velocity = wantedVelocity; //set the player's speed (switch directions)
			}
		}else if(amount < 0) //want left
		{
			if(physics.velocity.x < 0)//is moving left
			{
				if(wantedVelocity.x < physics.velocity.x)//if wanted speed is more than current
				{
					physics.velocity = wantedVelocity; //set the player's speed
				}else if(wantedVelocity.x > physics.velocity.x)//if wanted speed is less than current
				{
					//dont set the speed to lower than current unless switching directions (this keeps inertia if the player keeps the stick neutral)
                }
            }else if(physics.velocity.x >= 0)//is moving right
            {
                physics.velocity = wantedVelocity; //set the player's speed (switch directions)
            }
        }
    }
	
	public void Jump()
	{
		// Zero out the player's vertical speed to combat compounded jumping
		physics.velocity = new Vector2(physics.velocity.x, 0);
		
		// Adds an upward force to the player
		physics.AddForce(new Vector2(0 , jumpForce), ForceMode2D.Impulse);
	}
	
	public void TakeDamage(int amount)
	{
		health -= amount;
		
		// Pass the update up the chain
		controller.OnUpdateHealth(health);
		
		if(health <= 0)
		{
			health = 0;
			Die ();
		}
	}
	
	protected void Die()
	{
		// Temp
		Debug.Log("Player is dead");
	}
}
