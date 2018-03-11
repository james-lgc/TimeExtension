﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DSA.Extensions.Base;

[System.Serializable]
public struct TimeProgress
{
	[SerializeField] private TimeSpan gameTime;
	public TimeSpan GameTime { get { return gameTime; } }

	[SerializeField] private TransformValue sunPosition;
	public TransformValue SunPosition { get { return sunPosition; } }

	public TimeProgress(TimeSpan sentTime, TransformValue sentTrans)
	{
		gameTime = sentTime;
		sunPosition = sentTrans;
	}
}
