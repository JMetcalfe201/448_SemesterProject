using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerModel : MonoBehaviour 
{
	[SerializeField] PlayerController controller;

	[SerializeField] protected float moveVelocity;
	
	[SerializeField] protected float jumpForce;
	protected bool isGrounded;
	protected int numJumps;
	[SerializeField] protected int maxJumps;
	
	[SerializeField] protected int health;

	protected Rigidbody2D physics;

	// Use this for initialization (inherated from MonoBehavior)
	protected void Start () 
	{
		isGrounded = false;
		numJumps = 0;
		
		// Make sure the player can jump at least once
		if(maxJumps < 1)
		{
			maxJumps = 1;
		}
		
		physics = GetComponent<Rigidbody2D>();
		
		controller.OnUpdateHealth(health);
	}
	
	protected void Update ()
	{
		// If not on the ground or falling (don't check when rising or player could get extra jump)
		if(!isGrounded && physics.velocity.y <= 0)
		{
			CheckGrounding();
		}
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
		if(numJumps < maxJumps)
		{
			// Zero out the player's vertical speed to combat compounded jumping
			physics.velocity = new Vector2(physics.velocity.x, 0);
			
			// Adds an upward force to the player
			physics.AddForce(new Vector2(0 , jumpForce), ForceMode2D.Impulse);
			
			isGrounded = false;
			numJumps++;
		}
	}
	
	public void TakeDamage(int amount)
	{
		health -= amount;
		
		// Pass the update up the chain
		controller.OnUpdateHealth(health);
			
			
		// Clamp health and check for death
		if(health <= 0)
		{
			health = 0;
			Die ();
		}
	}
	
	protected void Die()
	{
		// Pass the call up
		controller.OnDie();
		
		// Turn off collision and throw the player up in the air and spin them
		GetComponent<BoxCollider2D>().enabled = false;
		physics.fixedAngle = false;
		physics.AddForce(new Vector2(Random.Range(-3,3), 25), ForceMode2D.Impulse);
		physics.AddTorque(500f);
	}
	
	public void CheckGrounding()
	{
		BoxCollider2D col = GetComponent<BoxCollider2D>();
	
		RaycastHit2D hit;
		
		// Slide the player collider down 0.05 units to check if there is an object on the Ground layer to see if there is ground below
		hit = Physics2D.BoxCast(gameObject.transform.position + new Vector3(col.offset.x, col.offset.y, 0), col.size, 0, new Vector2(0, -1), 0.05f, 1 << 8);
		
		// Check if anythign was hit
		if(hit.collider)
		{
			isGrounded = true;
			numJumps = 0;
		}
    }
	
	protected void OnCollisionExit2D(Collision2D collision)
	{
		if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			TimerManager.GetWorldTimerManager().AddTimer(0.1f, this.CheckGrounding);
		}
	}
}
