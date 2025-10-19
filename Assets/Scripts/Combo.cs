using DamageNumbersPro;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Combo : MonoBehaviour
{
    [SerializeField]RectTransform basetransform;
    [SerializeField] RectTransform candytransform;
    public static Combo Instance;
    [SerializeField] int currentCombo=0;
    [SerializeField] float comboTime = 4.0f;
    [SerializeField] float currenttime = 0f;
    [SerializeField] string comboWord = "";
    [SerializeField] int candiesDuringCombo = 0;
    [SerializeField] float candyMultiplier = 1f;

    [SerializeField] DamageNumberGUI ScoreNumber;

    [SerializeField] DamageNumberGUI Candynumber;
  // [SerializeField] DamageNumber candypermanente;

   // [SerializeField] TextMeshProUGUI ComboText;
    [SerializeField] TextMeshProUGUI CombowordText;
    [SerializeField] Image TimeBar;
    [SerializeField] TextMeshProUGUI CandyMultiplierText;
    [SerializeField] AudioClip soundFinishcombo;
    private float width;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        } 
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        if (TimeBar != null)
            width = TimeBar.rectTransform.sizeDelta.x;
        UpdateUI();
    }
    private void Update()
    {
        UpdateComboTimeBar();
        if (Time.time - currenttime > comboTime && currentCombo > 0)
        {
            Debug.Log("termino el combo");
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFX(soundFinishcombo);
            AddFinalComboScore();
            ResetCombo();
           
        }
    }
    private void AddFinalComboScore()
    {
        if (candiesDuringCombo > 0) // Solo sumar si hay dulces
        {

            float candyMultiplierTotal = (candiesDuringCombo * candyMultiplier);
            int comboBonus = Mathf.RoundToInt(currentCombo * candyMultiplierTotal);

           // score += comboBonus;

            if (GameManager.instance != null)
            {
                Debug.Log("gamemanager");
                GameManager.instance.AddScore(comboBonus);
            }
        }
        else
        {

        }
    }
    public void Registr()
    {
        if (Time.time - currenttime <= comboTime)
        {
            //Añade combo///////////////////////
            currentCombo++;
            DamageNumber sc = Candynumber.SpawnGUI(basetransform,Vector2.zero);
            sc.leftText = "x ";
        }
        else
        {
            currentCombo = 1;
            candiesDuringCombo = 0;
        }
        currenttime = Time.time;
        WordCombo();
        UpdateUI();
    }
    public void RegisterCandyDuringCombo(int candyValue = 1)
    {
        if (currentCombo > 0)
        {
            //Añade puntaje//////////////////////
            candiesDuringCombo += candyValue;

            // Crear el número flotante
            DamageNumber ne = ScoreNumber.SpawnGUI(candytransform, Vector2.zero,1f);
            

            UpdateUI();
        }
    }

    private void ResetCombo()
    {
        currentCombo = 0;
        candiesDuringCombo = 0;

        comboWord = "";
        UpdateUI();
    }
    public void AddScore(int points)
    {
        //score += points;
        if (GameManager.instance != null)
        {
            GameManager.instance.AddScore(points);
        }
        UpdateUI();
    }
    private void UpdateComboTimeBar()
    {
        if (TimeBar == null) return;

        if (currentCombo > 0)
        {
            float timeRemaining = comboTime - (Time.time - currenttime);
            float fillAmount = Mathf.Clamp01(timeRemaining / comboTime);
            TimeBar.fillAmount = fillAmount;
            if (fillAmount > 0.5f)
                TimeBar.color = Color.green;
            else if (fillAmount > 0.2f)
                TimeBar.color = Color.yellow;
            else
                TimeBar.color = Color.red;
        }
        else
        {
            TimeBar.fillAmount = 0f;
        }
    }
    private void UpdateUI()
    {


        // Actualizar palabra del combo
        if (CombowordText != null)
        {
            if (currentCombo > 0)
                CombowordText.text = comboWord;
            else
                CombowordText.text = "";
        }

        // NUEVO: Actualizar texto del multiplicador de dulces
        if (CandyMultiplierText != null)
        {
            if (currentCombo > 0 && candiesDuringCombo > 0)
            {
                float totalMultiplier = (candiesDuringCombo * candyMultiplier);
                CandyMultiplierText.text = $"DULCES: {candiesDuringCombo} (x{totalMultiplier:F1})";


               

            }
            else
            {
                CandyMultiplierText.text = "";
            }
        }
    }

    // Propiedades para acceder desde otros scripts
    public int CurrentCombo => currentCombo;
    public int CandiesDuringCombo => candiesDuringCombo;
    public float CurrentMultiplier => (candiesDuringCombo * candyMultiplier);
    public int ReturnScoreActual()
    {
        float candyMultiplierTotal = (candiesDuringCombo * candyMultiplier);
        int comboBonus = Mathf.RoundToInt(currentCombo * candyMultiplierTotal);
        return comboBonus;
    }
    public void WordCombo()
    {
        switch (currentCombo)
        {
            case < 4:
                comboWord = "Kid!";
                break;
            case < 9:
                comboWord = "BadKid!";
                break;
            case < 17:
                comboWord = "MessMaker";
                break;
            case < 27:
                comboWord = "FurnitureLover";
                break;
            case < 36:
                comboWord = "RoomRuiner!";
                break;
            case < 48:
                comboWord = "HomeWrecker!";
                break;
            case < 56:
                comboWord = "UnstoppableBrat!";
                break;
            case < 66:
                comboWord = "UnstoppableBrat!";
                break;
            case < 78:
                comboWord = "DestructionBoy!";
                break;
            case < 87:
                comboWord = "DomesticDisaster!";
                break;
            case < 99:
                comboWord = "PropertyDamage!";
                break;
            default:
                comboWord = "TREATH!!!!!";
                break;
        }
    }
}
