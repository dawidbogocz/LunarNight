using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BiomeLayerHandler : MonoBehaviour
{
    [SerializeField]
    private BiomeLayerHandler next;

    public bool Handle(ChunkData chunkData, int x, int y, int z, int surfaceHeightNoise, Vector2Int mapSeedOffset)
    {
        if(TryHandling(chunkData, x, y, z, surfaceHeightNoise, mapSeedOffset))
        {
            return true;
        }
        if(next != null)
        {
            return next.Handle(chunkData, x,y,z, surfaceHeightNoise,mapSeedOffset);
        }
        return false;
    }

    protected abstract bool TryHandling(ChunkData chunkData, int x, int y, int z, int surfaceHeightNoise, Vector2Int mapSeedOffset);
}
