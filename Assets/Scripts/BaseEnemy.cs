using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class BaseEnemy : MonoBehaviour 
{
	[SerializeField] protected int health;
	[SerializeField] protected float speed;
	[SerializeField] protected int damage;
	
	[SerializeField] protected bool movingLeft;
	
	protected bool alive;
	
	protected BoxCollider2D coll;
	protected Rigidbody2D physics;
	
	protected int scoreWorth;
	
	protected bool alreadySentDamage;
	
	[SerializeField] protected Sprite deathSprite;

	// Use this for initialization
	protected void Start () 
	{
		tag = "Enemy";
	
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
		float dirX = movingLeft ? -1.0f : 1.0f;
		
		physics.AddForce(new Vector2(dirX * 100f, 0), ForceMode2D.Force);
		
		if(physics.velocity.x > speed || physics.velocity.x < -speed)
		{
			physics.velocity = new Vector2(speed*dirX, physics.velocity.y); 
		}
	}
	
	protected virtual void Die()
	{
		TimerManager.GetWorldTimerManager().RemoveTimer(this.CheckDirectionChange);
		GetComponent<SpriteRenderer>().sprite = deathSprite;
	
		// Turn off collision and throw the player up in the air and spin them
		GetComponent<BoxCollider2D>().enabled = false;
		physics.fixedAngle = false;
		physics.AddForce(new Vector2(Random.Range(-3f,3f), 25f), ForceMode2D.Impulse);
		physics.AddTorque(500f);
		
		TimerManager.GetWorldTimerManager().AddTimer(4f, this.FinalizeDeath);
	}
	
	public void FinalizeDeath()
	{
		Destroy(gameObject);
	}
	
	// Box cast infront of this enemy downward to see if it will fall
	public virtual void CheckDirectionChange()
	{
		float dirX = movingLeft ? -1.0f : 1.0f;
	
		RaycastHit2D hit;
		hit = Physics2D.BoxCast(transform.position + new Vector3(dirX * coll.size.x + 0.25f, 0, 0), coll.size, 0, new Vector2(0, -1), 5f, 1 << LayerMask.NameToLayer("Ground"));

		if(hit.collider == null)
		{
			movingLeft = !movingLeft;
			physics.velocity = new Vector2(-physics.velocity.x, physics.velocity.y);
		}
	}
	
	public void TakeDamage(int amount)
	{
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
		if(col.gameObject.name.Equals("Player"))
		{	
			// If the player is not above this enemy
			if(!(col.gameObject.transform.position.x > transform.position.x - (coll.size.x * 1.5f) && col.gameObject.transform.position.x < transform.position.x + (coll.size.x * 1.25f) && col.gameObject.transform.position.y > transform.position.y + coll.size.y/2))
			{
				col.gameObject.GetComponent<PlayerModel>().TakeDamage(damage);
				col.gameObject.GetComponent<Rigidbody2D>().AddForce((col.transform.position - transform.position).normalized * (damage * 50f), ForceMode2D.Impulse);
				alreadySentDamage = true;
			}
		}
	}
	
	public void OnCollisionExit2D (Collision2D col)
	{
		if(col.gameObject.name.Equals("Player"))
		{
			alreadySentDamage = false;
		}
	}
}
