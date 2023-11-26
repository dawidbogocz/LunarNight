using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeGenerator : MonoBehaviour
{
	public int waterThreshold = 50;

	public NoiseSettings noiseSettings;

	public DomainWarping domainWarping;

	public bool useDomainWarping = true;

	public BiomeLayerHandler biomeLayerHandler;

	public List<BiomeLayerHandler> additionalLayerHandlers;

	public ChunkData ProcessChunk(ChunkData data, int x, int z, Vector2Int mapSeedOffset)
	{
		noiseSettings.worldOffset = mapSeedOffset;
		int groundPos = GetSurfaceHeightNoise(data.chunkWorldPos.x + x,data.chunkWorldPos.z + z, data.chunkHeight);

		for (int y = 0; y < data.chunkHeight; y++)
		{
			biomeLayerHandler.Handle(data, x, y, z, groundPos, mapSeedOffset);
		}

		foreach (var layer in additionalLayerHandlers)
		{
			layer.Handle(data, x, data.chunkWorldPos.y, z, groundPos, mapSeedOffset);
		}
		return data;
	}

	private int GetSurfaceHeightNoise(int x, int z, int chunkHeight)
	{
		float terrainHeight;

		if(useDomainWarping)
			terrainHeight = Noise.OctavePerlin(x, z, noiseSettings);
		else
			terrainHeight = domainWarping.GenerateDomainNoise(x, z, noiseSettings);

		terrainHeight = Noise.Redistribution(terrainHeight, noiseSettings);
		int surfaceHeight = Noise.RemapValue01ToInt(terrainHeight, 0, chunkHeight);

		return surfaceHeight;
	}
}
