using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;
using System;

namespace DSA.Extensions.GameTime
{
	public class GameTimeSystem : ComponentSystem
	{
		private TimeSpan gameTime;
		public TimeSpan GameTime { get { return gameTime; } }
		private float timeCounter;
		public float TimeCounter { get { return timeCounter; } }
		private float minutesPerDay = 1;
		private float gameSpeed;

		protected override void OnCreateManager()
		{
			gameTime = new TimeSpan();
			gameSpeed = (1440F / minutesPerDay);
			timeCounter = 0;
		}

		protected override void OnUpdate()
		{
			timeCounter += Time.deltaTime * gameSpeed;
			gameTime = new TimeSpan(0, 0, Mathf.RoundToInt(timeCounter));
		}
	}
}