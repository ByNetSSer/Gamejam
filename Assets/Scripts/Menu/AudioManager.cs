using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Mixer Groups")]
    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;

    [Header(" Música")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameplayMusic;

    [Header(" Volumen (0 a 1)")]
    [Range(0f, 1f)] public float musicVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    [Header(" Efectos de Sonido (SFX)")]
    [SerializeField] private AudioClip[] sfxClips;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    private string currentSceneName = "";

    // Parámetros del mixer (deben coincidir con los del Audio Mixer)
    private const string MUSIC_VOLUME_PARAM = "MusicVolume";
    private const string SFX_VOLUME_PARAM = "SFXVolume";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.playOnAwake = true;
        sfxSource = gameObject.AddComponent<AudioSource>();

        // Asignar los mixer groups
        if (musicMixerGroup != null)
            musicSource.outputAudioMixerGroup = musicMixerGroup;

        if (sfxMixerGroup != null)
            sfxSource.outputAudioMixerGroup = sfxMixerGroup;

        musicSource.loop = true;
        musicSource.playOnAwake = false;
        sfxSource.playOnAwake = false;

        UpdateVolumes();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        PlayMusicForScene(currentSceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;
        musicSource.Stop();
        PlayMusicForScene(currentSceneName);
    }

    private void PlayMusicForScene(string sceneName)
    {
        AudioClip nextClip = null;

        if (sceneName == "Menu")
            nextClip = menuMusic;
        else if (sceneName == "Demo_01")
            nextClip = gameplayMusic;
       
        if (nextClip != null && musicSource.clip != nextClip)
        {
            musicSource.clip = nextClip;
            musicSource.Play();
        }
    }

    public void PlaySFX(int index)
    {
        if (index < 0 || index >= sfxClips.Length)
        {
            Debug.LogWarning($"Índice de SFX fuera de rango: {index}");
            return;
        }

        AudioClip clip = sfxClips[index];
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public void UpdateVolumes()
    {
        // Convertir volumen lineal (0-1) a decibelios (-80 a 0) para el mixer
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;

        // Aplicar al Audio Mixer
        if (audioMixer != null)
        {
            audioMixer.SetFloat(MUSIC_VOLUME_PARAM, LinearToDecibels(musicVolume));
            audioMixer.SetFloat(SFX_VOLUME_PARAM, LinearToDecibels(sfxVolume));
        }
    }
    private float LinearToDecibels(float linear)
    {
        if (linear <= 0f)
            return -80f; // Silencio
        return Mathf.Log10(linear) * 20f;
    }

    // Conversión de decibelios a volumen lineal (para sliders)
    private float DecibelsToLinear(float db)
    {
        return Mathf.Pow(10f, db / 20f);
    }
    public AudioClip GetSFXClip(int index)
    {
        return sfxClips[index];
    }
    public AudioMixerGroup GetSFXMixerGroup()
    {
        return sfxMixerGroup;
    }

    public AudioMixerGroup GetMusicMixerGroup()
    {
        return musicMixerGroup;
    }
}
