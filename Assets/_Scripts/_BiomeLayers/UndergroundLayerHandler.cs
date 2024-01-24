using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndergroundLayerHandler : BlockLayerHandler
{
    public BlockType undergroundBlockType;
	public BlockType caveBlockType = BlockType.Air; // Define a block type for cave

	protected override bool TryHandling(ChunkData chunkData, int x, int y, int z, int surfaceHeightNoise, Vector2Int mapSeedOffset)
	{
		Vector3Int pos = new Vector3Int(x, y - chunkData.worldPosition.y, z);

		if (y < surfaceHeightNoise - 5)
		{
			// Consider caching IsCavePoint results if they are used multiple times for the same position
			bool isCave = IsCavePoint(pos + chunkData.worldPosition, mapSeedOffset);

			Chunk.SetBlock(chunkData, pos, isCave ? caveBlockType : DetermineUndergroundBlockType(y, surfaceHeightNoise));
			return true;
		}
		else if (y < surfaceHeightNoise)
		{
			Chunk.SetBlock(chunkData, pos, DetermineUndergroundBlockType(y, surfaceHeightNoise));
			return true;
		}
		return false;
	}


	private BlockType DetermineUndergroundBlockType(int y, int surfaceHeightNoise)
	{
		// Example: Choose different block types based on depth
		if (y < surfaceHeightNoise - 5)
		{
			return BlockType.Stone; // Example block type for deeper underground
		}
		else
		{
			return undergroundBlockType;
		}
	}

	private bool IsCavePoint(Vector3Int worldPosition, Vector2Int mapSeedOffset)
	{
		// Adjust these parameters as needed
		float caveFrequency = 0.05f;
		float caveAmplitude = 1.0f;
		float cavePersistence = 0.5f;
		int caveOctaves = 4;
		int caveSeed = mapSeedOffset.x + mapSeedOffset.y; // Example seed calculation

		float noiseValue = MyNoise.Noise3D(worldPosition.x, worldPosition.y, worldPosition.z, caveFrequency, caveAmplitude, cavePersistence, caveOctaves, caveSeed);

		// Define your threshold for what is considered a cave
		float caveThreshold = 0.2f; // Example threshold, adjust as needed
		return noiseValue < caveThreshold;
	}
}
