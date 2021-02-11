using System;
using UnityEngine;
using Cinemachine;
using System.Collections;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
	private InputReader inputReader;
	private Camera mainCamera;
	private CinemachineFreeLook freeLookVCam;
	private bool _isRmbPressed;

	public Transform playerTransform;
	
	[SerializeField, Range(.5f, 100f)]
	private float speedMultiplier = 10f; 
	public TransformAnchor cameraTransformAnchor = default;
	
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
		cameraTransformAnchor.Transform = mainCamera.transform;
		inputReader = GameManager.Instance.playerInputReader;
	}

	private void OnEnable()
	{
		try
		{
			inputReader.CameraMoveEvent += OnCameraMove;
			Cursor.lockState = CursorLockMode.Locked;
		}
		catch{}
	}

	private void Start()
	{
		Cursor.visible = false;
	}
	private void OnDisable()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		inputReader.CameraMoveEvent -= OnCameraMove;
	}
	
	private void OnCameraMove(Vector2 cameraMovement, bool isDeviceMouse)
	{
		if (_cameraMovementLock)
			return;
		freeLookVCam.m_XAxis.m_InputAxisValue = cameraMovement.x * Time.smoothDeltaTime * speedMultiplier;
		freeLookVCam.m_YAxis.m_InputAxisValue = cameraMovement.y * Time.smoothDeltaTime * speedMultiplier;
	}

	private void OnFrameObjectEvent(Transform value)
	{
		SetupProtagonistVirtualCamera(value);
	}
}