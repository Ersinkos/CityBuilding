using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/StructureTypeList")]
public class StructureTypeListSO : ScriptableObject
{
    public List<StructureSO> list;
}
