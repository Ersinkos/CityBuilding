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
		Debug.Log("on mouse down");
	}
	private void OnMouseEnter()
	{

	}
}
