using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ChunkRenderer : MonoBehaviour
{
	MeshCollider meshCollider;
	Mesh mesh;
	MeshFilter meshFilter;

	public ChunkData ChunkData { get; private set; }

	public bool IsModifiedByPlayer
	{
		get
		{
			return ChunkData.isModifiedByPlayer;
		}
		set
		{
			ChunkData.isModifiedByPlayer = value;
		}
	}

	private void Awake()
	{
		meshCollider = GetComponent<MeshCollider>();
		meshFilter = GetComponent<MeshFilter>();
		mesh = meshFilter.mesh;
	}

	public void InitChunk(ChunkData chunkData)
	{
		this.ChunkData = chunkData;
	}

	private void RenderMesh(MeshData meshData)
	{
		mesh.Clear();

		mesh.subMeshCount = 2;
		mesh.vertices = meshData.vertices.Concat(meshData.waterMesh.vertices).ToArray();

		mesh.SetTriangles(meshData.triangles.ToArray(), 0);
		mesh.SetTriangles(meshData.waterMesh.triangles.Select(val => val + meshData.vertices.Count).ToArray(), 1);

		mesh.uv = meshData.uv.Concat(meshData.waterMesh.uv).ToArray();
		mesh.RecalculateNormals();

		meshCollider.sharedMesh = null;
		Mesh collisionMesh = new Mesh();
		collisionMesh.vertices = meshData.colliderVertices.ToArray();
		collisionMesh.triangles = meshData.colliderTriangles.ToArray();
		collisionMesh.RecalculateNormals();

		meshCollider.sharedMesh = collisionMesh;

	}

	public void UpdateChunk()
	{
		RenderMesh(Chunk.GetChunkMeshData(ChunkData));
	}

	public void UpdateChunk(MeshData data)
	{
		RenderMesh(data);
	}
}
