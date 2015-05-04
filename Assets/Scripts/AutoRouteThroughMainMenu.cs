using UnityEngine;
using System.Collections;

public class AutoRouteThroughMainMenu : MonoBehaviour 
{
	protected int levelToLoad;

	// Use this for initialization
	void Start () 
	{
		if(!LevelManager.GetLevelManager())
		{
			DontDestroyOnLoad(gameObject);
			levelToLoad = Application.loadedLevel;
			
			if(levelToLoad > 0)
			{
				Application.LoadLevel(0);
			}
		}
	}
	
	void OnLevelWasLoaded(int level)
	{
		if(level == 0)
		{
			LevelManager.GetLevelManager().LoadLevel(levelToLoad);
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
