using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class BaseEnemy : MonoBehaviour 
{
	[SerializeField] protected int health;
	[SerializeField] protected float speed;
	[SerializeField] protected int damage;
	
	protected bool alreadySentDamage;

	// Use this for initialization
	protected void Start () 
	{
		alreadySentDamage = false;
	}
	
	// Update is called once per frame
	protected void Update () 
	{
		
	}
	
	protected void Die()
	{
	
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
		if(col.gameObject.name.Equals("Player"))
		{
			col.gameObject.GetComponent<PlayerModel>().TakeDamage(damage);
			alreadySentDamage = true;
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
