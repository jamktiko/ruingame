using UnityEngine;
using Cinemachine;


public class CameraManager : MonoBehaviour
{
	public InputReader inputReader;
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
	}
	private void OnEnable()
	{
		try
		{
			inputReader.CameraMoveEvent += OnCameraMove;
		}
		catch{}
	}

	private void Update()
	{
		float ScrollWheelChange = Input.GetAxis("Mouse ScrollWheel");           //This little peece of code is written by JelleWho https://github.com/jellewie
		if (ScrollWheelChange != 0){                                            //If the scrollwheel has changed
			float R = ScrollWheelChange * 15;                                   //The radius from current camera
			float PosX = Camera.main.transform.eulerAngles.x + 90;              //Get up and down
			float PosY = -1 * (Camera.main.transform.eulerAngles.y - 90);       //Get left to right
			PosX = PosX / 180 * Mathf.PI;                                       //Convert from degrees to radians
			PosY = PosY / 180 * Mathf.PI;                                       //^
			float X = R * Mathf.Sin(PosX) * Mathf.Cos(PosY);                    //Calculate new coords
			float Z = R * Mathf.Sin(PosX) * Mathf.Sin(PosY);                    //^
			float Y = R * Mathf.Cos(PosX);                                      //^
			float CamX = Camera.main.transform.position.x;                      //Get current camera postition for the offset
			float CamY = Camera.main.transform.position.y;                      //^
			float CamZ = Camera.main.transform.position.z;                      //^
			Camera.main.transform.position = new Vector3(CamX + X, CamY + Y, CamZ + Z);//Move the main camera
		}
	}
	private void Start()
	{
		inputReader = GameManager.Instance.playerInputReader;
		Cursor.visible = false;
	}
	private void OnDisable()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
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
}