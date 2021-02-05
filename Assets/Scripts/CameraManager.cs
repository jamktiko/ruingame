using System;
using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
	public InputReader inputReader;
	public Camera mainCamera;
	public CinemachineFreeLook freeLookVCam;
	private bool _isRMBPressed;

	public Transform playerTransform;
	
	[SerializeField, Range(.5f, 3f)]
	private float _speedMultiplier = 1f; 
	public TransformAnchor _cameraTransformAnchor = default;
	

	private bool _cameraMovementLock = false;

	public void SetupProtagonistVirtualCamera(Transform target)
	{
		freeLookVCam.Follow = target;
		freeLookVCam.LookAt = target;
	}

	private void Awake()
	{
		freeLookVCam = GetComponentInChildren<CinemachineFreeLook>();
		mainCamera = GetComponentInChildren<Camera>();
		_cameraTransformAnchor.Transform = mainCamera.transform;
	}

	private void OnEnable()
	{
		try
		{
			inputReader.cameraMoveEvent += OnCameraMove;
			inputReader.enableMouseControlCameraEvent += OnEnableMouseControlCamera;
			inputReader.disableMouseControlCameraEvent += OnDisableMouseControlCamera;
		}
		catch{}
	}

	private void OnDisable()
	{
		inputReader.cameraMoveEvent -= OnCameraMove;
		inputReader.enableMouseControlCameraEvent -= OnEnableMouseControlCamera;
		inputReader.disableMouseControlCameraEvent -= OnDisableMouseControlCamera;
	}

	private void OnEnableMouseControlCamera()
	{
		_isRMBPressed = true;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		StartCoroutine(DisableMouseControlForFrame());
	}

	IEnumerator DisableMouseControlForFrame()
	{
		_cameraMovementLock = true;
		yield return new WaitForEndOfFrame();
		_cameraMovementLock = false;
	}

	private void OnDisableMouseControlCamera()
	{
		_isRMBPressed = false;

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		freeLookVCam.m_XAxis.m_InputAxisValue = 0;
		freeLookVCam.m_YAxis.m_InputAxisValue = 0;
	}

	private void OnCameraMove(Vector2 cameraMovement, bool isDeviceMouse)
	{
		if (_cameraMovementLock)
			return;

		if (isDeviceMouse && !_isRMBPressed)
			return;

		freeLookVCam.m_XAxis.m_InputAxisValue = cameraMovement.x * Time.smoothDeltaTime * _speedMultiplier;
		freeLookVCam.m_YAxis.m_InputAxisValue = cameraMovement.y * Time.smoothDeltaTime * _speedMultiplier;
	}

	private void OnFrameObjectEvent(Transform value)
	{
		SetupProtagonistVirtualCamera(value);
	}
}