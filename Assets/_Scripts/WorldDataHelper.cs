using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class WorldDataHelper
{
    public static Vector3Int ChunkPositionFromBlockCoords(World world, Vector3Int worldBlockPosition)
    {
        return new Vector3Int
        {
            x = Mathf.FloorToInt(worldBlockPosition.x / (float)world.chunkSize) * world.chunkSize,
            y = Mathf.FloorToInt(worldBlockPosition.y / (float)world.chunkHeight) * world.chunkHeight,
            z = Mathf.FloorToInt(worldBlockPosition.z / (float)world.chunkSize) * world.chunkSize
        };
    }

	

	internal static void RemoveChunkData(World world, Vector3Int pos)
    {
        world.worldData.chunkDataDictionary.Remove(pos);
    }

    internal static void RemoveChunk(World world, Vector3Int pos)
    {
        ChunkRenderer chunk = null;
        if (world.worldData.chunkDictionary.TryGetValue(pos, out chunk))
        {
            world.worldRenderer.RemoveChunk(chunk);
            world.worldData.chunkDictionary.Remove(pos);
        }
    }
    internal static List<Vector3Int> GetChunkPositionsAroundPlayer(World world, Vector3Int playerPosition)
    {
		return GetPositionsAroundPlayer(world, playerPosition, world.chunkDrawingRange);
	}
    internal static List<Vector3Int> GetDataPositionsAroundPlayer(World world, Vector3Int playerPosition)
    {
		return GetPositionsAroundPlayer(world, playerPosition, world.chunkDrawingRange + 1);
	}

	private static List<Vector3Int> GetPositionsAroundPlayer(World world, Vector3Int playerPosition, int drawingRange)
	{
		int startX = playerPosition.x - drawingRange * world.chunkSize;
		int startZ = playerPosition.z - drawingRange * world.chunkSize;
		int endX = playerPosition.x + drawingRange * world.chunkSize;
		int endZ = playerPosition.z + drawingRange * world.chunkSize;

		List<Vector3Int> positions = new List<Vector3Int>();

		for (int x = startX; x <= endX; x += world.chunkSize)
		{
			for (int z = startZ; z <= endZ; z += world.chunkSize)
			{
				Vector3Int chunkPos = ChunkPositionFromBlockCoords(world, new Vector3Int(x, 0, z));
				positions.Add(chunkPos);
			}
		}
		return positions;	
	}

	internal static ChunkRenderer GetChunk(World worldReference, Vector3Int worldPosition)
    {
        if (worldReference.worldData.chunkDictionary.ContainsKey(worldPosition))
            return worldReference.worldData.chunkDictionary[worldPosition];
        return null;
    }

    internal static void SetBlock(World worldReference, Vector3Int worldBlockPosition, BlockType blockType)
    {
        ChunkData chunkData = GetChunkData(worldReference, worldBlockPosition);
        if (chunkData != null)
        {
            Vector3Int localPosition = Chunk.GetBlockInChunkCoordinates(chunkData, worldBlockPosition);
            Chunk.SetBlock(chunkData, localPosition, blockType);
        }
    }

    public static ChunkData GetChunkData(World worldReference, Vector3Int worldBlockPosition)
    {
        Vector3Int chunkPosition = ChunkPositionFromBlockCoords(worldReference, worldBlockPosition);

        ChunkData containerChunk = null;

        worldReference.worldData.chunkDataDictionary.TryGetValue(chunkPosition, out containerChunk);

        return containerChunk;
    }

    internal static List<Vector3Int> GetUnnededData(WorldData worldData, List<Vector3Int> allChunkDataPositionsNeeded)
    {
        return worldData.chunkDataDictionary.Keys
    .Where(pos => allChunkDataPositionsNeeded.Contains(pos) == false && worldData.chunkDataDictionary[pos].modifiedByThePlayer == false)
    .ToList();

    }

    internal static List<Vector3Int> GetUnnededChunks(WorldData worldData, List<Vector3Int> allChunkPositionsNeeded)
    {
        List<Vector3Int> positionToRemove = new List<Vector3Int>();
        foreach (var pos in worldData.chunkDictionary.Keys
            .Where(pos => allChunkPositionsNeeded.Contains(pos) == false))
        {
            if (worldData.chunkDictionary.ContainsKey(pos))
            {
                positionToRemove.Add(pos);

            }
        }

        return positionToRemove;
    }

    internal static List<Vector3Int> SelectPositonsToCreate(WorldData worldData, List<Vector3Int> allChunkPositionsNeeded, Vector3Int playerPosition)
    {
        return allChunkPositionsNeeded
            .Where(pos => worldData.chunkDictionary.ContainsKey(pos) == false)
            .OrderBy(pos => Vector3.Distance(playerPosition, pos))
            .ToList();
    }

    internal static List<Vector3Int> SelectDataPositonsToCreate(WorldData worldData, List<Vector3Int> allChunkDataPositionsNeeded, Vector3Int playerPosition)
    {
        return allChunkDataPositionsNeeded
            .Where(pos => worldData.chunkDataDictionary.ContainsKey(pos) == false)
            .OrderBy(pos => Vector3.Distance(playerPosition, pos))
            .ToList();
    }
}
