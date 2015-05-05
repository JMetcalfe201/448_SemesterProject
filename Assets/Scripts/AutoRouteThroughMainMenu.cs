using UnityEngine;
using System.Collections;

public class AutoRouteThroughMainMenu : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		if(!LevelManager.GetLevelManager())
		{
			Debug.LogError("When testing you must hit play from the main_menu level. Then press the number on the keyboard for the index of the level you want to load.\n Make sure the level you want is added in File->Build Options");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
