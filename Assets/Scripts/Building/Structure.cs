using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    [SerializeField] private StructureSO structureData;
    public Tile tile;
    private Material defaultMaterial;
    private void Awake()
    {
        defaultMaterial = GetComponent<MeshRenderer>().material;
    }
    private void Start()
    {
        if (structureData.generatedMaterials.Count > 0)
        {
            StartCoroutine(StartGenerateMaterial());
        }
        if (structureData.id == 5) // if it's house
            ResourceManager.instance.AddResource(ResourceType.Population, 1);
    }
    private IEnumerator StartGenerateMaterial()
    {
        while (true)
        {
            foreach (GeneratedMaterial material in structureData.generatedMaterials)
            {
                if (structureData.nameString == "House")
                    Debug.Log("spend water");
                if (material.generatedAmount < 0 || ResourceManager.instance.HasEnoughStorageCapacity(material.generatedType))
                    ResourceManager.instance.AddResource(material.generatedType, material.generatedAmount);
            }
            yield return new WaitForSeconds(structureData.materialGenerationTime);
        }
    }
    private void OnMouseEnter()
    {
        if (!BuildManager.instance.buildMode && !BuildManager.instance.isHq(structureData))
        {
            GetComponent<MeshRenderer>().material = BuildManager.instance.GetOnMouseMaterial();
        }
    }
    private void OnMouseDown()
    {
        if (!BuildManager.instance.isHq(structureData))
            BuildManager.instance.DemolishStructure(this);
    }
    private void OnMouseExit()
    {
        if (GetComponent<MeshRenderer>().material != defaultMaterial)
        {
            GetComponent<MeshRenderer>().material = defaultMaterial;
        }
    }
}
