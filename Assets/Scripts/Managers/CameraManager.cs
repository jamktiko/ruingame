using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem.Controls;


public class CameraManager : MonoBehaviour
{
	public InputReader inputReader;
	private Camera mainCamera;
	private CinemachineFreeLook freeLookVCam;

	public float zoom = 0f;
	private float zoomPercent = 1f;
	public float minimumZoom = -0.5f;
	public float maximumZoom = 1f;
	public Transform playerTransform;

	public CameraSettings _cameraData;
	private CinemachineFreeLook.Orbit[] originalOrbits;
	
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
		originalOrbits = new CinemachineFreeLook.Orbit[freeLookVCam.m_Orbits.Length];
		for (int i = 0; i < freeLookVCam.m_Orbits.Length; i++)
		{
			originalOrbits[i].m_Height = freeLookVCam.m_Orbits[i].m_Height;
			originalOrbits[i].m_Radius = freeLookVCam.m_Orbits[i].m_Radius;
		}
		mainCamera = GetComponentInChildren<Camera>();
		cameraTransformAnchor.Transform = mainCamera.transform;
		inputReader = GameManager.Instance.playerInputReader;
		UpdateCameraSettings();
	}
	private void OnEnable()
	{
		inputReader.CameraMoveEvent += OnCameraMove;
		inputReader.ScrollEvent += OnScroll;

	}

	private void OnDisable()
	{
		inputReader.CameraMoveEvent -= OnCameraMove;
	}

	private void OnScroll(float scrollY)
	{
		zoom -= scrollY / 12000;
		zoom = Mathf.Clamp(zoom, minimumZoom, maximumZoom);
		zoomPercent = zoom + 1;
		for (int i = 0; i < freeLookVCam.m_Orbits.Length; i++)
		{
			freeLookVCam.m_Orbits[i].m_Height = originalOrbits[i].m_Height * zoomPercent;
			freeLookVCam.m_Orbits[i].m_Radius = originalOrbits[i].m_Radius * zoomPercent;
		}
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
		freeLookVCam.m_XAxis.m_AccelTime = _cameraData.XAcceleration;
		freeLookVCam.m_YAxis.m_AccelTime = _cameraData.YAcceleration;
		freeLookVCam.m_XAxis.m_DecelTime = _cameraData.XSmoothing;
		freeLookVCam.m_YAxis.m_DecelTime = _cameraData.YSmoothing;
	}
}