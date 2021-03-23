using UnityEngine;
using Cinemachine;


public class CameraManager : MonoBehaviour
{
	public InputReader inputReader;
	private Camera mainCamera;
	private CinemachineFreeLook freeLookVCam;

	private int xAxisMulti = 1;
	private int yAxisMulti = 1;

	public Transform playerTransform;

	public CameraSettings _cameraData;
	
	public float speedMultiplier = 0f; 
	public TransformAnchor cameraTransformAnchor = default;
	
	private bool _cameraMovementLock = false;

	public void SetupProtagonistVirtualCamera(Transform target)
	{
		freeLookVCam.Follow = target;
		freeLookVCam.LookAt = target;
	}

	private void Awake()
	{
		_cameraData = Resources.Load<CameraSettings>("CameraSettings");
		freeLookVCam = GetComponentInChildren<CinemachineFreeLook>();
		mainCamera = GetComponentInChildren<Camera>();
		cameraTransformAnchor.Transform = mainCamera.transform;
		inputReader = GameManager.Instance.playerInputReader;
		UpdateCameraSettings();
	}
	private void OnEnable()
	{
		try
		{
			inputReader.CameraMoveEvent += OnCameraMove;
		}
		catch{}
	}

	private void OnDisable()
	{
		inputReader.CameraMoveEvent -= OnCameraMove;
	}
	
	private void OnCameraMove(Vector2 cameraMovement)
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
	
	public void UpdateCameraSettings()
	{
		speedMultiplier = _cameraData.CameraSensitivity;
		freeLookVCam.m_XAxis.m_InvertInput = _cameraData.invertAxisX;
		freeLookVCam.m_YAxis.m_InvertInput = _cameraData.invertAxisY;
	}
}