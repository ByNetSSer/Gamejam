using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

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
            sfxSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public void UpdateVolumes()
    {
        musicSource.volume = Mathf.Pow(musicVolume, 2f);
        sfxSource.volume = Mathf.Pow(sfxVolume, 2f);
    }
    public AudioClip GetSFXClip(int index)
    {
        return sfxClips[index];
    }
}
