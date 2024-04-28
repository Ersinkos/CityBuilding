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
		BuildManager.instance.BuildStructure(gameObject);
	}
	private void OnMouseEnter()
	{
		Debug.Log(gameObject.name);
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
