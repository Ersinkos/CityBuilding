using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	public static InputManager instance;
	private CameraControlActions playerInput;
	[SerializeField] CameraMovement cameraMovement;

	private void Awake()
	{
		instance = this;
		playerInput = new CameraControlActions();
		cameraMovement.AssignCameraMovementInput(playerInput);
		GameCanvas.instance.AssignGameCanvasInput(playerInput);
	}
	public CameraControlActions GetPlayerInput()
	{
		return playerInput;
	}
}
