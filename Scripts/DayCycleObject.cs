using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DSA.Extensions.Base;

namespace DSA.Extensions.GameTime
{
	public abstract class DayCycleObject : ExtendedMonoBehaviour, ITimeListener
	{
		public override ExtensionEnum Extension { get { return ExtensionEnum.Time; } }
		[SerializeField] protected float midnightValue;
		[SerializeField] protected float noonValue;
		protected float timeElapsed;
		protected float nextValue;
		protected TimeSpan lastTime;
		protected TimeSpan nextTime;
		protected TimeSpan currentTime;
		protected float timeSpeed;
		protected float realTimeIncrement;
		protected float lastValue;
		protected float currentValue;

		public Func<bool> TimeProcessingFunction { get; set; }

		protected abstract void SetLastValue();

		public void UpdateTime(TimeSpan sentLastUpdate, TimeSpan sentCurrentTime, TimeSpan sentNextUpdate, float sentTimeSpeed)
		{
			timeElapsed = 0;
			lastTime = sentLastUpdate;
			nextTime = sentNextUpdate;
			currentTime = sentCurrentTime;
			timeSpeed = sentTimeSpeed;
			realTimeIncrement = ((float)nextTime.TotalSeconds - (float)currentTime.TotalSeconds) / timeSpeed;
			SetNextValue();
			SetLastValue();
		}

		protected abstract void SetNextValue();
	}
}