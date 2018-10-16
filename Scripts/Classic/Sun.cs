using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSA.Extensions.Base;
using System;

namespace DSA.Extensions.GameTime
{
	public class Sun : DayCycleObject
	{
		[SerializeField] protected float xOffset;
		[SerializeField] protected float yOffset;
		[SerializeField] protected float zOffset;
		protected Quaternion lastRotation;

		public void Update()
		{
			if (!GetIsExtensionLoaded()) return;
			if (TimeProcessingFunction == null) { return; }
			if (!TimeProcessingFunction()) { return; }
			timeElapsed += Time.deltaTime;
			Quaternion nextRotation = Quaternion.Euler(nextValue - xOffset, 0, 0);
			transform.rotation = Quaternion.Slerp(lastRotation, nextRotation, timeElapsed / realTimeIncrement);
		}

		public void SetPosition(TransformValue sentTrans)
		{
			transform.position = sentTrans.Position;
			transform.rotation = sentTrans.Rotation;
		}

		protected override void SetNextValue()
		{
			float changeDivision = (midnightValue - noonValue) / 12;
			float totalValue = changeDivision * nextTime.Hours;
			changeDivision = changeDivision / 60;
			totalValue += changeDivision * nextTime.Minutes;
			changeDivision = changeDivision / 60;
			totalValue += changeDivision * nextTime.Seconds;
			nextValue = totalValue;
		}

		protected override void SetLastValue()
		{
			lastValue = transform.rotation.eulerAngles.x;
			lastRotation = transform.rotation;
		}
	}
}