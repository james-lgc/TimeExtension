using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSA.Extensions.Base;

namespace DSA.Extensions.GameTime
{
	public class Sun : ExtendedMonoBehaviour
	{
		public override ExtensionEnum Extension { get { return ExtensionEnum.Time; } }

		private float timeSpeed;
		[SerializeField] private float degreesPerRealMinute;
		[SerializeField] private float degreesPerGameMinute;

		private System.Func<bool> TimeProcessingFunction;

		public void SetTimeProcessingFunction(System.Func<bool> sentFunc)
		{
			TimeProcessingFunction = sentFunc;
		}

		public void Set(float sentTimeSpeed)
		{
			timeSpeed = sentTimeSpeed;
			degreesPerRealMinute = 360F / 1440F;
			degreesPerGameMinute = degreesPerRealMinute * timeSpeed;
		}

		public void FixedUpdate()
		{
			if (!GetIsExtensionLoaded()) return;
			//if (TimeGlobals.IsProcessing == false) return;
			if (TimeProcessingFunction == null) { return; }
			if (!TimeProcessingFunction()) { return; }
			transform.Rotate(Vector3.up * degreesPerGameMinute / 60 * Time.deltaTime);
		}

		public void SetPosition(TransformValue sentTrans)
		{
			transform.position = sentTrans.Position;
			transform.rotation = sentTrans.Rotation;
		}
	}
}