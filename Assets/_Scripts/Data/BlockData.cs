using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Block", menuName = "Data/Block")]
public class BlockData : ScriptableObject
{
	public float textureSizeX, textureSizeY;
	public List<TextureData> textureDataList;
}

[Serializable]
public class TextureData
{
	public BlockType blockType;
	public Vector2Int up, down, side;
	public bool isSolid = true;
	public bool generatesCollider = true;
}