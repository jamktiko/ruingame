
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
    [Header("Smoothing")] 
    public Slider xSmoothingSlider;
    public TextMeshProUGUI xSmoothingValueText;
    public Slider ySmoothingSlider;
    public TextMeshProUGUI ySmoothingValueText;
    [Header("Acceleration")]
    public Slider xAccelerationSlider;
    public TextMeshProUGUI xAccelerationValueText;
    public Slider yAccelerationSlider;
    public TextMeshProUGUI yAccelerationValueText;
    
    
    private void Awake()
    {
        _cameraData = Resources.Load<CameraSettings>("CameraSettings");
        _defaultCameraData = Resources.Load<CameraSettings>("DefaultCameraSettings");
        sensitivitySlider.onValueChanged.AddListener (delegate {ChangeCameraSensitivity();});
        invertXToggle.onValueChanged.AddListener(delegate { ChangeAxisInvertX();});
        invertYToggle.onValueChanged.AddListener(delegate { ChangeAxisInvertY();});
        xSmoothingSlider.onValueChanged.AddListener (delegate {ChangeXSmoothing();});
        ySmoothingSlider.onValueChanged.AddListener (delegate {ChangeYSmoothing();});
        xAccelerationSlider.onValueChanged.AddListener (delegate {ChangeXAcceleration();});
        yAccelerationSlider.onValueChanged.AddListener (delegate {ChangeYAcceleration();});

        UpdateValues();
    }

    public void ChangeCameraSensitivity()
    {
        _cameraData.CameraSensitivity = sensitivitySlider.value;
        sensitivityValueText.text = _cameraData.CameraSensitivity.ToString();
    }
    
    private void ChangeAxisInvertX()
    {
        _cameraData.invertAxisX = invertXToggle.isOn;
    }

    private void ChangeAxisInvertY()
    {
        _cameraData.invertAxisY = invertYToggle.isOn;
    }
    private void ChangeXSmoothing()
    { 
        _cameraData.XSmoothing = xSmoothingSlider.value;
       xSmoothingValueText.text = _cameraData.XSmoothing.ToString();
    }
    private void ChangeYSmoothing()
    { 
        _cameraData.YSmoothing = ySmoothingSlider.value;
        ySmoothingValueText.text = _cameraData.YSmoothing.ToString();
    }
    private void ChangeXAcceleration()
    { 
        _cameraData.XAcceleration = xAccelerationSlider.value;
        xAccelerationValueText.text = _cameraData.XAcceleration.ToString();
    }
    private void ChangeYAcceleration()
    { 
        _cameraData.YAcceleration = yAccelerationSlider.value;
        yAccelerationValueText.text = _cameraData.YAcceleration.ToString();
    }
    
    private void UpdateSensitivity()
    {
        sensitivitySlider.value = _cameraData.CameraSensitivity;
        sensitivityValueText.text = _cameraData.CameraSensitivity.ToString();
    }
    private void UpdateToggles()
    {
        invertXToggle.isOn = _cameraData.invertAxisX;
        invertYToggle.isOn = _cameraData.invertAxisY;
    }

    private void UpdateSmoothing()
    {
        xSmoothingSlider.value = _cameraData.XSmoothing;
        xSmoothingValueText.text = _cameraData.XSmoothing.ToString();
        ySmoothingSlider.value = _cameraData.YSmoothing;
        ySmoothingValueText.text = _cameraData.YSmoothing.ToString();
    }
    private void UpdateAcceleration()
    {
        xAccelerationSlider.value = _cameraData.XAcceleration;
        xAccelerationValueText.text = _cameraData.XAcceleration.ToString();
        yAccelerationSlider.value = _cameraData.YAcceleration;
        yAccelerationValueText.text = _cameraData.YAcceleration.ToString();
    }
    private void UpdateValues()
    {
        UpdateSensitivity();
        UpdateSmoothing();
        UpdateAcceleration();
        UpdateToggles();
    }
    public void RestoreDefaults()
    {
        _cameraData.CameraSensitivity = _defaultCameraData.CameraSensitivity;
        _cameraData.invertAxisX = _defaultCameraData.invertAxisX;
        _cameraData.invertAxisY = _defaultCameraData.invertAxisY;
        _cameraData.XSmoothing = _defaultCameraData.XSmoothing;
        _cameraData.XAcceleration = _defaultCameraData.XAcceleration;
        _cameraData.YSmoothing = _defaultCameraData.YSmoothing;
        _cameraData.YAcceleration= _defaultCameraData.YAcceleration;
        UpdateValues();
    }

}
