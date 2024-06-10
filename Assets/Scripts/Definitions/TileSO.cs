using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
	Lake,
	Forest,
	Oil,
	Gold,
	Normal
}
[CreateAssetMenu(menuName = "ScriptableObjects/TileType")]
public class TileSO : ScriptableObject
{
	public TileType tileType;
}
