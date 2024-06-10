using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance { get; private set; }
    private StructureSO activeStructure;
    public bool buildMode;
    public List<Structure> structuresOnMap;
    [SerializeField] private Material onMouseMaterial;
    [SerializeField] private GameObject hqPrefab;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {

    }
    public void BuildHqOnGameStart()
    {
        Tile centerTile = GameObject.Find("Tile (0,0)").GetComponent<Tile>();
        GameObject hq = Instantiate(hqPrefab, centerTile.transform.position, Quaternion.identity);
        centerTile.occupied = true;
        structuresOnMap.Add(hq.GetComponent<Structure>());
    }
    public void BuildStructure(GameObject tile)
    {
        //TO-DO : Add building functionality
        Debug.Log("Build to : " + tile.name);
        GameObject structure = Instantiate(activeStructure.prefab, tile.transform.position, Quaternion.identity);
        ResourceManager.instance.OnStructurePlacing(activeStructure);
        tile.GetComponent<Tile>().occupied = true;
        structure.GetComponent<Structure>().tile = tile.GetComponent<Tile>();
        structuresOnMap.Add(structure.GetComponent<Structure>());
    }
    public void DemolishStructure(Structure structure)
    {
        structure.tile.occupied = false;
        structuresOnMap.Remove(structure.GetComponent<Structure>());
        Destroy(structure.gameObject);
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
    public bool CanBuild(Tile tile)
    {
        if (!HasActiveStructure())
        {
            return false;
        }
        if (tile.tileType.tileType != activeStructure.placeableTile)
        {
            return false;
        }
        if (tile.occupied)
        {
            return false;
        }
        return true;
    }
    public Material GetOnMouseMaterial()
    {
        return onMouseMaterial;
    }
    public bool isHq(StructureSO structure)
    {
        return structure.id == 0;
    }
}
