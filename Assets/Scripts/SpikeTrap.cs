using UnityEngine;
using System.Collections;

public class SpikeTrap : MonoBehaviour
{
	[SerializeField] protected int damage;
	protected bool alreadyDoneDamage = false;
	
	protected void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("trigger overlap");
	
		if(col.name.Equals("Player") && !alreadyDoneDamage)
		{
			col.gameObject.GetComponent<PlayerModel>().TakeDamage(damage);
			alreadyDoneDamage = true;
		}
	}
	
	protected void OnTriggerExit2D(Collider2D col)
	{
		if(col.name.Equals("Player"))
		{
			alreadyDoneDamage = false;
		}
	}
}
