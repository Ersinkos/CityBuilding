using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
	public static BuildingGhost instance;
	Vector3 ghostNewPosition;

	private void Awake()
	{
		instance = this;
	}
	private void Update()
	{
		transform.position = ghostNewPosition;
	}
	public void SetGhostNewPosition(Vector3 newPos)
	{
		ghostNewPosition = newPos;
	}
}
