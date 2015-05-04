using UnityEngine;
using System.Collections;

public class AutoAddUIToCanvas : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		transform.SetParent(GameObject.Find ("GameplayUIGroup").transform);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
