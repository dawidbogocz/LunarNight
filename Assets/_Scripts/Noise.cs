using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise 
{
	public static float OctavePerlin(float x, float z, NoiseSettings settings)
	{
		x *= settings.noiseScope;
		z *= settings.noiseScope;
		x += settings.noiseScope;
		z += settings.noiseScope;

		float total = 0;
		float frequency = 1;
		float amplitude = 1;
		float amplitudeSum = 0;  
		for (int i = 0; i < settings.octaves; i++)
		{
			total += Mathf.PerlinNoise((settings.offest.x + settings.worldOffset.x + x) * frequency, (settings.offest.y + settings.worldOffset.y + z) * frequency) * amplitude;

			amplitudeSum += amplitude;

			amplitude *= settings.persistance;
			frequency *= 2;
		}

		return total / amplitudeSum;
	}

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

}
