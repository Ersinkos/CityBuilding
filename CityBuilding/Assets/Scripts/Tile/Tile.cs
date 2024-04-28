using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	private void Awake()
	{
		gameObject.name = "Tile " + "(" + transform.position.x + "," + transform.position.z + ")";
	}
	private void OnMouseDown()
	{
		if (BuildManager.instance.CanBuild())
		{
			//Debug.Log("can build to this slot");
			BuildManager.instance.BuildStructure(gameObject);
			FlexibleUI.instance.UpdateResourceRequirementIndicators();
		}
	}
	private void OnMouseEnter()
	{
		BuildingGhost.instance.SetGhostNewPosition(transform.position);
		if (BuildingGhost.instance.HasBuildingGhost())
			BuildingGhost.instance.BuildingGhostEnabled(true);
	}
	private void OnMouseExit()
	{
		if (BuildingGhost.instance.HasBuildingGhost())
			BuildingGhost.instance.BuildingGhostEnabled(false);
	}
}
