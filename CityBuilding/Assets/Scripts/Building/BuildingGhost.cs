using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
	public static BuildingGhost instance;
	private Vector3 ghostNewPosition;
	private GameObject buildingGhost;
	private Vector3 rotation;
	private float rotationAmount;
	private List<Renderer> activeGhostRenderers;
	private void Awake()
	{
		instance = this;
		ghostNewPosition = Vector3.zero;
		rotation = Vector3.zero;
		rotationAmount = 90f;
		activeGhostRenderers = new List<Renderer>();
	}
	private void Update()
	{
		transform.position = ghostNewPosition;
	}
	public void SetGhostNewPosition(Vector3 newPos)
	{
		ghostNewPosition = newPos;
	}
	public void SetActiveStructureGhost(GameObject activeGhost)
	{
		buildingGhost = activeGhost;
		rotation = Vector3.zero;
		ResetBuildingGhostRotation();
		if (activeGhost == null)
		{
			Awake();
		}
	}
	public void ResetBuildingGhostRotation()
	{
		transform.rotation = Quaternion.identity;
	}
}
