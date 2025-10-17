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
    public float Current;
    public static TimeSlow instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
        }
    }
    public void ActivateTimeSlowForAbility(float abilityDuration)
    {
        if (timeSlowCoroutine != null)
            StopCoroutine(timeSlowCoroutine);

        timeSlowCoroutine = StartCoroutine(TimeSlowForAbilityRoutine(abilityDuration));
    }
    private IEnumerator TimeSlowForAbilityRoutine(float abilityDuration)
    {
        isTimeSlowed = true;
        float elapsedTime = 0f;
        float startTimeScale = Time.timeScale;
        Current = abilityDuration;

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
        yield return new WaitForSecondsRealtime(abilityDuration);
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
    }
    public void StopTimeSlow()
    {
        if (timeSlowCoroutine != null)
        {
            StopCoroutine(timeSlowCoroutine);
            timeSlowCoroutine = null;
        }
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        isTimeSlowed = false;
    }
    public float GetCurrentDuration()
    {
        return Current;
    }
}
