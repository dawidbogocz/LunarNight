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

		
		if (y < surfaceHeightNoise)
		{
			if(y < surfaceHeightNoise - 10)
			{
				if (IsCavePoint(pos + chunkData.worldPosition, mapSeedOffset))
				{
					// Set block to Air if it's part of a cave
					Chunk.SetBlock(chunkData, pos, caveBlockType);
				}
				else
				{
					// Handle different underground block types here based on depth or other conditions
					Chunk.SetBlock(chunkData, pos, DetermineUndergroundBlockType(y, surfaceHeightNoise));
				}
			}
			else
			{
				// Handle different underground block types here based on depth or other conditions
				Chunk.SetBlock(chunkData, pos, DetermineUndergroundBlockType(y, surfaceHeightNoise));
			}

			return true;
		}
		return false;
	}

	private BlockType DetermineUndergroundBlockType(int y, int surfaceHeightNoise)
	{
		// Example: Choose different block types based on depth
		if (y < surfaceHeightNoise - 10)
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
