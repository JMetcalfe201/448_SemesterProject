using UnityEngine;
using System.Collections;

public class DebugLevelLoader : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.anyKeyDown && Application.isEditor)
		{
			if(Input.GetKeyDown(KeyCode.Alpha0))
			{
				Load (0);
			}else if(Input.GetKeyDown(KeyCode.Alpha1))
			{
				Load (1);
			}else if(Input.GetKeyDown(KeyCode.Alpha2))
			{
				Load (2);
			}else if(Input.GetKeyDown(KeyCode.Alpha3))
			{
				Load (3);
			}else if(Input.GetKeyDown(KeyCode.Alpha4))
			{
				Load (4);
			}else if(Input.GetKeyDown(KeyCode.Alpha5))
			{
				Load (5);
			}else if(Input.GetKeyDown(KeyCode.Alpha6))
			{
				Load (6);
			}else if(Input.GetKeyDown(KeyCode.Alpha7))
			{
				Load (7);
			}else if(Input.GetKeyDown(KeyCode.Alpha8))
			{
				Load (8);
			}else if(Input.GetKeyDown(KeyCode.Alpha9))
			{
				Load (9);
			}
		}
	}
	
	void Load(int level)
	{
		LevelManager.GetLevelManager().LoadLevel(level);
	}
}
