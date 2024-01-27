using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoiseSettings", menuName = "Data/NoiseSettings")]
public class NoiseSettings : ScriptableObject
{
    [Header("Basic Settings")]
    public float noiseZoom = 1.0f;
	[Range(0, 10)]
	public int octaves = 4;
    [Tooltip("Global offset applied to noise generation")]
    public Vector2Int worldOffset;
    [Range(0f, 1f)]
    public float persistence = 0.5f;
    [Range(1f, 3f)]
    public float lacunarity = 2.0f; // Typically, lacunarity should be > 1

	[Header("Advanced Settings")]
	[Tooltip("Used for modifying noise output")]
    public AnimationCurve frequencyModifierCurve = AnimationCurve.Linear(0, 0.5f, 1, 0.5f);

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
