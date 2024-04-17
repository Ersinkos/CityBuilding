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
}
