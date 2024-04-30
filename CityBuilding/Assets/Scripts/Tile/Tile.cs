using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	public TileSO tileType;
	private void Awake()
	{
		gameObject.name = "Tile " + "(" + transform.position.x + "," + transform.position.z + ")";
		tileType = TileGenerator.instance.GetRandomTileType();
		switch (tileType.tileType)
		{
			case TileType.Lake:
				GetComponentInChildren<SpriteRenderer>().color = Color.blue;
				break;
			case TileType.Forest:
				GetComponentInChildren<SpriteRenderer>().color = Color.green;
				break;
			default:
				break;
		}
	}
	private void OnMouseDown()
	{
		if (BuildManager.instance.CanBuild(this))
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
