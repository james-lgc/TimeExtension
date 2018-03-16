using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ITimeListener
{
	Func<bool> TimeProcessingFunction { get; set; }
	void UpdateTime(TimeSpan sentLastUpdate, TimeSpan sentCurrentTime, TimeSpan sentNextUpdate, float sentTimeSpeed);
}
