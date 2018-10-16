using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DSA.Extensions.Base;

[System.Serializable]
public struct TimeProgress
{
	[SerializeField] private TimeSpan gameTime;
	public TimeSpan GameTime { get { return gameTime; } }

	public TimeProgress(TimeSpan sentTime)
	{
		gameTime = sentTime;
	}
}
