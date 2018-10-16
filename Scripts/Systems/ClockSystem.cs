using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;
using UnityEngine.UI;
using DSA.Extensions.Base;

namespace DSA.Extensions.GameTime
{
	public class ClockSystem : ComponentSystem
	{
		private struct ClockGroup
		{
			public Text Text;

			public DailyUpdateComponent UpdateComponent;

			public TextValueComponent TextValue;
		}

		private float dailySeconds;
		public float DailySeconds { get { return dailySeconds; } }
		private TimeSpan timeOfDay;
		public TimeSpan TimeOfDay { get { return timeOfDay; } }
		[Inject] GameTimeSystem gameTimeSystem;
		private int lastHour;

		protected override void OnCreateManager()
		{
			dailySeconds = 0;
			timeOfDay = new TimeSpan(0, 0, 0);
			lastHour = 0;
		}

		protected override void OnUpdate()
		{
			dailySeconds = gameTimeSystem.TimeCounter;
			if (dailySeconds > 86400) { dailySeconds -= 86400; }
			timeOfDay = new TimeSpan(gameTimeSystem.GameTime.Hours, gameTimeSystem.GameTime.Minutes, gameTimeSystem.GameTime.Seconds);
			if (gameTimeSystem.GameTime.Hours != lastHour)
			{
				Debug.Log("Time of day: " + timeOfDay.ToString());
				lastHour = gameTimeSystem.GameTime.Hours;
			}
			foreach (var entity in GetEntities<ClockGroup>())
			{
				entity.Text.text = timeOfDay.ToString();
			}
		}
	}
}