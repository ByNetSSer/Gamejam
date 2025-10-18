using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header(" Música")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameplayMusic;

    [Header(" Volumen (1-10)")]
    [Range(1, 10)] public int musicVolume = 5;
    [Range(1, 10)] public int sfxVolume = 5;

    [Header(" Efectos de Sonido (SFX)")]
    [SerializeField] private AudioClip[] sfxClips;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    private string currentSceneName = "";

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
        sfxSource = gameObject.AddComponent<AudioSource>();

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
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public void UpdateVolumes()
    {
        musicSource.volume = Mathf.Clamp01(musicVolume / 10f);
        sfxSource.volume = Mathf.Clamp01(sfxVolume / 10f);
    }
}
