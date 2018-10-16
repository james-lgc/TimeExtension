using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSA.Extensions.Base;
using System;

namespace DSA.Extensions.GameTime
{
	public class TimeManager : ManagerBase
	{
		public override ExtensionEnum Extension { get { return ExtensionEnum.Time; } }

		private float timeSpeed;
		[SerializeField] private System.TimeSpan updateIncrement;
		[SerializeField] private float minutesPerDay;
		[SerializeField] private System.TimeSpan timeElapsed;
		[SerializeField] private string timeElapsedString;
		[SerializeField] private System.TimeSpan gameTime;
		[SerializeField] private string gameTimeString;
		private float timeIncrement;
		private TimeSpan lastUpdate;
		private TimeSpan nextUpdate;

		public delegate void OnTimeUpdateEvent(TimeSpan sentLastUpdate, TimeSpan sentCurrentTime, TimeSpan sentNextUpdate, float sentTimeSpeed);
		public OnTimeUpdateEvent OnTimeUpdate;

		public override void Initialize()
		{
			base.Initialize();
			updateIncrement = new TimeSpan(0, 3, 0, 0);
			timeSpeed = 1440F / minutesPerDay;
			timeElapsed = new TimeSpan();
			timeElapsedString = timeElapsed.ToString();
			gameTime = new TimeSpan(0, 12, 0, 0);
			lastUpdate = gameTime;
			nextUpdate = lastUpdate.Add(updateIncrement);
			gameTimeString = gameTime.ToString();
			timeIncrement = 1;
		}

		public override void Load()
		{
			OnTimeUpdate = null;
			DayCycleObject[] dayCycleObjects = FindObjectsOfType<DayCycleObject>();
			for (int i = 0; i < dayCycleObjects.Length; i++)
			{
				if (OnTimeUpdate != dayCycleObjects[i].UpdateTime)
				{
					OnTimeUpdate += dayCycleObjects[i].UpdateTime;
					dayCycleObjects[i].TimeProcessingFunction = GetIsProcessing;
				}
			}
			RaiseOnTimeUpdate();
		}

		private void SetUpdateTimes()
		{
			lastUpdate = nextUpdate;
			nextUpdate = lastUpdate.Add(updateIncrement);
		}

		protected override void StartProcess()
		{
			base.StartProcess();
			StartCoroutine(WaitForProcessEnd());
		}

		private IEnumerator WaitForProcessEnd()
		{
			while (IsProcessing == true)
			{
				if (Time.time >= timeIncrement)
				{
					timeElapsed += new System.TimeSpan(0, 0, 1);
					timeElapsedString = timeElapsed.ToString();
					int ratioInt = Mathf.RoundToInt(timeSpeed);
					gameTime += new System.TimeSpan(0, 0, ratioInt);
					gameTimeString = gameTime.ToString();
					timeIncrement += 1;
					if (gameTime >= nextUpdate)
					{
						SetUpdateTimes();
						RaiseOnTimeUpdate();
					}
				}
				yield return null;
			}
		}

		private void RaiseOnTimeUpdate()
		{
			if (OnTimeUpdate != null)
			{
				OnTimeUpdate(lastUpdate, gameTime, nextUpdate, timeSpeed);
			}
		}

		public bool GetIsProcessing()
		{
			return IsProcessing;
		}

		public override void AddDataToArrayList(ArrayList sentArrayList)
		{
			TimeProgress progress = new TimeProgress(gameTime);
			sentArrayList.Add(progress);
		}

		public override void ProcessArrayList(ArrayList sentArrayList)
		{
			for (int i = 0; i < sentArrayList.Count; i++)
			{
				if (sentArrayList[i] is TimeProgress)
				{
					TimeProgress progress = (TimeProgress)sentArrayList[i];
					gameTime = progress.GameTime;
				}
			}
		}
	}
}