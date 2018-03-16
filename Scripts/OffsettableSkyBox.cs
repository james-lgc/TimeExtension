using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DSA.Extensions.GameTime
{
	public class OffsettableSkyBox : DayCycleObject
	{
		private Renderer thisRenderer;

		private void Awake()
		{
			thisRenderer = GetComponent<Renderer>();
		}

		public void Update()
		{
			if (!GetIsExtensionLoaded()) return;
			if (TimeProcessingFunction == null) { return; }
			if (!TimeProcessingFunction()) { return; }
			timeElapsed += Time.deltaTime;
			currentValue = Mathf.Lerp(lastValue, nextValue, timeElapsed / realTimeIncrement);
			thisRenderer.material.SetTextureOffset("_MainTex", new Vector2(currentValue, 0));
		}

		protected override void SetNextValue()
		{
			float changeDivision = (midnightValue - noonValue) / 12;
			float totalValue = changeDivision * nextTime.Hours;
			changeDivision = changeDivision / 60;
			totalValue += changeDivision * nextTime.Minutes;
			changeDivision = changeDivision / 60;
			totalValue += changeDivision * nextTime.Seconds;
			if (currentTime.Hours > 0 && currentTime.Hours < 12)
			{
				nextValue = noonValue + (midnightValue - totalValue);
			}
			else
			{
				nextValue = noonValue + Mathf.Abs(totalValue - midnightValue);
			}
		}

		protected override void SetLastValue()
		{
			lastValue = thisRenderer.material.GetTextureOffset("_MainTex").x;
		}
	}
}