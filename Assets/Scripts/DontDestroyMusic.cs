using UnityEngine;
using System.Collections;

public class DontDestroyMusic : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		name = "M";
		
		if(GameObject.Find("Music"))
		{
			Destroy(gameObject);
		}else{
			name = "Music";
		}
	
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
