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
		BuildingGhost.instance.SetGhostNewPosition(transform.position);
		if (BuildingGhost.instance.HasBuildingGhost())
			BuildingGhost.instance.BuildingGhostEnabled(true);
	}
}
