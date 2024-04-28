using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
	Water,
	Iron,
	Energy,
	Money,
}
[CreateAssetMenu(menuName = "ScriptableObjects/Resource")]
public class ResourceSO : ScriptableObject
{
	public ResourceType resourceType;
	public string nameString;
	public int startingAmount;
}
