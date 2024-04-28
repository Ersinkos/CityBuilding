using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FlexibleUI : MonoBehaviour
{
	public static FlexibleUI instance { get; private set; }
	public FlexibleUISO flexibleUISO;
	public GameObject structureGhost;
	public int filterType = 0;
	[SerializeField] private GameObject structureButton;
	[SerializeField] private TextMeshProUGUI buttonText;
	[SerializeField] private List<GameObject> ghosts;
	Dictionary<string, GameObject> ghostsDictionary;
	private List<GameObject> ListedButtons;
	private int activeButtonIndex = 0;
	public Color normalColor;
	public Color disabledColor;
	public ScrollRect scrollable;

	public string filterString = "";

	private void Awake()
	{
		normalColor = Color.black;
		instance = this;
		ListedButtons = new List<GameObject>();
		ghostsDictionary = new Dictionary<string, GameObject>();
		foreach (GameObject ghost in ghosts)
		{
			ghostsDictionary.Add(ghost.name, ghost);
		}
	}

	private void Start()
	{
		ClosePanel();
	}

	public void ShowBuildings()
	{
		activeButtonIndex = 0;
		gameObject.SetActive(true);
		filterType = 2;
		foreach (Transform child in transform)
		{
			GameObject.Destroy(child.gameObject);
		}
		var triggerFirstClick = true;
		foreach (StructureSO structureType in flexibleUISO.structureTypeList.list)
		{

			if (structureType.type == StructureType.building)
			{
				GameObject button = Instantiate(structureButton);
				button.transform.SetParent(transform, false);
				button.GetComponentsInChildren<TextMeshProUGUI>()[1].text = structureType.buttonName;
				button.GetComponentsInChildren<Image>()[3].sprite = structureType.spriteImage;

				//button.GetComponent<Button>().interactable = ResourceManager.instance.HasEnoughResourceToBuild(structureType);
				button.GetComponent<StructureButton>().structureType = structureType;

				button.GetComponent<Button>().onClick.AddListener(delegate { BuildingGhost.instance.SetActiveStructureGhost(ghostsDictionary[structureType.ghostName]); });
				button.GetComponent<Button>().onClick.AddListener(delegate { BuildManager.instance.SetActiveStructureType(structureType); });

				if (triggerFirstClick)
				{
					BuildingGhost.instance.SetActiveStructureGhost(ghostsDictionary[structureType.ghostName]);
					BuildManager.instance.SetActiveStructureType(structureType);
					EventSystem.current.SetSelectedGameObject(button);
					triggerFirstClick = false;
					BuildingGhost.instance.ResetBuildingGhostRotation();
				}
				if (ResourceManager.instance.HasEnoughResourceToBuild(structureType))
				{
					var colors = button.GetComponent<Button>().colors;
					colors.normalColor = normalColor;
					button.GetComponent<Button>().colors = colors;
				}
				else
				{
					var colors = button.GetComponent<Button>().colors;
					colors.normalColor = disabledColor;
					button.GetComponent<Button>().colors = colors;
				}
			}

		}
	}

	public void UpdateResourceRequirementIndicators()
	{
		var AllStructureButtons = transform.GetComponentsInChildren<StructureButton>();
		if (AllStructureButtons.Length > 0)
		{
			foreach (StructureButton structureButton in AllStructureButtons)
			{
				if (structureButton != null)
					structureButton.UpdateInteractability();
			}
		}
	}

	public void ClosePanel()
	{
		filterString = "";
		filterType = 0;
		gameObject.SetActive(false);
		//BuildManager.instance.CancelBuilding();
		//BuildManager.instance.ModeIndicatorDeactivate();
	}
}
