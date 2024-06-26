using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
	private InputAction movement;
	private Transform cameraTransform;

	[SerializeField] private float maxSpeed = 5f;
	private float speed;

	[SerializeField] private float acceleration = 10f;
	[SerializeField] private float damping = 15f;
	[SerializeField] private float stepSize = 2f;
	[SerializeField] private float zoomDampening = 7.5f;
	[SerializeField] private float minHeight = 5f;
	[SerializeField] private float maxHeight = 50f;
	[SerializeField] private float zoomSpeed = 2f;
	[SerializeField] private float maxRotationSpeed = 1f;
	[SerializeField] private float verticalRotationMin = -15f;
	[SerializeField] private float verticalRotationMax = 30f;

	[SerializeField]
	[Range(0f, 0.1f)]
	private float edgeTolerance = 0f;



	//value set in various functions 
	//used to update the position of the camera base object.
	private Vector3 targetPosition;

	private float zoomHeight;

	//used to track and maintain velocity
	private Vector3 horizontalVelocity;
	private Vector3 lastPosition;

	//tracks where the dragging action started
	Vector3 startDrag;

	private void Awake()
	{
		cameraTransform = this.GetComponentInChildren<Camera>().transform;
	}
	private void OnEnable()
	{
		zoomHeight = cameraTransform.localPosition.y;
		cameraTransform.LookAt(this.transform);

		lastPosition = this.transform.position;


	}

	private void OnDisable()
	{
		UnassignCameraMovementInput();
	}
	public void AssignCameraMovementInput(CameraControlActions playerInput)
	{
		playerInput.Camera.RotateCamera.performed += RotateCamera;
		playerInput.Camera.ZoomCamera.performed += ZoomCamera;
		playerInput.Camera.Enable();
		movement = playerInput.Camera.Movement;
	}
	private void UnassignCameraMovementInput()
	{
		InputManager.instance.GetPlayerInput().Camera.RotateCamera.performed -= RotateCamera;
		InputManager.instance.GetPlayerInput().Camera.ZoomCamera.performed -= ZoomCamera;
		InputManager.instance.GetPlayerInput().Camera.Disable();
	}
	private void Update()
	{
		//inputs
		GetKeyboardMovement();
		//CheckMouseAtScreenEdge();
		DragCamera();

		//move base and camera objects
		UpdateVelocity();
		UpdateBasePosition();
		UpdateCameraPosition();
	}

	private void UpdateVelocity()
	{
		horizontalVelocity = (this.transform.position - lastPosition) / Time.deltaTime;
		horizontalVelocity.y = 0f;
		lastPosition = this.transform.position;
	}

	private void GetKeyboardMovement()
	{
		Vector3 inputValue = movement.ReadValue<Vector2>().x * GetCameraRight()
					+ movement.ReadValue<Vector2>().y * GetCameraForward();

		inputValue = inputValue.normalized;

		if (inputValue.sqrMagnitude > 0.1f)
			targetPosition += inputValue;
	}

	private void DragCamera()
	{
		if (!Mouse.current.middleButton.isPressed)
			return;

		//create plane to raycast to
		Plane plane = new Plane(Vector3.up, Vector3.zero);
		Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

		if (plane.Raycast(ray, out float distance))
		{
			if (Mouse.current.rightButton.wasPressedThisFrame)
				startDrag = ray.GetPoint(distance);
			else
				targetPosition += startDrag - ray.GetPoint(distance);
		}
	}

	private void CheckMouseAtScreenEdge()
	{
		//mouse position is in pixels
		Vector2 mousePosition = Mouse.current.position.ReadValue();
		Vector3 moveDirection = Vector3.zero;

		//horizontal scrolling
		if (mousePosition.x < edgeTolerance * Screen.width)
			moveDirection += -GetCameraRight();
		else if (mousePosition.x > (1f - edgeTolerance) * Screen.width)
			moveDirection += GetCameraRight();

		//vertical scrolling
		if (mousePosition.y < edgeTolerance * Screen.height)
			moveDirection += -GetCameraForward();
		else if (mousePosition.y > (1f - edgeTolerance) * Screen.height)
			moveDirection += GetCameraForward();

		targetPosition += moveDirection;
	}

	private void UpdateBasePosition()
	{
		if (targetPosition.sqrMagnitude > 0.1f)
		{
			speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime * acceleration);
			transform.position += targetPosition * speed * Time.deltaTime;
		}
		else
		{
			//create smooth slow down
			horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, Time.deltaTime * damping);
			transform.position += horizontalVelocity * Time.deltaTime;
		}

		//reset for next frame
		targetPosition = Vector3.zero;
	}

	private void ZoomCamera(InputAction.CallbackContext obj)
	{
		float inputValue = -obj.ReadValue<Vector2>().y / 100f;

		if (Mathf.Abs(inputValue) > 0.1f)
		{
			zoomHeight = cameraTransform.localPosition.y + inputValue * stepSize;

			if (zoomHeight < minHeight)
				zoomHeight = minHeight;
			else if (zoomHeight > maxHeight)
				zoomHeight = maxHeight;
		}
	}

	private void UpdateCameraPosition()
	{
		//set zoom target
		Vector3 zoomTarget = new Vector3(cameraTransform.localPosition.x, zoomHeight, cameraTransform.localPosition.z);
		//add vector for forward/backward zoom
		zoomTarget -= zoomSpeed * (zoomHeight - cameraTransform.localPosition.y) * Vector3.forward;

		cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, zoomTarget, Time.deltaTime * zoomDampening);
		cameraTransform.LookAt(this.transform);
	}

	private void RotateCamera(InputAction.CallbackContext obj)
	{
		if (!Mouse.current.rightButton.isPressed)
			return;

		float horizontalValue = obj.ReadValue<Vector2>().x;
		float verticalValue = -obj.ReadValue<Vector2>().y;

		if (horizontalValue != 0)
		{
			RotateHorizontally(horizontalValue);
		}
		if (verticalValue != 0)
		{
			RotateVertically(verticalValue);
		}

	}
	private void RotateHorizontally(float horizontalValue)
	{
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, horizontalValue * maxRotationSpeed * Time.deltaTime + transform.rotation.eulerAngles.y, 0f);
	}
	private void RotateVertically(float verticalValue)
	{
		float value = GetNegativeRotationX(transform);
		value += verticalValue * maxRotationSpeed * Time.deltaTime;
		value = Mathf.Clamp(value, verticalRotationMin, verticalRotationMax);
		transform.rotation = Quaternion.Euler(value, transform.rotation.eulerAngles.y, 0f);
	}
	private float GetNegativeRotationX(Transform transform)
	{
		float angle = transform.rotation.eulerAngles.x;
		if (angle > 180f)
		{
			angle -= 360f;
		}
		return angle;
	}
	//gets the horizontal forward vector of the camera
	private Vector3 GetCameraForward()
	{
		Vector3 forward = cameraTransform.forward;
		forward.y = 0f;
		return forward;
	}

	//gets the horizontal right vector of the camera
	private Vector3 GetCameraRight()
	{
		Vector3 right = cameraTransform.right;
		right.y = 0f;
		return right;
	}
}