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
    [SerializeField] private TextMeshProUGUI storageCapacityText;
    [SerializeField] private TextMeshProUGUI populationText;
    public Color normalMaterialTextColor;
    public Color exceedCapacityColor;
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
    private void OnDisable()
    {
        UnassignGameCanvasInput();
    }
    public void AssignGameCanvasInput(CameraControlActions playerInput)
    {
        InputManager.instance.GetPlayerInput().UI.Cancel.performed += OnCancelKey;
        InputManager.instance.GetPlayerInput().UI.Enable();
    }
    private void UnassignGameCanvasInput()
    {
        InputManager.instance.GetPlayerInput().UI.Cancel.performed -= OnCancelKey;
        InputManager.instance.GetPlayerInput().UI.Disable();
    }
    public void ChangeWaterMaterialText(string text)
    {
        waterMaterialText.text = text;
        ChangeMaterialTextColor(ResourceType.Water);
    }
    public void ChangeIronMaterialText(string text)
    {
        ironMaterialText.text = text;
        ChangeMaterialTextColor(ResourceType.Iron);
    }
    public void ChangeEnergyMaterialText(string text)
    {
        energyMaterialText.text = text;
        ChangeMaterialTextColor(ResourceType.Energy);
    }
    public void ChangeMoneyMaterialText(string text)
    {
        moneyMaterialText.text = text;
        ChangeMaterialTextColor(ResourceType.Money);
    }
    public void ChangeStorageCapacityText(string text)
    {
        storageCapacityText.text = text;
    }
    public void ChangePopulationText(string text)
    {
        populationText.text = text;
    }
    public void ChangeMaterialTextColor(ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceType.Water:

                if (ResourceManager.instance.GetWater() > ResourceManager.instance.GetStorageCapacity())
                    waterMaterialText.color = exceedCapacityColor;
                else
                    waterMaterialText.color = normalMaterialTextColor;

                break;
            case ResourceType.Iron:

                if (ResourceManager.instance.GetIron() > ResourceManager.instance.GetStorageCapacity())
                    ironMaterialText.color = exceedCapacityColor;
                else
                    ironMaterialText.color = normalMaterialTextColor;

                break;
            case ResourceType.Energy:

                if (ResourceManager.instance.GetEnergy() > ResourceManager.instance.GetStorageCapacity())
                    energyMaterialText.color = exceedCapacityColor;
                else
                    energyMaterialText.color = normalMaterialTextColor;

                break;
            case ResourceType.Money:

                if (ResourceManager.instance.GetMoney() > ResourceManager.instance.GetStorageCapacity())
                    moneyMaterialText.color = exceedCapacityColor;
                else
                    moneyMaterialText.color = normalMaterialTextColor;

                break;
        }
    }

}
