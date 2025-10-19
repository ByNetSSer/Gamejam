using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    void Start()
    {
        musicSlider.minValue = 0f;
        musicSlider.maxValue = 1f;
        sfxSlider.minValue = 0f;
        sfxSlider.maxValue = 1f;

        musicSlider.wholeNumbers = false;
        sfxSlider.wholeNumbers = false;

        musicSlider.value = AudioManager.Instance.musicVolume;
        sfxSlider.value = AudioManager.Instance.sfxVolume;

        musicSlider.onValueChanged.AddListener(OnMusicChange);
        sfxSlider.onValueChanged.AddListener(OnSFXChange);
    }

    void OnMusicChange(float value)
    {
        AudioManager.Instance.musicVolume = value;
        AudioManager.Instance.UpdateVolumes();
    }

    void OnSFXChange(float value)
    {
        AudioManager.Instance.sfxVolume = value;
        AudioManager.Instance.UpdateVolumes();
    }
}
