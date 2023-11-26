using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshData
{
	public List<Vector3> vertices = new List<Vector3>();
	public List<int> triangles = new List<int>();
	public List<Vector2> uv = new List<Vector2>();

	public List<Vector3> colliderVertices = new List<Vector3>();
	public List<int> colliderTriangles = new List<int>();

	public MeshData waterMesh;
	private bool isMainMesh = true;

	public MeshData(bool isMainMesh)
	{
		if (isMainMesh)
		{
			waterMesh = new MeshData(false);
		}
	}

}
