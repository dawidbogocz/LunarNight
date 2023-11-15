using System;
using UnityEngine;

public static class Chunk
{
	public static void LoopThroughBlocks(ChunkData chunkData, Action<int, int, int> actionToPerform)
	{
		for(int i = 0; i < chunkData.blocks.Length; i++)
		{
			var pos = GetPosFromIndex(chunkData, i);
			actionToPerform(pos.x, pos.y, pos.z);
		}
	}

	private static Vector3Int GetPosFromIndex(ChunkData chunkData, int i)
	{
		int x = i % chunkData.chunkSize;
		int y = (i / chunkData.chunkSize) % chunkData.chunkHeight;
		int z = i / (chunkData.chunkSize * chunkData.chunkHeight);

		return new Vector3Int(x, y, z);
	}

	private static bool InRange(ChunkData chunkData, int coord)
	{
		if (coord < 0 || coord >= chunkData.chunkSize)
			return false;

		return true;
	}

	private static bool InRangeHeight(ChunkData chunkData, int ycoord)
	{
		if (ycoord < 0 || ycoord >= chunkData.chunkHeight)
			return false;

		return true;
	}

	public static void SetBlock(ChunkData chunkData, Vector3Int localPos, BlockType block)
	{
		if (InRange(chunkData, localPos.x) && InRangeHeight(chunkData, localPos.y) && InRange(chunkData, localPos.z))
		{
			int index = GetIndexFromPos(chunkData, localPos.x, localPos.y, localPos.z);
			chunkData.blocks[index] = block;
		}
		else
		{
			throw new Exception("Inappropiate chunk");
		}
	}

	private static int GetIndexFromPos(ChunkData chunkData, int x, int y, int z)
	{
		return x + chunkData.chunkSize * y + chunkData.chunkSize * chunkData.chunkHeight * z;
	}
	public static Vector3Int GetBlockInChunkCoordinates(ChunkData chunkData, Vector3Int pos)
	{
		return new Vector3Int
		{
			x = pos.x - chunkData.chunkWorldPos.x,
			y = pos.y - chunkData.chunkWorldPos.y,
			z = pos.z - chunkData.chunkWorldPos.z
		};
	}

	public static BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, Vector3Int chunkCoordinates)
	{
		return GetBlockFromChunkCoordinates(chunkData, chunkCoordinates.x, chunkCoordinates.y, chunkCoordinates.z);
	}

	public static BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, int x, int y, int z)
	{
		if (InRange(chunkData, x) && InRangeHeight(chunkData, y) && InRange(chunkData, z))
		{
			int index = GetIndexFromPos(chunkData, x, y, z);
			return chunkData.blocks[index];
		}

		return chunkData.worldReference.GetBlockFromChunkCoordinates(chunkData, chunkData.chunkWorldPos.x + x, chunkData.chunkWorldPos.y + y, chunkData.chunkWorldPos.z + z);
	}

	public static MeshData GetChunkMeshData(ChunkData chunkData)
	{
		MeshData meshData = new MeshData(true);

		LoopThroughBlocks(chunkData, (x, y, z) => meshData = BlockHelper.GetMeshData(chunkData, x, y, z, meshData, chunkData.blocks[GetIndexFromPos(chunkData, x, y, z)]));

		return meshData;
	}

	internal static Vector3Int ChunkPosFromBlockCoords(World world, int x, int y, int z)
	{
		Vector3Int pos = new Vector3Int
		{
			x = Mathf.FloorToInt(x / (float)world.chunkSize) * world.chunkSize,
			y = Mathf.FloorToInt(y / (float)world.chunkHeight) * world.chunkHeight,
			z = Mathf.FloorToInt(z / (float)world.chunkSize) * world.chunkSize
		};
		return pos;
	}
}