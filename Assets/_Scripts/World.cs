using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
	public int mapSizeInChunks = 6;
	public int chunkSize = 16, chunkHeight = 100;
	public GameObject chunkPrefab;

	public TerrainGenerator terrainGenerator;
	public Vector2Int mapSeedOffset;

	Dictionary<Vector3Int, ChunkData> chunkDataDictionary = new Dictionary<Vector3Int, ChunkData>();
	Dictionary<Vector3Int, ChunkRenderer> chunkDictionary = new Dictionary<Vector3Int, ChunkRenderer>();

	private void GenerateVoxels(ChunkData data)
	{
		
	}

	public void GenerateWorld()
	{
		chunkDataDictionary.Clear();
		foreach (ChunkRenderer chunk in chunkDictionary.Values)
		{
			Destroy(chunk.gameObject);
		}
		chunkDictionary.Clear();

		for (int x = 0; x < mapSizeInChunks; x++)
		{
			for (int z = 0; z < mapSizeInChunks; z++)
			{

				ChunkData data = new ChunkData(chunkSize, chunkHeight, this, new Vector3Int(x * chunkSize, 0, z * chunkSize));
				//GenerateVoxels(data);
				ChunkData newData = terrainGenerator.GenerateChunkData(data, mapSeedOffset);
				chunkDataDictionary.Add(newData.chunkWorldPos, newData);
			}
		}

		foreach (ChunkData data in chunkDataDictionary.Values)
		{
			MeshData meshData = Chunk.GetChunkMeshData(data);
			GameObject chunkObject = Instantiate(chunkPrefab, data.chunkWorldPos, Quaternion.identity);
			ChunkRenderer chunkRenderer = chunkObject.GetComponent<ChunkRenderer>();
			chunkDictionary.Add(data.chunkWorldPos, chunkRenderer);
			chunkRenderer.InitChunk(data);
			chunkRenderer.UpdateChunk(meshData);

		}
	}

	internal BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, int x, int y, int z)
	{
		Vector3Int pos = Chunk.ChunkPosFromBlockCoords(this, x, y, z);
		ChunkData containerChunk = null;

		chunkDataDictionary.TryGetValue(pos, out containerChunk);

		if (containerChunk == null)
			return BlockType.Nothing;
		Vector3Int blockInCHunkCoordinates = Chunk.GetBlockInChunkCoordinates(containerChunk, new Vector3Int(x, y, z));
		return Chunk.GetBlockFromChunkCoordinates(containerChunk, blockInCHunkCoordinates);
	}
}
