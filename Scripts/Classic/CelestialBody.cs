using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DSA.Extensions.GameTime
{
	public class CelestialBody : DayCycleObject
	{
		[SerializeField] protected float xOffset;
		[SerializeField] protected float yOffset;
		[SerializeField] protected float zOffset;
		private Quaternion nextRotation;
		protected Quaternion lastRotation;

		public void Update()
		{
			if (!GetIsExtensionLoaded()) return;
			if (TimeProcessingFunction == null) { return; }
			if (!TimeProcessingFunction()) { return; }
			timeElapsed += Time.deltaTime;
			transform.rotation = Quaternion.Slerp(lastRotation, nextRotation, timeElapsed / realTimeIncrement);
		}

		protected override void SetNextValue()
		{
			float changeDivision = (midnightValue - noonValue) / 12;
			TimeSpan timeDifference = nextTime.Subtract(currentTime);
			float totalValue = timeDifference.Hours * changeDivision;
			changeDivision = changeDivision / 60;
			totalValue += timeDifference.Minutes * changeDivision;
			changeDivision = changeDivision / 60;
			totalValue += timeDifference.Seconds * changeDivision;
			nextValue = totalValue;
			nextRotation = transform.rotation * Quaternion.AngleAxis(nextValue, Vector3.up);
		}

		protected override void SetLastValue()
		{
			lastValue = transform.rotation.eulerAngles.x;
			lastRotation = transform.rotation;
		}
	}
}