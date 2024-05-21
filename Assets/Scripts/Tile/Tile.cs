using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileSO tileType;
    public bool occupied;
    private bool canBuild;
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
        if (canBuild)
        {
            //Debug.Log("can build to this slot");
            BuildManager.instance.BuildStructure(gameObject);
            FlexibleUI.instance.UpdateResourceRequirementIndicators();
        }
    }
    private void OnMouseEnter()
    {
        BuildingGhost.instance.SetGhostNewPosition(transform.position);
        canBuild = BuildingGhost.instance.HasBuildingGhost() && BuildManager.instance.CanBuild(this);
        if (BuildingGhost.instance.HasBuildingGhost())
        {
            BuildingGhost.instance.BuildingGhostEnabled(true);
            BuildingGhost.instance.UpdateGhostColor(canBuild);
        }


    }
    private void OnMouseExit()
    {
        if (BuildingGhost.instance.HasBuildingGhost())
            BuildingGhost.instance.BuildingGhostEnabled(false);
    }
}
