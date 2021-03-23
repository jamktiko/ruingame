
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CameraSettingsChanger : MonoBehaviour
{
    public CameraSettings _cameraData;
    public CameraSettings _defaultCameraData;
    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivityValueText;
    private void Awake()
    {
        _cameraData = Resources.Load<CameraSettings>("CameraSettings");
        _defaultCameraData = Resources.Load<CameraSettings>("DefaultCameraSettings");
        sensitivitySlider.value = _cameraData.CameraSensitivity;
        sensitivityValueText.text = _cameraData.CameraSensitivity.ToString();
        sensitivitySlider.onValueChanged.AddListener (delegate {ChangeCameraSensitivity();});
    }

    public void ChangeCameraSensitivity()
    {
        _cameraData.CameraSensitivity = sensitivitySlider.value;
        sensitivityValueText.text = _cameraData.CameraSensitivity.ToString();
    }

    private void UpdateSensitivity()
    {
        sensitivitySlider.value = _cameraData.CameraSensitivity;
        sensitivityValueText.text = _cameraData.CameraSensitivity.ToString();
    }

    public void ChangeAxisInvert()
    {
        
    }

    public void RestoreDefaults()
    {
        _cameraData.CameraSensitivity = _defaultCameraData.CameraSensitivity;
        UpdateSensitivity();
    }
}
