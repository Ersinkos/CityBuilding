using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
	public static BuildManager instance { get; private set; }
	private void Awake()
	{
		instance = this;
	}
	public void BuildStructure(GameObject tile)
	{
		//TO-DO : Add building functionality
		Debug.Log("Build to : " + tile.name);
	}
}
