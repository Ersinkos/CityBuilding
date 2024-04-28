using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameCanvas : MonoBehaviour
{
	public static GameCanvas instance;
	private void Awake()
	{
		instance = this;
	}
	public void OnStructureButton()
	{
		FlexibleUI.instance.ShowBuildings();
		BuildManager.instance.SetBuildMode(true);
	}
	public void OnCancelKey(InputAction.CallbackContext obj)
	{
		Debug.Log("on cancel key");
		if (FlexibleUI.instance?.filterType != 0)
		{
			FlexibleUI.instance.ClosePanel();
			return;
		}
	}
	private void OnEnable()
	{
		InputManager.instance.GetPlayerInput().UI.Cancel.performed += OnCancelKey;
		InputManager.instance.GetPlayerInput().UI.Enable();
	}
	private void OnDisable()
	{
		InputManager.instance.GetPlayerInput().UI.Cancel.performed -= OnCancelKey;
		InputManager.instance.GetPlayerInput().UI.Disable();
	}
}
