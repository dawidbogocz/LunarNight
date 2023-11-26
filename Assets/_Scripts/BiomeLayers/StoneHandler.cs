using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StoneHandler : BiomeLayerHandler
{
	[Range(0, 1)]
	public float stoneThreshold = 0.5f;

	[SerializeField]
	private NoiseSettings stoneNoiseSettings;

	protected override bool TryHandling(ChunkData chunkData, int x, int y, int z, int surfaceHeightNoise, Vector2Int mapSeedOffset)
	{
		if (chunkData.chunkWorldPos.y > surfaceHeightNoise)
			return false;

		stoneNoiseSettings.worldOffset = mapSeedOffset;
		float stoneNoise = Noise.OctavePerlin(chunkData.chunkWorldPos.x + x, chunkData.chunkWorldPos.z + z, stoneNoiseSettings);

		int endPosition = surfaceHeightNoise;
		if (chunkData.chunkWorldPos.y < 0)
		{
			endPosition = chunkData.chunkWorldPos.y + chunkData.chunkHeight;
		}

		if (stoneNoise > stoneThreshold)
		{
			for (int i = chunkData.chunkWorldPos.y; i <= endPosition; i++)
			{
				Vector3Int pos = new Vector3Int(x, i, z);
				Chunk.SetBlock(chunkData, pos, BlockType.Stone);
			}
			return true;
		}
		return false;
	}
}