using System.Collections.Generic;
using UnityEngine;

public struct WorldGenerationData
{
	public List<Vector3Int> chunkPositionsToCreate;
	public List<Vector3Int> chunkDataPositionsToCreate;
	public List<Vector3Int> chunkPositionsToRemove;
	public List<Vector3Int> chunkDataToRemove;
}