using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseVisualization : MonoBehaviour
{
	public NoiseSettings noiseSettings;
	public int visualizationWidth = 256;
	public int visualizationHeight = 256;

	private GameObject visualizationPlane;

	void Start()
	{
		if (noiseSettings != null)
		{
			noiseSettings.OnSettingsUpdated += VisualizeAndUpdate;
		}
		VisualizeAndUpdate();
	}

	void Update()
	{
		// Check if noiseSettings has changed
		if (noiseSettings == null || !Application.isPlaying)
		{
			return;
		}

		// Check if any noise settings have changed
		if (Input.GetKeyDown(KeyCode.Space)) // You can change this condition to any input or event that suits your needs
		{
			ClearVisualization();
			VisualizeAndUpdate();
		}
	}

	void VisualizeAndUpdate()
	{
		if (visualizationPlane != null)
		{
			Destroy(visualizationPlane);
		}

		visualizationPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		visualizationPlane.transform.SetParent(transform);

		MyNoise.VisualizeNoise(noiseSettings, visualizationWidth, visualizationHeight, visualizationPlane);
	}

	void ClearVisualization()
	{
		if (visualizationPlane != null)
		{
			Destroy(visualizationPlane);
		}
	}

	void OnDestroy()
	{
		if (noiseSettings != null)
		{
			noiseSettings.OnSettingsUpdated -= VisualizeAndUpdate;
		}
	}
}

