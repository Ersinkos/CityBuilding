using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Structure")]
public class StructureSO : ScriptableObject
{
	public string nameString;
	public GameObject prefab;
	public int waterCost;
	public int ironCost;
	public int energyCost;
	public int moneyCost;
}
