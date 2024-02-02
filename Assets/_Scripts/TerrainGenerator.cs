using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
	public BiomeGenerator biomeGenerator;

	[SerializeField]
	private NoiseSettings biomeNoiseTempSettings;

	[SerializeField]
	private NoiseSettings biomeNoiseHumiSettings;

	public DomainWarping biomeDomainWarping;

	[SerializeField]
	private List<BiomeData> biomeGeneratorsData = new List<BiomeData>();


	public ChunkData GenerateChunkData(ChunkData data, Vector2Int mapSeedOffset)
	{
		biomeNoiseTempSettings.worldOffset = mapSeedOffset;
		biomeNoiseHumiSettings.worldOffset = mapSeedOffset;
		BiomeGeneratorSelection biomeSelection = SelectBiomeGenerator(data.worldPosition, data, false);
		//TreeData treeData = biomeGenerator.GetTreeData(data, mapSeedOffset);
		data.treeData = biomeSelection.biomeGenerator.GetTreeData(data, mapSeedOffset);
		for (int x = 0; x < data.chunkSize; x++)
		{
			for (int z = 0; z < data.chunkSize; z++)
			{
				biomeSelection = SelectBiomeGenerator(new Vector3Int(data.worldPosition.x + x, 0, data.worldPosition.z + z), data);
				data = biomeSelection.biomeGenerator.ProcessChunkColumn(data, x, z, mapSeedOffset, biomeSelection.terrainSurfaceNoise);
			}
		}
		return data;
	}

	private BiomeGeneratorSelection SelectBiomeGenerator(Vector3Int worldPosition, ChunkData data, bool useDomainWarping = true)
	{
		if (useDomainWarping)
		{
			Vector2Int domainOffset = Vector2Int.RoundToInt(biomeDomainWarping.GenerateDomainOffset(worldPosition.x, worldPosition.z));
			worldPosition += new Vector3Int(domainOffset.x, 0, domainOffset.y);
		}

		// Get temperature and humidity noise at the position
		float tempNoise = MyNoise.OctavePerlin(worldPosition.x, worldPosition.z, biomeNoiseTempSettings);
		float humiNoise = MyNoise.OctavePerlin(worldPosition.x, worldPosition.z, biomeNoiseHumiSettings);

		// Find the main biome and its neighbors based on noise
		var mainBiome = SelectBiomeByNoise(tempNoise, humiNoise);
		var neighborBiomes = FindNeighborBiomes(worldPosition, tempNoise, humiNoise);

		// Blend terrain height across main and neighbor biomes
		float blendedHeight = BlendTerrainHeight(mainBiome, neighborBiomes, worldPosition, data);

		// Return the biome generator selection with blended height
		return new BiomeGeneratorSelection(mainBiome, Mathf.RoundToInt(blendedHeight));
	}

	private float BlendTerrainHeight(BiomeGenerator mainBiome, List<BiomeGenerator> neighbors, Vector3Int worldPosition, ChunkData data)
	{
		float mainHeight = mainBiome.GetSurfaceHeightNoise(worldPosition.x, worldPosition.z, data.chunkHeight);
		float blendedHeight = mainHeight;
		float totalWeight = 1f;

		foreach (var neighbor in neighbors)
		{
			// Calculate distance-based weight (can be adjusted or based on other factors)
			float weight = 10f; // Adjust this based on your specific requirements

			float neighborHeight = neighbor.GetSurfaceHeightNoise(worldPosition.x, worldPosition.z, data.chunkHeight);
			blendedHeight += neighborHeight * weight;
			totalWeight += weight;
		}

		return blendedHeight / totalWeight; // Return the average height based on weights
	}

	private List<BiomeGenerator> FindNeighborBiomes(Vector3Int worldPosition, float tempNoise, float humiNoise)
	{
		List<BiomeGenerator> neighbors = new List<BiomeGenerator>();

		// Define ranges for finding neighboring biomes. These ranges can be adjusted.
		float tempRange = 0.82f; // Example range for temperature
		float humiRange = 0.82f; // Example range for humidity

		// Check neighboring noise values within the defined range
		foreach (var data in biomeGeneratorsData)
		{
			if ((tempNoise + tempRange >= data.temperatureStartThreshold && tempNoise - tempRange < data.temperatureEndThreshold) &&
				(humiNoise + humiRange >= data.humidityStartThreshold && humiNoise - humiRange < data.humidityEndThreshold))
			{
				BiomeGenerator biomeGen = data.biomeTerrainGenerator;
				if (!neighbors.Contains(biomeGen))
				{
					neighbors.Add(biomeGen);
				}
			}
		}

		return neighbors;
	}

	private BiomeGenerator SelectBiomeByNoise(float temp, float humidity)
	{
		foreach (var data in biomeGeneratorsData)
		{
			if (temp >= data.temperatureStartThreshold && temp < data.temperatureEndThreshold
				&& humidity >= data.humidityStartThreshold && humidity < data.humidityEndThreshold)
			{
				return data.biomeTerrainGenerator;
			}
		}
		return biomeGeneratorsData[0].biomeTerrainGenerator;
	}

}

[Serializable]
public struct BiomeData
{
	[Range(0f, 1f)]
	public float temperatureStartThreshold, temperatureEndThreshold;
	[Range(0f, 1f)]
	public float humidityStartThreshold, humidityEndThreshold;
	public int minHeight, maxHeight;
	public BiomeGenerator biomeTerrainGenerator;
}

public class BiomeGeneratorSelection
{
	public BiomeGenerator biomeGenerator = null;
	public int? terrainSurfaceNoise = null;

	public BiomeGeneratorSelection(BiomeGenerator biomeGeneror, int? terrainSurfaceNoise = null)
	{
		this.biomeGenerator = biomeGeneror;
		this.terrainSurfaceNoise = terrainSurfaceNoise;
	}
}