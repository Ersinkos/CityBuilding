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
	public ResourceTypeListSO resourceList;
	private void Awake()
	{
		instance = this;
	}
	private void Start()
	{
		water = GetResourceType(ResourceType.Water).startingAmount;
		iron = GetResourceType(ResourceType.Iron).startingAmount;
		energy = GetResourceType(ResourceType.Energy).startingAmount;
		money = GetResourceType(ResourceType.Money).startingAmount;
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
		int ironCost = structureType.ironCost;
		int waterCost = structureType.waterCost;
		int energyCost = structureType.energyCost;
		int moneyCost = structureType.moneyCost;

		if (iron < ironCost)
			return false;
		if (money < moneyCost)
			return false;
		if (energy < energyCost)
			return false;
		if (water < waterCost)
			return false;
		return true;
	}
	public ResourceSO GetResourceType(ResourceType resourceEnumType)
	{
		foreach (ResourceSO resourceType in resourceList.list)
		{
			if (resourceType.resourceType == resourceEnumType)
			{
				return resourceType;
			}
		}
		return resourceList.list[0];
	}
}
