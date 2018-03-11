using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSA.Extensions.Base;

namespace DSA.Extensions.GameTime
{
	public class TimeManager : ManagerBase
	{
		public override ExtensionEnum.Extension Extension { get { return ExtensionEnum.Extension.Time; } }

		private float timeRatio;
		[SerializeField] private float minutesPerDay;
		[SerializeField] private System.TimeSpan timeElapsed;
		[SerializeField] private string timeElapsedString;
		[SerializeField] private System.TimeSpan gameTime;
		[SerializeField] private string gameTimeString;
		private float timeIncrement;

		[SerializeField] private Sun sun;

		public override void Initialize()
		{
			base.Initialize();
			timeRatio = 1440F / minutesPerDay;
			timeElapsed = new System.TimeSpan();
			timeElapsedString = timeElapsed.ToString();
			gameTime = new System.TimeSpan(0, 12, 0, 0);
			gameTimeString = gameTime.ToString();
			timeIncrement = 1;
			if (sun == null) sun = FindObjectOfType<Sun>();
			sun.Set(timeRatio);
			sun.SetTimeProcessingFunction(GetIsProcessing);

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
					int ratioInt = Mathf.RoundToInt(timeRatio);
					gameTime += new System.TimeSpan(0, 0, ratioInt);
					gameTimeString = gameTime.ToString();
					timeIncrement += 1;
				}
				yield return null;
			}
		}

		public bool GetIsProcessing()
		{
			return IsProcessing;
		}

		public override void AddDataToArrayList(ArrayList sentArrayList)
		{
			TimeProgress progress = new TimeProgress(gameTime, new TransformValue(sun.transform));
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
					sun.SetPosition(progress.SunPosition);
				}
			}
		}
	}
}