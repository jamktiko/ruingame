
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CameraSettingsChanger : MonoBehaviour
{
    public CameraSettings _cameraData;
    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivityValueText;
    private void Awake()
    {
        _cameraData = Resources.Load<CameraSettings>("CameraSettings");
        sensitivitySlider.value = _cameraData.CameraSensitivity;
        sensitivityValueText.text = _cameraData.CameraSensitivity.ToString();
        sensitivitySlider.onValueChanged.AddListener (delegate {ChangeCameraSensitivity();});
    }

    public void ChangeCameraSensitivity()
    {
        _cameraData.CameraSensitivity = sensitivitySlider.value;
        sensitivityValueText.text = _cameraData.CameraSensitivity.ToString();
    }

}
