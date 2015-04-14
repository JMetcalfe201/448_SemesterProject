using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimerManager : MonoBehaviour 
{
	public delegate void Delegate();

	protected struct Timer
	{
		public float expireTime;
		public Delegate expireFunction;
		public bool repeat;
		public float length;
		public int repeatedTimes;
		public int maxRepeatTimes;
	}
	
	protected List<Timer> timers;

	// Use this for initialization
	protected void Start () 
	{
		// Name the object so it can be retreived later
		gameObject.name = "World Timer Manager";
		
		// Initialize the list of timers
		timers = new List<Timer>();
	}
	
	// Update is called once per frame
	protected void Update () 
	{
		// Go through all the current timers
		for(int i = 0; i < timers.Count; i++)
		{
			// If the timer is expired
			if(timers[i].expireTime >= Time.time)
			{
				// Call the delegate
				timers[i].expireFunction();
				
				// If the timer should repeat
				if(timers[i].repeat && (timers[i].maxRepeatTimes == 0 || timers[i].repeatedTimes < timers[i].maxRepeatTimes))
				{
					// Recalculate the expire time and increment times repeated
					float leftOver = Time.time - timers[i].expireTime;
					Timer timer = timers[i];
					timer.expireTime += leftOver + timers[i].length;
					timer.repeatedTimes++;
					timers[i] = timer;
					
					// If this was the last time the timer should expire then remove it instead of waiting to for it to expire again
					if(timers[i].repeatedTimes == timers[i].maxRepeatTimes)
					{
						timers.RemoveAt(i);
					}
				}else
				{
					// Remove the timer since it is done repeating or shouldn't repeat
					timers.RemoveAt(i);	
				}
			}
		}
	}
	
	/* Parameters
	 * - length: the amount of time the timer will wait before calling the expireFunction
	 * - expireFunction: the function delegate that will be called when the timer expires
	 * - delay: the amount of time to wait before starting the timer (only effects first run)
	 * - repeat: whether the timer should repeat
	 * - maxRepeatTimes: the amount of times the timer should repeat (0 = repeat forever)
	 */
	public void AddTimer(float length, Delegate expireFunction, float delay = 0, bool repeat = false, int maxRepeatTimes = 0)
	{
		Timer newTimer;
		
		newTimer.expireTime = Time.time + delay + length;
		newTimer.expireFunction = expireFunction;
		newTimer.repeat = repeat;
		newTimer.length = length;
		newTimer.repeatedTimes = 0;
		newTimer.maxRepeatTimes = maxRepeatTimes;
		
		timers.Add(newTimer);
	}
	
	public void RemoveTimer(Delegate expireFunction)
	{
		for(int i = 0; i < timers.Count; i++)
		{
			if(timers[i].expireFunction == expireFunction)
			{
				timers.RemoveAt(i);
			}
		}
	}
	
	public static TimerManager GetWorldTimerManager()
	{
		return GameObject.Find("World Timer Manager").GetComponent<TimerManager>();
	}
}
