using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System;

public class RotationTest : MonoBehaviour
{

	public float3 midDayValue;
	public float3 midNightValue;
	public float noonToMidnightDifference;
	public float rotScale;

	private TimeSpan gameTime;
	private TimeSpan timeOfDay;
	private float minutesPerDay = 1;
	private float gameSpeed;
	float timeCounter;

	private void Awake()
	{
		gameTime = new TimeSpan();
		gameSpeed = 1440F / minutesPerDay;
		timeCounter = 0;
		rotScale = rotScale / gameSpeed;
	}

	public void Update()
	{
		timeCounter += Time.deltaTime;
		// Debug.Log("Time counter: " + timeCounter);
		gameTime = new TimeSpan(0, 0, Mathf.RoundToInt(timeCounter * gameSpeed));
		timeOfDay = new TimeSpan(gameTime.Hours, gameTime.Minutes, gameTime.Seconds);
		int seconds = (int)timeOfDay.TotalSeconds;

		float changeDivision = (noonToMidnightDifference) / 12;
		float totalValue = timeOfDay.Hours * changeDivision;
		changeDivision = changeDivision / 60;
		totalValue += timeOfDay.Minutes * changeDivision;
		changeDivision = changeDivision / 60;
		totalValue += timeOfDay.Seconds * changeDivision;
		// Quaternion.
		transform.rotation = Quaternion.Euler(midNightValue) * Quaternion.AngleAxis(rotScale * seconds, Vector3.up);
		// transform.rotation = Quaternion.Euler(midNightValue);
	}
}
