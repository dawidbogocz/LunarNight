using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class ChunkData
{
	public BlockType[] blocks;
	public int chunkSize = 16;
	public int chunkHeight = 100;
	public World worldReference;
	public Vector3Int chunkWorldPos;

	public bool isModifiedByPlayer = false;

	public ChunkData(int  chunkSize, int chunkHeight, World world, Vector3Int chunkWorldPos)
	{
		this.chunkHeight = chunkHeight;
		this.chunkSize = chunkSize;
		this.worldReference = world;
		this.chunkWorldPos = chunkWorldPos;
		blocks = new BlockType[chunkSize * chunkHeight * chunkSize];
	}
}
