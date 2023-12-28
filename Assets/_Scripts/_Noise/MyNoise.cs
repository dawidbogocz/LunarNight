using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyNoise
{
	public static float RemapValue(float value, float initialMin, float initialMax, float outputMin, float outputMax)
	{
		return outputMin + (value - initialMin) * (outputMax - outputMin) / (initialMax - initialMin);
	}

	public static float RemapValue01(float value, float outputMin, float outputMax)
	{
		return outputMin + (value - 0) * (outputMax - outputMin) / (1 - 0);
	}

	public static int RemapValue01ToInt(float value, float outputMin, float outputMax)
	{
		return (int)RemapValue01(value, outputMin, outputMax);
	}

	public static float Redistribution(float noise, NoiseSettings settings)
	{
		return Mathf.Pow(noise * settings.redistributionModifier, settings.exponent);
	}

	public static float OctavePerlin(float x, float z, NoiseSettings settings)
	{
		x *= settings.noiseZoom;
		z *= settings.noiseZoom;
		x += settings.noiseZoom;
		z += settings.noiseZoom;

		float total = 0;
		float frequency = 1;
		float amplitude = 1;
		float amplitudeSum = 0;  // Used for normalizing result to 0.0 - 1.0 range
		for (int i = 0; i < settings.octaves; i++)
		{
			total += Mathf.PerlinNoise((settings.offset.x + settings.worldOffset.x + x) * frequency, (settings.offset.y + settings.worldOffset.y + z) * frequency) * amplitude;

			amplitudeSum += amplitude;

			amplitude *= settings.persistance;
			frequency *= 2;
		}

		return total / amplitudeSum;
	}
	public static void VisualizeNoise(NoiseSettings settings, int width, int height, GameObject visualizationPlane = null)
	{
		Texture2D texture = new Texture2D(width, height);
		Color[] colors = new Color[width * height];

		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				float value = OctavePerlin(x, y, settings);
				value = RemapValue(value, 0, 1, 0, 1); // Ensure the value is in the 0-1 range

				Color color = new Color(value, value, value);
				colors[y * width + x] = color;
			}
		}

		texture.SetPixels(colors);
		texture.Apply();

		if (visualizationPlane == null)
		{
			visualizationPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		}

		visualizationPlane.transform.localScale = new Vector3(width * settings.visualizationScale, 1, height * settings.visualizationScale);
		visualizationPlane.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Texture");
		visualizationPlane.GetComponent<Renderer>().material.color = Color.white;
		visualizationPlane.GetComponent<Renderer>().material.mainTexture = texture;
		visualizationPlane.transform.position = new Vector3(width * settings.visualizationScale * 0.5f, 0, height * settings.visualizationScale * 0.5f);
	}
}
