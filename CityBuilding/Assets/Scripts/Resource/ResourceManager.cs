using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
	public static ResourceManager instance;
	[SerializeField] private int water;
	[SerializeField] private int iron;
	[SerializeField] private int energy;
	[SerializeField] private int money;
	private void Awake()
	{
		instance = this;
	}
	public int GetWater()
	{
		return water;
	}
	public int GetIron()
	{
		return iron;
	}
	public int GetEnergy()
	{
		return energy;
	}
	public int GetMoney()
	{
		return money;
	}
	public bool HasEnoughResourceToBuild(StructureSO structureType)
	{
		//TO-DO : add functional code
		return true;
	}
}
