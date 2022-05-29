﻿using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have one int argument.
/// Example: An Achievement unlock event, where the int is the Achievement ID.
/// </summary>

[CreateAssetMenu(menuName = "Events/String Event Channel")]
public class StringEventChannelSO : DescriptionBaseSO
{
	public UnityAction<string> OnEventRaised;
	
	public void RaiseEvent(string value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}
