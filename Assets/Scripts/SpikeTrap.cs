using UnityEngine;
using System.Collections;

public class SpikeTrap : MonoBehaviour
{
	[SerializeField] protected int damage;
	protected bool alreadyDoneDamage = false;
	
	// Built in callback for overlaps on 2d colliders marked as trigger when entering collision
	protected void OnTriggerEnter2D(Collider2D col)
	{
		// If the object is a player and we have not already damaged him
		if(col.name.Equals("Player") && !alreadyDoneDamage)
		{
			// Tell the player model component to take damage
			col.gameObject.GetComponent<PlayerModel>().TakeDamage(damage);
			
			// Set flag so damage is only given once per overlap
			alreadyDoneDamage = true;
		}
	}
	
	// Built in callback for overlaps on 2d colliders marked as trigger when exiting collision
	protected void OnTriggerExit2D(Collider2D col)
	{
		// If it is the player leaving
		if(col.name.Equals("Player"))
		{
			// Reset flag
			alreadyDoneDamage = false;
		}
	}
}
