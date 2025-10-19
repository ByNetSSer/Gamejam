using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivityUI : MonoBehaviour
{
    [SerializeField] private Slider sensitivitySlider;
    private const string SensitivityKey = "MouseSensitivity";
    private float defaultSensitivity = 2f;

    void Start()
    {
        sensitivitySlider.minValue = 0.1f;
        sensitivitySlider.maxValue = 5f;

        float savedSensitivity = PlayerPrefs.GetFloat(SensitivityKey, defaultSensitivity);
        savedSensitivity = Mathf.Clamp(savedSensitivity, 0.1f, 5f);

        sensitivitySlider.value = savedSensitivity;

        CameraRotation.SetSensitivity(savedSensitivity);

        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
    }

    private void OnSensitivityChanged(float newValue)
    {
        newValue = Mathf.Clamp(newValue, 0.1f, 5f); 
        PlayerPrefs.SetFloat(SensitivityKey, newValue);
        PlayerPrefs.Save();
        CameraRotation.SetSensitivity(newValue);
    }
}
