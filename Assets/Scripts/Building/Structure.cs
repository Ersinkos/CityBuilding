using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    [SerializeField] private StructureSO structureData;
    private void Start()
    {
        if (structureData.generatedMaterials.Count > 0)
        {
            StartCoroutine(StartGenerateMaterial());
        }
    }
    private IEnumerator StartGenerateMaterial()
    {
        while (true)
        {
            foreach (GeneratedMaterial material in structureData.generatedMaterials)
            {
                if (ResourceManager.instance.HasEnoughStorageCapacity(material.generatedType))
                    ResourceManager.instance.AddResource(material.generatedType, material.generatedAmount);
            }
            yield return new WaitForSeconds(structureData.materialGenerationTime);
        }
    }
}
