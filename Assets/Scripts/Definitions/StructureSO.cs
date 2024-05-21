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
    public string ghostName;
    public GameObject prefab;
    public bool canPlaceLake;
    public bool canPlaceForest;
    public int waterCost;
    public int ironCost;
    public int energyCost;
    public int moneyCost;
    public StructureType type;
    public Sprite spriteImage;
    public List<GeneratedMaterial> generatedMaterials;
    public int materialGenerationTime;
}
public enum StructureType
{
    building,
}

[System.Serializable]
public struct GeneratedMaterial
{
    public ResourceType generatedType;
    public int generatedAmount;
}