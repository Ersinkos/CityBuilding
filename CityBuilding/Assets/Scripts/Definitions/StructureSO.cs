using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Structure")]
public class StructureSO : ScriptableObject
{
	public int id;
	public string nameString;
	public string buttonName;
	public string description;
	public GameObject prefab;
	public int waterCost;
	public int ironCost;
	public int energyCost;
	public int moneyCost;
	public StructureType type;
	public Sprite spriteImage;
}
public enum StructureType
{
	building,
}