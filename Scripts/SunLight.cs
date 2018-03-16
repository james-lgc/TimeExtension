using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSA.Extensions.GameTime
{
	public class SunLight : DayCycleObject
	{
		private float lightIntensity = 1F;
		private Light thisLight;

		private void Awake()
		{
			thisLight = GetComponent<Light>();
		}
		public void Update()
		{
			if (!GetIsExtensionLoaded()) return;
			if (TimeProcessingFunction == null) { return; }
			if (!TimeProcessingFunction()) { return; }
			timeElapsed += Time.deltaTime;
			currentValue = Mathf.Lerp(lastValue, nextValue, timeElapsed / realTimeIncrement);
			thisLight.intensity = currentValue;
		}

		protected override void SetNextValue()
		{
			float changeDivision = (noonValue - midnightValue) / 12;
			float totalValue = changeDivision * nextTime.Hours;
			changeDivision = changeDivision / 60;
			totalValue += changeDivision * nextTime.Minutes;
			changeDivision = changeDivision / 60;
			totalValue += changeDivision * nextTime.Seconds;
			if (nextTime.Hours >= 0 && nextTime.Hours <= 12)
			{
				nextValue = totalValue;
			}
			else
			{
				nextValue = noonValue - (totalValue - noonValue);
			}
		}

		protected override void SetLastValue()
		{
			lastValue = thisLight.intensity;
		}
	}
}