using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	protected int score;
	
	[SerializeField] protected Text ScoreText;
	[SerializeField] protected UIGroup LoadingScreenGroup;
	[SerializeField] protected UIGroup GameplayUIGroup;
	[SerializeField] protected UIGroup WinningScreenGroup;
	[SerializeField] protected GameObject Canvas;
	[SerializeField] protected GameObject EventSystem;
	
	protected float loadingTime;
	[SerializeField] protected float minLoadingTime;


	// Use this for initialization
	void Start () 
	{
		// Make this object persistance between level loading
		DontDestroyOnLoad(gameObject);
		DontDestroyOnLoad(this);
		DontDestroyOnLoad(Canvas);
		DontDestroyOnLoad(EventSystem);
		
		LoadingScreenGroup.HideUI();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Increment the timer while the level is loading
		if(Application.isLoadingLevel)
		{
			loadingTime += Time.deltaTime;
		}
	}
	
	public void AddScore(int points)
	{	
		// Update the score
		score += points;
		
		// Update the UI text for score
		ScoreText.text = score.ToString();
	}
	
	protected void OnLevelWasLoaded(int level)
	{
		// If the level was loading for too short of a time to show the screen then wait until the min time to hide it
		if(loadingTime < minLoadingTime)
		{
			TimerManager.GetWorldTimerManager().AddTimer(minLoadingTime - loadingTime, this.HideLoadingScreen);
		}else{
			HideLoadingScreen();
		}
		
		// Reset score and show score if this is not the main menu
		score = 0;
		
		if(Application.loadedLevel > 0)
		{
			ScoreText.text = score.ToString();
		}
	}
	
	public void LoadNextLevel()
	{	
		LoadLevel (Application.loadedLevel + 1);
	}
	
	public void LoadLevel(int level)
	{
		// Destory the start button if leaving the main menu
		if(Application.loadedLevel == 0)
		{
			Destroy (GameObject.Find ("Start Button"));
		}
		
		if(level == 0)
		{
			Destroy(Canvas);
			Destroy(EventSystem);
		}
		
		// Show the loading screen and start loading
		ShowLoadingScreen();
		Application.LoadLevel(level);
		
		if(level == 0)
		{
			Destroy (gameObject);
		}
	}
	
	public void ResetLevel()
	{
		ShowLoadingScreen();
		
		Application.LoadLevel(Application.loadedLevel);
	}
	
	public void Quit()
	{
		Application.Quit();
	}
		
	protected void ShowLoadingScreen()
	{	
		// Turn on the loading screen UI objects
		LoadingScreenGroup.ShowUI();
		GameplayUIGroup.HideUI();
		
		// Update the previous level score
		Text prevScoreText = GameObject.Find ("Loading Score Text").GetComponent<Text>();
		
		if(score == 0)
		{
			prevScoreText.text = "";
		}else{
			prevScoreText.text = "Previous Score: " + score;
		}
	}
	
	protected void HideLoadingScreen()
	{	
		// Deactivate the loading screen UI objects
		LoadingScreenGroup.HideUI();
		
		if(!Application.loadedLevelName.Equals("level_Victory"))
		{
			GameplayUIGroup.ShowUI();
		}else{
			WinningScreenGroup.ShowUI();
		}
	}
	
	// So this object is availiable from any where
	public static LevelManager GetLevelManager()
	{
		GameObject go = GameObject.Find("Level Manager");
		
		if(go)
		{
			return go.GetComponent<LevelManager>();
		}else{
			return null;
		}
	}
}
