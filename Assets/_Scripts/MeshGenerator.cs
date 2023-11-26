using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator 
{
	public static void AddVertex(MeshData meshData, Vector3 vertex, bool vertexGeneratesCollider)
	{
		meshData.vertices.Add(vertex);
		if (vertexGeneratesCollider)
		{
			meshData.colliderVertices.Add(vertex);
		}
	}

	public static void AddQuadTriangles(MeshData meshData, bool quadGeneratesCollider)
	{
		meshData.triangles.Add(meshData.vertices.Count - 4);
		meshData.triangles.Add(meshData.vertices.Count - 3);
		meshData.triangles.Add(meshData.vertices.Count - 2);

		meshData.triangles.Add(meshData.vertices.Count - 4);
		meshData.triangles.Add(meshData.vertices.Count - 2);
		meshData.triangles.Add(meshData.vertices.Count - 1);

		if (quadGeneratesCollider)
		{
			meshData.colliderTriangles.Add(meshData.colliderVertices.Count - 4);
			meshData.colliderTriangles.Add(meshData.colliderVertices.Count - 3);
			meshData.colliderTriangles.Add(meshData.colliderVertices.Count - 2);
			meshData.colliderTriangles.Add(meshData.colliderVertices.Count - 4);
			meshData.colliderTriangles.Add(meshData.colliderVertices.Count - 2);
			meshData.colliderTriangles.Add(meshData.colliderVertices.Count - 1);
		}
	}
}
