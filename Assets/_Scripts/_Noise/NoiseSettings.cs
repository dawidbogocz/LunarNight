using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "noiseSettings", menuName = "Data/NoiseSettings")]
public class NoiseSettings : ScriptableObject
{
	[Header("Basic Settings")]
	public float noiseZoom = 1.0f;
	public int octaves = 4;
	public Vector2Int offset;

	[Header("Advanced Settings")]
	public Vector2Int worldOffset;
	public float persistance = 0.5f;
	public float redistributionModifier = 1.0f;
	public float exponent = 1.0f;

	[Header("Visualization Settings")]
	public bool visualizeInEditor = true;
	public float visualizationScale = 1.0f;

	public event Action OnSettingsUpdated;

	public void NotifySettingsUpdated()
	{
		OnSettingsUpdated?.Invoke();
	}

	private void OnValidate()
	{
		NotifySettingsUpdated();
	}
}
