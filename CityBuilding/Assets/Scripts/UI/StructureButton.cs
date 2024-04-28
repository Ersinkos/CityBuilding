using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class StructureButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	public StructureSO structureType;
	public GameObject HoverPanel;

	public void OnPointerEnter(PointerEventData eventData)
	{
		HoverPanel.SetActive(true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		HoverPanel.SetActive(false);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		bool hasResource = false;
		if (structureType != null)
		{
			hasResource = ResourceManager.instance.HasEnoughResourceToBuild(structureType);
		}
		if (hasResource)
		{
			HoverPanel.SetActive(false);
			foreach (GameObject StructureActiveIndicator in GameObject.FindGameObjectsWithTag("StructureActiveIndicator"))
			{
				StructureActiveIndicator.SetActive(false);
			}
			this.transform.GetChild(0).gameObject.SetActive(true);
		}
	}

	void Start()
	{
		if (structureType != null)
		{
			this.transform.GetChild(0).gameObject.SetActive(structureType.id == 1005 || structureType.id == 902);
		}
		else
		{
			this.transform.GetChild(0).gameObject.SetActive(false);
		}
		HoverPanel.SetActive(false);
		if (!structureType) return;

		UpdateInteractability();
	}

	public void UpdateInteractability()
	{
		if (structureType != null)
		{
			//UITranslation
			var hoverString = structureType.description + "\n\n<size=105%><b>" + "Cost" + "</b><size=120%>";
			hoverString = SetColorStructureButton(hoverString);

			HoverPanel.GetComponentInChildren<TextMeshProUGUI>().text = IconParser.instance.Parse(hoverString);

			if (ResourceManager.instance.HasEnoughResourceToBuild(structureType))
			{
				var colors = GetComponent<Button>().colors;
				colors.normalColor = FlexibleUI.instance.normalColor;
				GetComponent<Button>().colors = colors;
			}
			else
			{
				var colors = GetComponent<Button>().colors;
				colors.normalColor = FlexibleUI.instance.disabledColor;
				GetComponent<Button>().colors = colors;
			}
		}
		//GetComponent<Button>().interactable = ResourceManager.instance.HasEnoughResourceToBuild(structureType);
	}

	public string SetColorStructureButton(string hoverString)
	{
		var originalhoverString = hoverString;
		int water = structureType.waterCost;
		int iron = structureType.ironCost;
		int energy = structureType.energyCost;
		int money = structureType.moneyCost;

		if (structureType != null)
		{
			if (water != 0)
			{
				var color = ResourceManager.instance.GetWater() >= water ? "#fff" : "red";
				hoverString = hoverString + "\n<color=" + color + ">" + water + "</color> Water";
			}
			if (iron != 0)
			{
				var color = ResourceManager.instance.GetIron() >= iron ? "#fff" : "red";
				hoverString = hoverString + "\n<color=" + color + ">" + iron + "</color> Iron";
			}
			if (energy != 0)
			{
				var color = ResourceManager.instance.GetEnergy() >= energy ? "#fff" : "red";
				hoverString = hoverString + "\n<color=" + color + ">" + energy + "</color> Energy";
			}
			if (money != 0)
			{
				var color = ResourceManager.instance.GetMoney() >= money ? "#fff" : "red";
				hoverString = hoverString + "\n<color=" + color + ">" + money + "</color> Money";
			}
		}
		if (hoverString == originalhoverString)
		{
			return hoverString + "\n" + "Free";
		}
		return hoverString;
	}
	// Update is called once per frame
	void Update()
	{

	}
}
