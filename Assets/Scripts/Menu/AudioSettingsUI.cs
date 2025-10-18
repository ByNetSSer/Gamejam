using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    void Start()
    {
        musicSlider.value = AudioManager.Instance.musicVolume;
        sfxSlider.value = AudioManager.Instance.sfxVolume;

        musicSlider.onValueChanged.AddListener(OnMusicChange);
        sfxSlider.onValueChanged.AddListener(OnSFXChange);
    }

    void OnMusicChange(float value)
    {
        AudioManager.Instance.musicVolume = Mathf.RoundToInt(value);
        AudioManager.Instance.UpdateVolumes();
    }

    void OnSFXChange(float value)
    {
        AudioManager.Instance.sfxVolume = Mathf.RoundToInt(value);
        AudioManager.Instance.UpdateVolumes();
    }
}
