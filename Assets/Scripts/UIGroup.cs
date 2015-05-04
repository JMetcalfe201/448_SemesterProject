using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIGroup : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void ShowUI()
	{
		foreach(Image im in GetComponentsInChildren<Image>())
		{
			im.enabled = true;
		}
		foreach(Text im in GetComponentsInChildren<Text>())
		{
			im.enabled = true;
		}
		foreach(SpriteRenderer im in GetComponentsInChildren<SpriteRenderer>())
		{
			im.enabled = true;
		}
	}
	
	public void HideUI()
	{
		foreach(Image im in GetComponentsInChildren<Image>())
		{
			im.enabled = false;
		}
		foreach(Text im in GetComponentsInChildren<Text>())
		{
			im.enabled = false;
		}
		foreach(SpriteRenderer im in GetComponentsInChildren<SpriteRenderer>())
		{
			im.enabled = false;
		}
	}
}
