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
	[SerializeField] private GameObject researchButton;
	[SerializeField] private TextMeshProUGUI buttonText;
	[SerializeField] private List<GameObject> ghosts;
	Dictionary<string, GameObject> ghostsDictionary;
	private List<GameObject> ListedButtons;
	private int activeButtonIndex = 0;
	public Color normalColor;
	public Color disabledColor;
	public ScrollRect scrollable;
	public Button ScrollLeftButton;
	public Button ScrollRightButton;

	private Vector2 savedPosition;

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
		//ClosePanel();
	}

	public void FilterBuildings(string filterText){
		Debug.Log("Filtering by: " + filterText);
		filterString = filterText;
		//ShowBuildings();
	}

	public void ScrollButtonLeft()
	{
		scrollable.normalizedPosition = new Vector2(scrollable.normalizedPosition.x - 0.15f, 1.0F);
		if (scrollable.normalizedPosition.x <= 0f) ScrollLeftButton.interactable = false;
		ScrollRightButton.interactable = true;
	}

	public void ScrollButtonRight()
	{
		scrollable.normalizedPosition = new Vector2(scrollable.normalizedPosition.x + 0.15f, 1.0F);
		if (scrollable.normalizedPosition.x >= 1f) ScrollRightButton.interactable = false;
		ScrollLeftButton.interactable = true;
	}

	public void Navigate(string direction)
	{
		if(ListedButtons.Count > 0 && gameObject.activeSelf)
		{
			int newActiveButtonIndex = 0;
			if (direction == "right")
			{
				newActiveButtonIndex = activeButtonIndex + 1;
				if (newActiveButtonIndex > ListedButtons.Count - 1)
				{
					newActiveButtonIndex = 0;
				}
			}
			else if (direction == "left")
			{
				newActiveButtonIndex = activeButtonIndex - 1;
				if (newActiveButtonIndex < 0)
				{
					newActiveButtonIndex = ListedButtons.Count - 1;
				}
			}

			var choosenStructure = ListedButtons[newActiveButtonIndex];
			activeButtonIndex = newActiveButtonIndex;
			EventSystem.current.SetSelectedGameObject(choosenStructure);
			choosenStructure.GetComponent<Button>().onClick.Invoke();
		}
	}

	public void ShowTunnels()
	{
		if (filterType == 2)
		{
			savedPosition = scrollable.normalizedPosition;
		}

		ListedButtons.Clear();
		activeButtonIndex = 0;
		gameObject.SetActive(true);
		filterType = 1;
		foreach (Transform child in transform)
		{
			GameObject.Destroy(child.gameObject);
		}

		ListedButtons.Clear();

		var triggerFirstClick = true;
		//foreach (StructureSO structureType in flexibleUISO.structureTypeList.list)
		//{
		//	var showStructure = false;
		//	if (structureType.unlockedWithResearch)
		//	{
		//		showStructure = ResearchManager.instance.IsResearched(structureType.researchToUnlock);
		//	}
		//	else
		//	{
		//		showStructure = true;
		//	}

		//	if (showStructure && structureType.type != StructureType.building)
		//	{

		//		if (structureType.id == 901 || structureType.id == 909 || structureType.id == 911)
		//		{
		//			//Debug.Log("Flexible UI Tunnel 1 statement!!");
		//			continue;
		//		}
		//		if (structureType.type == StructureType.tunnel)
		//		{
		//			//Debug.Log("Flexible UI Tunnel 2 statement!!");
		//			GameObject tunnelButton = Instantiate(structureButton);
		//			tunnelButton.transform.SetParent(transform, false);
		//			tunnelButton.GetComponentsInChildren<TextMeshProUGUI>()[1].text = LocalizationManager.instance.TranslateSO(structureType.buttonName, structureType.buttonNameLocale);
		//			tunnelButton.GetComponentsInChildren<Image>()[3].sprite = structureType.spriteImage;
		//			tunnelButton.GetComponent<StructureButton>().structureType = structureType;
		//			if (structureType.tutorialLocked)
		//			{
		//				tunnelButton.transform.Find("Locked").gameObject.SetActive(true);
		//			}
		//			else
		//			{
		//				tunnelButton.GetComponent<Button>().onClick.AddListener(delegate { TunnelManager.instance.StartBuildingTunnel(); });
		//				tunnelButton.GetComponent<Button>().onClick.AddListener(delegate { TunnelManager.instance.SetActiveTunnel(structureType, structureType.prefab, GameObject.Find("BuildingGhost").transform.Find(structureType.ghostName).gameObject); });
		//				tunnelButton.transform.Find("Locked").gameObject.SetActive(false);
		//			}

		//			ListedButtons.Add(tunnelButton);

		//			if (triggerFirstClick)
		//			{
		//				TunnelManager.instance.StartBuildingTunnel();
		//				EventSystem.current.SetSelectedGameObject(tunnelButton);
		//				triggerFirstClick = false;
		//			}

		//			continue;
		//		}
		//		GameObject button = Instantiate(structureButton);
		//		button.transform.SetParent(transform, false);
		//		button.GetComponentsInChildren<TextMeshProUGUI>()[1].text = LocalizationManager.instance.TranslateSO(structureType.buttonName, structureType.buttonNameLocale);
		//		button.GetComponentsInChildren<Image>()[3].sprite = structureType.spriteImage;
		//		button.GetComponent<StructureButton>().structureType = structureType;
		//		if (structureType.tutorialLocked)
		//		{
		//			button.transform.Find("Locked").gameObject.SetActive(true);
		//		}
		//		else
		//		{
		//			button.GetComponent<Button>().onClick.AddListener(delegate { TunnelManager.instance.StopBuildingTunnel(); });
		//			button.GetComponent<Button>().onClick.AddListener(delegate { BuildManager.instance.SetActiveStructureGhost(ghostsDictionary[structureType.ghostName]); });
		//			button.GetComponent<Button>().onClick.AddListener(delegate { BuildManager.instance.SetActiveStructureType(structureType); });
		//			button.transform.Find("Locked").gameObject.SetActive(false);
		//		}
		//	}
		//}
	}

	//public void ShowBuildings()
	//{

	//	ScrollRightButton.interactable = true;
	//	ScrollLeftButton.interactable = false;
	//	ListedButtons.Clear();
	//	activeButtonIndex = 0;
	//	gameObject.SetActive(true);
	//	filterType = 2;
	//	foreach (Transform child in transform)
	//	{
	//		GameObject.Destroy(child.gameObject);
	//	}
	//	var triggerFirstClick = true;
	//	foreach (StructureTypeSO structureType in flexibleUISO.structureTypeList.list)
	//	{
	//		var showStructure = false;
	//		if (structureType.unlockedWithResearch)
	//		{
	//			showStructure = ResearchManager.instance.IsResearched(structureType.researchToUnlock);
	//		}
	//		else
	//		{
	//			showStructure = true;
	//		}

	//		if(showStructure && filterString != ""){
	//				if(filterString == "Functional"){
	//					if(!structureType.isFunctional) showStructure = false;
	//				}else if(filterString == "Population"){
	//					if(!structureType.isPopulation) showStructure = false;
	//				}else if(filterString == "SettlerMorale"){
	//					if(!structureType.isSettlerMorale) showStructure = false;
	//				}else if(filterString == "StorageCapacity"){
	//					if(!structureType.isStorageCapacity) showStructure = false;
	//				}else if(filterString == "ResearchCredit"){
	//					if(!structureType.isResearchCredit) showStructure = false;
	//				}else if(filterString == "ConductiveMaterial"){
	//					if(!structureType.isConductiveMaterial) showStructure = false;
	//				}else if(filterString == "EnergyMaterial"){
	//					if(!structureType.isEnergyMaterial) showStructure = false;
	//				}else if(filterString == "BuildingMaterial"){
	//					if(!structureType.isBuildingMaterial) showStructure = false;
	//				}else if(filterString == "LifeSupportMaterial"){
	//					if(!structureType.isLifeSupportMaterial) showStructure = false;
	//				}
	//		}

	//		if (showStructure && structureType.type == StructureType.building)
	//		{
	//			GameObject button = Instantiate(structureButton);
	//			button.transform.SetParent(transform, false);
	//			button.GetComponentsInChildren<TextMeshProUGUI>()[1].text = LocalizationManager.instance.TranslateSO(structureType.buttonName, structureType.buttonNameLocale);
	//			button.GetComponentsInChildren<Image>()[3].sprite = structureType.spriteImage;

	//			//button.GetComponent<Button>().interactable = ResourceManager.instance.HasEnoughResourceToBuild(structureType);
	//			button.GetComponent<StructureButton>().structureType = structureType;


	//			if (structureType.tutorialLocked)
	//			{
	//				button.transform.Find("Locked").gameObject.SetActive(true);
	//			}
	//			else
	//			{
	//				button.GetComponent<Button>().onClick.AddListener(delegate { TunnelManager.instance.StopBuildingTunnel(); });
	//				button.GetComponent<Button>().onClick.AddListener(delegate { BuildManager.instance.SetActiveStructureGhost(ghostsDictionary[structureType.ghostName]); });
	//				button.GetComponent<Button>().onClick.AddListener(delegate { BuildManager.instance.SetActiveStructureType(structureType); });
	//				button.transform.Find("Locked").gameObject.SetActive(false);
	//			}

	//			ListedButtons.Add(button);

	//			if (triggerFirstClick)
	//			{
	//				TunnelManager.instance.StopBuildingTunnel();
	//				BuildManager.instance.SetActiveStructureGhost(ghostsDictionary[structureType.ghostName]);
	//				BuildManager.instance.SetActiveStructureType(structureType);
	//				BuildManager.instance.SetActiveSpaceshipPart(null);
	//				EventSystem.current.SetSelectedGameObject(button);
	//				triggerFirstClick = false;
	//				BuildingGhost.instance.ResetBuildingGhostRotation();
	//			}
	//			if (ResourceManager.instance.HasEnoughResourceToBuild(structureType))
	//			{
	//				var colors = button.GetComponent<Button>().colors;
	//				colors.normalColor = normalColor;
	//				button.GetComponent<Button>().colors = colors;
	//			}
	//			else
	//			{
	//				var colors = button.GetComponent<Button>().colors;
	//				colors.normalColor = disabledColor;
	//				button.GetComponent<Button>().colors = colors;
	//			}
	//		}

	//	}
	//	GameObject buttonResearch = Instantiate(researchButton);
	//	buttonResearch.transform.SetParent(transform, false);
	//	StartCoroutine(LateSavedPosition());
	//}

	private IEnumerator LateSavedPosition()
	{
		yield return new WaitForEndOfFrame();
		scrollable.normalizedPosition = savedPosition;

		ScrollLeftButton.interactable = true;
		ScrollRightButton.interactable = true;
		if (savedPosition.x <= 0f) ScrollLeftButton.interactable = false;
		if (savedPosition.x >= 1f) ScrollRightButton.interactable = false;

	}

	//public void ShowSpaceshipParts()
	//{
	//	if (filterType == 2)
	//	{
	//		savedPosition = scrollable.normalizedPosition;
	//	}

	//	BuildingGhost.instance.BuildingGhostDeactivate();
	//	ListedButtons.Clear();
	//	activeButtonIndex = 0;
	//	gameObject.SetActive(true);
	//	filterType = 3;
	//	foreach (Transform child in transform)
	//	{
	//		GameObject.Destroy(child.gameObject);
	//	}
	//	var triggerFirstClick = true;
	//	foreach (SpaceshipPartSO spaceshipPart in flexibleUISO.spaceshipPartList.list)
	//	{
	//		if (spaceshipPart.id == 9000 && BuildManager.instance.isThereAnyLaunchPad)
	//		{
	//			continue;
	//		}
	//		bool showStructure = false;

	//		if (spaceshipPart.spaceshipPartStructure.unlockedWithResearch)
	//		{
	//			showStructure = ResearchManager.instance.IsResearched(spaceshipPart.spaceshipPartStructure.researchToUnlock);
	//		}
	//		else
	//		{
	//			showStructure = true;
	//		}

	//		if (showStructure)
	//		{
	//			GameObject button = Instantiate(structureButton);

	//			button.transform.SetParent(transform, false);
	//			//Debug.Log(spaceshipPart.nameStringLocale);
	//			button.GetComponentsInChildren<TextMeshProUGUI>()[1].text = LocalizationManager.instance.TranslateSO(spaceshipPart.nameString, spaceshipPart.nameStringLocale);
	//			button.GetComponentsInChildren<Image>()[3].sprite = spaceshipPart.spriteImage;
	//			button.GetComponent<StructureButton>().spaceshipPart = spaceshipPart;

	//			if (!BuildManager.instance.isThereAnyLaunchPad && spaceshipPart.id != 9000)
	//			{
	//				button.transform.Find("Locked").gameObject.SetActive(true);
	//			}
	//			else
	//			{
	//				if (spaceshipPart.id == 9000)
	//				{
	//					button.GetComponent<Button>().onClick.AddListener(delegate { BuildManager.instance.SetActiveStructureGhost(ghostsDictionary[spaceshipPart.ghostName]); });
	//					button.GetComponent<Button>().onClick.AddListener(delegate { BuildManager.instance.SetActiveStructureType(spaceshipPart.spaceshipPartStructure); });
	//					button.GetComponent<Button>().onClick.AddListener(delegate { TunnelManager.instance.StopBuildingTunnel(); });

	//					if (ResourceManager.instance.HasEnoughResourceToBuild(spaceshipPart))
	//					{
	//						var colors = button.GetComponent<Button>().colors;
	//						colors.normalColor = normalColor;
	//						button.GetComponent<Button>().colors = colors;
	//					}
	//					else
	//					{
	//						var colors = button.GetComponent<Button>().colors;
	//						colors.normalColor = disabledColor;
	//						button.GetComponent<Button>().colors = colors;
	//					}
	//				}
	//				else
	//				{
	//					button.GetComponent<Button>().onClick.AddListener(delegate { BuildManager.instance.SetActiveStructureGhost(ghostsDictionary[spaceshipPart.ghostName]); });
	//					button.GetComponent<Button>().onClick.AddListener(delegate { BuildManager.instance.SetActiveSpaceshipPart(spaceshipPart); });
	//					button.GetComponent<Button>().onClick.AddListener(delegate { TunnelManager.instance.StopBuildingTunnel(); });
	//				}

	//				ListedButtons.Add(button);
	//				if (triggerFirstClick)
	//				{
	//					TunnelManager.instance.StopBuildingTunnel();
	//					BuildManager.instance.SetActiveStructureGhost(ghostsDictionary[spaceshipPart.ghostName]);
	//					switch (spaceshipPart.id)
	//					{
	//						case 9000:
	//							BuildManager.instance.SetActiveStructureType(spaceshipPart.spaceshipPartStructure);
	//							BuildManager.instance.SetActiveSpaceshipPart(null);
	//							break;
	//						default:
	//							BuildManager.instance.SetActiveStructureType(null);
	//							BuildManager.instance.SetActiveSpaceshipPart(spaceshipPart);
	//							break;
	//					}

	//					EventSystem.current.SetSelectedGameObject(button);
	//					triggerFirstClick = false;
	//					BuildingGhost.instance.ResetBuildingGhostRotation();
	//				}
	//			}

	//			//button.GetComponent<Button>().interactable = ResourceManager.instance.HasEnoughResourceToBuild(structureType);
	//		}
	//	}
	//	if (BuildManager.instance.launchPad != null)
	//	{
	//		float cameraTargetPosX = BuildManager.instance.launchPad.transform.position.x;
	//		float cameraTargetPosZ = BuildManager.instance.launchPad.transform.position.z;
	//		RTSCameraTargetController.Instance.JumpCameraTargetToPos(cameraTargetPosX, cameraTargetPosZ);
	//		GameCanvas.instance.ShowStructureInfoPanel(BuildManager.instance.launchPad.GetComponent<Structure>().structureType, BuildManager.instance.launchPad);
	//	}
	//}

	//public void UpdateResourceRequirementIndicators()
	//{
	//	var AllStructureButtons = transform.GetComponentsInChildren<StructureButton>();
	//	if (AllStructureButtons.Length > 0)
	//	{
	//		foreach (StructureButton structureButton in AllStructureButtons)
	//		{
	//			if (structureButton != null)
	//				structureButton.UpdateInteractability();
	//		}
	//	}
	//}
	//public void UpdateButtonTutorialLock()
	//{
	//	var AllStructureButtons = transform.GetComponentsInChildren<StructureButton>();
	//	foreach (StructureButton structureButton in AllStructureButtons)
	//	{
	//		if(structureButton.structureType){
	//			if (structureButton.structureType.tutorialLocked)
	//			{
	//				structureButton.transform.Find("Locked").gameObject.SetActive(true);
	//			}
	//			else
	//			{
	//				structureButton.GetComponent<Button>().onClick.AddListener(delegate { TunnelManager.instance.StopBuildingTunnel(); });
	//				structureButton.GetComponent<Button>().onClick.AddListener(delegate { BuildManager.instance.SetActiveStructureGhost(ghostsDictionary[structureButton.structureType.ghostName]); });
	//				structureButton.GetComponent<Button>().onClick.AddListener(delegate { BuildManager.instance.SetActiveStructureType(structureButton.structureType); });
	//				structureButton.transform.Find("Locked").gameObject.SetActive(false);
	//			}
	//		}

	//	}

	//}
	//public void ClosePanel()
	//{
	//	if(FilterButton.instance)
	//		FilterButton.instance.CloseSelection();
	//	filterString = "";
	//	if (filterType == 2)
	//	{
	//		savedPosition = scrollable.normalizedPosition;
	//	}
	//	filterType = 0;
	//	gameObject.SetActive(false);
	//	BuildingGhost.instance.buildingGhostNote.SetActive(false);
	//	//if (BuildManager.instance.HasStructureType() || BuildManager.instance.GetActiveSpaceshipPart() != null)
	//	//{

	//	//}
	//	BuildManager.instance.CancelBuilding();
	//	BuildManager.instance.ModeIndicatorDeactivate();
	//}
}
