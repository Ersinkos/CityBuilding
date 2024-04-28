using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameCanvas : MonoBehaviour
{
	public static GameCanvas instance;
	[SerializeField] private TextMeshProUGUI waterMaterialText;
	[SerializeField] private TextMeshProUGUI ironMaterialText;
	[SerializeField] private TextMeshProUGUI energyMaterialText;
	[SerializeField] private TextMeshProUGUI moneyMaterialText;
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
	public void ChangeWaterMaterialText(string text)
	{
		waterMaterialText.text = text;
	}
	public void ChangeIronMaterialText(string text)
	{
		ironMaterialText.text = text;
	}
	public void ChangeEnergyMaterialText(string text)
	{
		energyMaterialText.text = text;
	}
	public void ChangeMoneyMaterialText(string text)
	{
		moneyMaterialText.text = text;
	}
}
