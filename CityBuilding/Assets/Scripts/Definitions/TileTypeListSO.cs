using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/TileTypeList")]
public class TileTypeListSO : ScriptableObject
{
	public List<TileSO> list;
}
