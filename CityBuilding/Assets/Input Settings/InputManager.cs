using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	public static InputManager instance;
	private CameraControlActions playerInput;

	private void Awake()
	{
		instance = this;
		playerInput = new CameraControlActions();
	}
	public CameraControlActions GetPlayerInput()
	{
		return playerInput;
	}
}
