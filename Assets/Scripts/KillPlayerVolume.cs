using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class KillPlayerVolume : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.tag.Equals("Player"))
		{
			PlayerModel player = coll.gameObject.GetComponent<PlayerModel>();
			player.TakeDamage(player.GetHealth());
		}
	}
}
