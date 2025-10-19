using UnityEngine;
using UnityEngine.Audio;

public class CoinSound : MonoBehaviour
{
    public static CoinSound Instance;

    [Header("Coin Sound Settings")]
    public AudioClip defaultCoinSound;
    [Range(0f, 1f)]
    public float volume = 0.8f;

    [Header("Combo Pitch Settings")]
    [SerializeField] float minPitch = 0.8f;
    [SerializeField] float maxPitch = 1.8f;
    [SerializeField] int maxComboForPitch = 30;

    private AudioSource audioSource;
    private bool isInitialized = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudio();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeAudio()
    {
        if (isInitialized) return;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.volume = volume;

        // CONFIGURACIÓN IMPORTANTE: Usar el mismo SFX Mixer Group
        if (AudioManager.Instance != null)
        {
            AudioMixerGroup sfxMixerGroup = AudioManager.Instance.GetSFXMixerGroup();
            if (sfxMixerGroup != null)
            {
                audioSource.outputAudioMixerGroup = sfxMixerGroup;
            }
        }

        // Configuración optimizada para sonidos de monedas
        audioSource.bypassEffects = true;
        audioSource.bypassListenerEffects = true;
        audioSource.bypassReverbZones = true;
        audioSource.priority = 0;
        audioSource.spatialBlend = 0f;

        isInitialized = true;
    }

    // MÉTODO PRINCIPAL - SOBREESCRIBE EL SONIDO ANTERIOR CON PITCH DINÁMICO
    public void PlayCoinSound()
    {
        if (!isInitialized) InitializeAudio();
        if (defaultCoinSound == null)
        {
            Debug.LogWarning("Default coin sound no asignado");
            return;
        }

        PlaySoundInternal(defaultCoinSound);
    }

    public void PlayCoinSound(AudioClip customSound)
    {
        if (!isInitialized) InitializeAudio();
        if (customSound == null)
        {
            PlayCoinSound();
            return;
        }

        PlaySoundInternal(customSound);
    }

    private void PlaySoundInternal(AudioClip clip)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        float dynamicPitch = CalculateDynamicPitch();
        audioSource.pitch = dynamicPitch;
        audioSource.PlayOneShot(clip);
    }

    private float CalculateDynamicPitch()
    {
        if (Combo.Instance == null) return 1.0f;

        int currentCombo = Combo.Instance.CurrentCombo;

        if (currentCombo <= 1) return 1.0f;

        float comboProgress = Mathf.Clamp01((float)currentCombo / maxComboForPitch);
        float pitch = minPitch + (maxPitch - minPitch) * comboProgress;

        return Mathf.Clamp(pitch, minPitch, maxPitch);
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
