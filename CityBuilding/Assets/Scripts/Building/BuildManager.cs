using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
	public static BuildManager instance { get; private set; }
	private StructureSO activeStructure;
	public bool buildMode;
	private void Awake()
	{
		instance = this;
	}
	public void BuildStructure(GameObject tile)
	{
		//TO-DO : Add building functionality
		Debug.Log("Build to : " + tile.name);
		Instantiate(activeStructure.prefab, tile.transform.position, Quaternion.identity);
	}
	public void SetActiveStructureType(StructureSO structure)
	{
		activeStructure = structure;
	}
	public void CancelBuilding()
	{
		BuildingGhost.instance.BuildingGhostEnabled(false);
		BuildingGhost.instance.SetActiveStructureGhost(null);
		SetActiveStructureType(null);
		SetBuildMode(false);
	}
	public void SetBuildMode(bool enabled)
	{
		buildMode = enabled;
	}
	public bool HasActiveStructure()
	{
		return activeStructure != null;
	}
	public bool CanBuild()
	{
		return HasActiveStructure();
	}
}
