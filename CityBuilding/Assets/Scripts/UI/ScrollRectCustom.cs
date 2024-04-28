using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ScrollRectCustom : ScrollRect, IPointerEnterHandler, IPointerExitHandler
{
	private static string mouseScrollWheelAxis = "Mouse ScrollWheel";
	private bool swallowMouseWheelScrolls = true;
	private bool isMouseOver = false;
	private InputAction scrollAction;

	private void OnEnable()
	{
		if (verticalScrollbar != null)
			verticalScrollbar.onValueChanged.AddListener(delegate { verticalNormalizedPosition = verticalScrollbar.value; });
		// Create the input action for scrolling
		scrollAction = new InputAction(binding: "<Mouse>/scroll");
		scrollAction.Enable();

		// Register a callback for when the action is performed
		scrollAction.performed += OnScrollPerformed;
	}

	private void OnDisable()
	{
		scrollAction.Disable();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		isMouseOver = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isMouseOver = false;
	}

	private void OnScrollPerformed(InputAction.CallbackContext context)
	{
		// Detect the mouse wheel and generate a scroll. This fixes the issue where Unity will prevent our ScrollRect
		// from receiving any mouse wheel messages if the mouse is over a raycast target (such as a button).
		if (isMouseOver && IsMouseWheelRolling())
		{
			Vector2 scrollValue = context.ReadValue<Vector2>();
			var delta = scrollValue.y;

			PointerEventData pointerData = new PointerEventData(EventSystem.current);
			pointerData.scrollDelta = new Vector2(0f, delta);

			swallowMouseWheelScrolls = false;
			OnScroll(pointerData);
			swallowMouseWheelScrolls = true;
		}
	}

	public override void OnScroll(PointerEventData data)
	{
		if (IsMouseWheelRolling() && swallowMouseWheelScrolls)
		{
			// Eat the scroll so that we don't get a double scroll when the mouse is over an image
		}
		else
		{
			// Amplify the mousewheel so that it matches the scroll sensitivity.
			if (data.scrollDelta.y < -Mathf.Epsilon)
				data.scrollDelta = new Vector2(0f, -scrollSensitivity);
			else if (data.scrollDelta.y > Mathf.Epsilon)
				data.scrollDelta = new Vector2(0f, scrollSensitivity);

			base.OnScroll(data);
		}
	}

	private static bool IsMouseWheelRolling()
	{
		return UnityEngine.Input.GetAxis(mouseScrollWheelAxis) != 0;
	}
}
