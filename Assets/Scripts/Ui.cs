
using DamageNumbersPro;
using DamageNumbersPro.Demo;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Ui : MonoBehaviour
{
    public static Ui instance;

    [Header("Referencias UI")]
    [SerializeField] private Image leftBar;
    [SerializeField] private Image rightBar;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Configuración")]
    [SerializeField] private float maxBarWidth = 200f;
    [SerializeField] private float barHeight = 20f;
    [SerializeField] private float fadeSpeed = 2f;

    private TimeSlow timeSlow;
    private bool isActive = false;
    private float currentTime = 0f;
    private float totalDuration = 0f;

    [SerializeField] private GameObject cubeHighlightPrefab;
    private void Start()
    {
        timeSlow = TimeSlow.instance;
        if (canvasGroup != null)
            canvasGroup.alpha = 0f;
        SetBarsFill(1f);
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (timeSlow != null && timeSlow.isTimeSlowed)
        {
            if (!isActive)
            {
                isActive = true;
                totalDuration = timeSlow.GetCurrentDuration();
                currentTime = 0f;
                ShowUI();
            }

            currentTime += Time.unscaledDeltaTime;
            float progress = Mathf.Clamp01(currentTime / totalDuration);
            SetBarsFill(1f - progress);
        }
        else if (isActive)
        {
            isActive = false;
            HideUI();
        }


    }
    private void SetBarsFill(float fillAmount)
    {
        if (leftBar != null)
            leftBar.fillAmount = fillAmount;

        if (rightBar != null)
            rightBar.fillAmount = fillAmount;
    }
    private void ShowUI()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
        }
    }
    private void HideUI()
    {
        if (canvasGroup != null)
        {
            StartCoroutine(FadeOutUI());
        }
    }
    private IEnumerator FadeOutUI()
    {
        if (canvasGroup != null)
        {
            float fadeProgress = 1f;
            while (fadeProgress > 0f)
            {
                fadeProgress -= Time.unscaledDeltaTime * fadeSpeed;
                canvasGroup.alpha = fadeProgress;
                yield return null;
            }
            canvasGroup.alpha = 0f;
        }
    }
    public void SpawnDamageNumber(int value, Transform position)
    {
    }




}
