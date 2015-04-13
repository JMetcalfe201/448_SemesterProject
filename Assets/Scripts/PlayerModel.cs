using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerModel : MonoBehaviour 
{
	[SerializeField] PlayerController controller;

	[SerializeField] protected float jumpForce;
	[SerializeField] protected float moveForce;
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
		// Adds a lateral force to the player 
		//  -amount decides direction (and is a scalar if using a controller)
		//  -Time.deltaTime makes it so the force is independent of framerate
		physics.AddForce(new Vector2(moveForce * amount * Time.deltaTime, 0), ForceMode2D.Impulse);
	}
	
	public void Jump()
	{
		// Adds an upward force to the player
		physics.AddForce(new Vector2(0 , jumpForce), ForceMode2D.Impulse);
	}
	
	public void TakeDamage(int amount)
	{
		health -= amount;
		controller.OnUpdateHealth(health);
	}
	
	public void Die()
	{
		// Temp
		Debug.Log("Player is dead");
	}
}
