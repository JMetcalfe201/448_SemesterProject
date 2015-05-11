using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class BaseEnemy : MonoBehaviour 
{
	[SerializeField] protected int scoreValue;

	[SerializeField] protected int health;
	[SerializeField] protected float speed;
	[SerializeField] protected int damage;
	
	[SerializeField] protected bool movingLeft;
	
	protected bool alive;
	
	protected BoxCollider2D coll;
	protected Rigidbody2D physics;
	
	protected bool alreadySentDamage;
	
	[SerializeField] protected Sprite deathSprite;

	// Use this for initialization
	protected void Start () 
	{
		tag = "Enemy";
	
		// Get highly accessed componenets
		coll = GetComponent<BoxCollider2D>();
		physics = GetComponent<Rigidbody2D>();
	
		alreadySentDamage = false;
		
		alive = true;
		
		// Set a timer to check if the character has reached the end of the platform every 1 second
		TimerManager.GetWorldTimerManager().AddTimer(.02f, this.CheckDirectionChange, 0, true);
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		if(alive)
			Move ();
	}
	
	protected virtual void Move()
	{
		// Get the direction
		float dirX = movingLeft ? -1.0f : 1.0f;
		
		// Add teh move force
		physics.AddForce(new Vector2(dirX * 100f, 0), ForceMode2D.Force);
		
		// Clamp the speed
		if(physics.velocity.x > speed || physics.velocity.x < -speed)
		{
			physics.velocity = new Vector2(speed*dirX, physics.velocity.y); 
		}
	}
	
	protected virtual void Die()
	{
		// Remove the direction checking timer
		TimerManager.GetWorldTimerManager().RemoveTimer(this.CheckDirectionChange);
		
		// Update the sprite
		GetComponent<SpriteRenderer>().sprite = deathSprite;
	
		// Turn off collision and throw the player up in the air and spin them
		GetComponent<BoxCollider2D>().enabled = false;
		physics.fixedAngle = false;
		physics.AddForce(new Vector2(Random.Range(-3f,3f), 25f), ForceMode2D.Impulse);
		physics.AddTorque(500f);
		
		// Add to player's score
		LevelManager.GetLevelManager().AddScore(scoreValue);
		
		// Set the object to be deleted
		TimerManager.GetWorldTimerManager().AddTimer(4f, this.FinalizeDeath);
	}
	
	public void FinalizeDeath()
	{
		Destroy(gameObject);
	}
	
	// Box cast infront of this enemy downward to see if it will fall
	public virtual void CheckDirectionChange()
	{
		// Find current direction
		float dirX = movingLeft ? -1.0f : 1.0f;
	
		// Cast a box infront of the character to see if it is the edge of the platform
		RaycastHit2D hit;
		hit = Physics2D.BoxCast(transform.position + new Vector3(dirX * coll.size.x + 0.25f, 0, 0), coll.size, 0, new Vector2(0, -1), 5f, 1 << LayerMask.NameToLayer("Ground"));
	
		// If no ground was hit then we are at the edge and switch direction
		if(hit.collider == null)
		{
			movingLeft = !movingLeft;
			physics.velocity = new Vector2(-physics.velocity.x, physics.velocity.y);
		}
	}
	
	public void TakeDamage(int amount)
	{
		// Decrement health and check for death
		health -= amount;
		
		if(health <= 0)
		{
			health = 0;
			Die ();
		}
	}
	
	public void OnCollisionEnter2D (Collision2D col)
	{
		// If collision with the player
		if (col.gameObject.name.Equals ("Player")) {	
			// If the player is not above this enemy
			if (!(col.gameObject.transform.position.x > transform.position.x - (coll.size.x * 1.5f) && col.gameObject.transform.position.x < transform.position.x + (coll.size.x * 1.25f) && col.gameObject.transform.position.y > transform.position.y + coll.size.y / 2)) {
				col.gameObject.GetComponent<PlayerModel> ().TakeDamage (damage);
				col.gameObject.GetComponent<Rigidbody2D> ().AddForce ((col.transform.position - transform.position).normalized * (damage * 50f), ForceMode2D.Impulse);
				alreadySentDamage = true;
			}
		}
		if (col.gameObject.tag.Equals("Wall") || col.gameObject.tag.Equals("Enemy")) 
		{
			Debug.Log("Turning around : coll with " + col.gameObject.name);
			movingLeft = !movingLeft;
			physics.velocity = new Vector2(-physics.velocity.x, physics.velocity.y);
		}
	}
	
	public void OnCollisionExit2D (Collision2D col)
	{
		// Reset so the player can take damage again
		if(col.gameObject.name.Equals("Player"))
		{
			alreadySentDamage = false;
		}
	}
}
