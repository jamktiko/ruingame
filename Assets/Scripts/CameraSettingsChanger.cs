
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CameraSettingsChanger : MonoBehaviour
{
    public CameraSettings _cameraData;
    public CameraSettings _defaultCameraData;
    [Header("Sensitivity")]
    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivityValueText;
    [Header("Invert")] 
    public Toggle invertXToggle;
    public Toggle invertYToggle;
    
    
    private void Awake()
    {
        _cameraData = Resources.Load<CameraSettings>("CameraSettings");
        _defaultCameraData = Resources.Load<CameraSettings>("DefaultCameraSettings");
        sensitivitySlider.value = _cameraData.CameraSensitivity;
        sensitivityValueText.text = _cameraData.CameraSensitivity.ToString();
        sensitivitySlider.onValueChanged.AddListener (delegate {ChangeCameraSensitivity();});
        invertXToggle.onValueChanged.AddListener(delegate { ChangeAxisInvertX();});
        invertYToggle.onValueChanged.AddListener(delegate { ChangeAxisInvertY();});
        UpdateXToggle();
        UpdateYToggle();
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
    private void UpdateXToggle()
    {
        invertXToggle.isOn = _cameraData.invertAxisX;
    }
    private void UpdateYToggle()
    {
        invertYToggle.isOn = _cameraData.invertAxisY;
    }
    public void ChangeAxisInvertX()
    {
        _cameraData.invertAxisX = invertXToggle.isOn;
    }

    public void ChangeAxisInvertY()
    {
        _cameraData.invertAxisY = invertYToggle.isOn;
    }

    private void UpdateDefaults()
    {
        UpdateSensitivity();
        UpdateXToggle();
        UpdateYToggle();
    }
    public void RestoreDefaults()
    {
        _cameraData.CameraSensitivity = _defaultCameraData.CameraSensitivity;
        _cameraData.invertAxisX = _defaultCameraData.invertAxisX;
        _cameraData.invertAxisY = _defaultCameraData.invertAxisY;
        UpdateDefaults();
    }

    private void OnDisable()
    {
        sensitivitySlider.onValueChanged.RemoveAllListeners();
        
    }
}
