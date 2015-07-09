using UnityEngine;
using System.Collections;
using System;

public class SetFrameRate : MonoBehaviour {

	public int desiredFPS = 120;
	
	void Awake()
	{
		Application.targetFrameRate = 60;
		QualitySettings.vSyncCount = -1;
	}
	
	void Update()
	{
		long lastTicks = DateTime.Now.Ticks;
		long currentTicks = lastTicks;
		float delay = 1f / desiredFPS;
		float elapsedTime;
		
		if (desiredFPS <= 0)
			return;
		
		while (true)
		{
			currentTicks = DateTime.Now.Ticks;
			elapsedTime = (float)TimeSpan.FromTicks(currentTicks - lastTicks).TotalSeconds;
			if(elapsedTime >= delay)
			{
				break;
			}
		}
	}
}
