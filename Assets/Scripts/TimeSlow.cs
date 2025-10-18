using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements.Experimental;
using static System.TimeZoneInfo;

public class TimeSlow : MonoBehaviour
{
    [SerializeField] float slowtime = 0.1f;
    [SerializeField] float duration = 0.5f;
    [SerializeField] float transition = 0.1f;

    [SerializeField] public bool isTimeSlowed = false;
    [SerializeField] Coroutine timeSlowCoroutine;

    private int timeSlowRequests = 0;
    private float originalTimeScale = 1f;
    private float originalFixedDeltaTime = 0.02f;
    private float currentDuration;
    public float Current;
    public static TimeSlow instance;
    private bool isTransitioning = false;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            originalTimeScale = Time.timeScale;
            originalFixedDeltaTime = Time.fixedDeltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void OnDestroy()
    {
        if (isTimeSlowed)
        {

            ResetTimeScale();
           // Time.timeScale = 1f;
           // Time.fixedDeltaTime = 0.02f;
        }
    }
    public void ActivateTimeSlow(float abilityDuration)
    {
        timeSlowRequests++;
        if (isTimeSlowed)
        {
            // Opcional: extender la duración actual si lo deseas
            // Current = Mathf.Max(Current, abilityDuration);
            return;
        }

        if (timeSlowCoroutine != null)
            StopCoroutine(timeSlowCoroutine);

        timeSlowCoroutine = StartCoroutine(TimeSlowCount(abilityDuration));
    }
    private IEnumerator TimeSlowCount(float abilityDuration)
    {
        isTimeSlowed = true;
        isTransitioning = true;
        currentDuration = abilityDuration;

        // TRANSICIÓN ORIGINAL EXACTA - Entrada
        float elapsedTime = 0f;
        float startTimeScale = Time.timeScale;

        while (elapsedTime < transition)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float t = elapsedTime / transition;
            Time.timeScale = Mathf.Lerp(startTimeScale, slowtime, t);
            Time.fixedDeltaTime = 0.02f * Time.timeScale; 
            yield return null;
        }

        Time.timeScale = slowtime;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        isTransitioning = false;

        yield return new WaitForSecondsRealtime(abilityDuration);
        if (isTimeSlowed)
        {
            isTransitioning = true;

            // TRANSICIÓN ORIGINAL EXACTA - Salida
            elapsedTime = 0f;
            startTimeScale = Time.timeScale;

            while (elapsedTime < transition)
            {
                elapsedTime += Time.unscaledDeltaTime;
                float t = elapsedTime / transition;
                Time.timeScale = Mathf.Lerp(startTimeScale, 1f, t);
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                yield return null;
            }

            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
            isTimeSlowed = false;
            isTransitioning = false;
        }
    }
    public void StopTimeSlow()
    {
        timeSlowRequests = 0;

        if (timeSlowCoroutine != null)
        {
            StopCoroutine(timeSlowCoroutine);
            timeSlowCoroutine = null;
        }

        ResetTimeScale();
        isTimeSlowed = false;
    }
    private void ResetTimeScale()
    {
        Time.timeScale = originalTimeScale;
        Time.fixedDeltaTime = originalFixedDeltaTime;
    }
    public float GetCurrentDuration()
    {
        return Current;
    }
    public bool CanActivateTimeSlow()
    {
        return !isTimeSlowed || timeSlowRequests == 0;
    }
    public void DeactivateTimeSlow()
    {
        if (timeSlowRequests > 0)
            timeSlowRequests--;

        if (timeSlowRequests <= 0 && isTimeSlowed)
        {
            StopTimeSlow();
        }
    }
}
