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
    [SerializeField] private int storageCapacity;
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
        storageCapacity = GetResourceType(ResourceType.StorageCapacity).startingAmount;

        UpdateResourceUI();
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
    public int GetStorageCapacity()
    {
        return storageCapacity;
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

    public void OnStructurePlacing(StructureSO structureType)
    {
        SpendMaterial(structureType);
    }
    private void SpendMaterial(StructureSO structureType)
    {
        water -= structureType.waterCost;
        iron -= structureType.ironCost;
        energy -= structureType.energyCost;
        money -= structureType.moneyCost;

        UpdateResourceUI();
    }
    public void AddResource(ResourceType type, int amount)
    {
        switch (type)
        {
            case ResourceType.Water:
                water += amount;
                break;
            case ResourceType.Iron:
                iron += amount;
                break;
            case ResourceType.Energy:
                energy += amount;
                break;
            case ResourceType.Money:
                money += amount;
                break;
            case ResourceType.StorageCapacity:
                storageCapacity += amount;
                break;
        }
        UpdateResourceUI();
    }
    public bool HasEnoughStorageCapacity(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.Water:
                return storageCapacity > water;
            case ResourceType.Iron:
                return storageCapacity > iron;
            case ResourceType.Energy:
                return storageCapacity > energy;
            case ResourceType.Money:
                return storageCapacity > money;
            default:
                return true;

        }
    }
    private void UpdateResourceUI()
    {
        GameCanvas.instance.ChangeWaterMaterialText(water.ToString());
        GameCanvas.instance.ChangeIronMaterialText(iron.ToString());
        GameCanvas.instance.ChangeEnergyMaterialText(energy.ToString());
        GameCanvas.instance.ChangeMoneyMaterialText(money.ToString());
        GameCanvas.instance.ChangeStorageCapacityText(storageCapacity.ToString());

        if (FlexibleUI.instance != null)
            FlexibleUI.instance.UpdateResourceRequirementIndicators();
    }
}
