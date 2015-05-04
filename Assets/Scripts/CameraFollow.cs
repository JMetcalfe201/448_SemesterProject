using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	protected Transform target;
	
	[SerializeField] protected float followSpeed;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnLevelWasLoaded(int level)
	{
		GameObject player = GameObject.FindWithTag("Player");
		
		if(player)
		{
			target = player.transform;
		}
	}
	
	void LateUpdate()
	{
		if(target && !Application.isLoadingLevel)
		{
			Vector2 direction = new Vector2(target.position.x, target.position.y) - new Vector2(transform.position.x, transform.position.y);
			float distance = direction.magnitude;
			
			if(distance < followSpeed * Time.deltaTime)
			{
				transform.position = transform.position + new Vector3(direction.x, direction.y, 0);
			}else{
				transform.position = transform.position + (new Vector3(direction.x, direction.y, 0).normalized * followSpeed * Time.deltaTime);
			}
		}
	}
}
