using DamageNumbersPro;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField] CameraRotation camerarotation;

    [SerializeField] private int life;
    [SerializeField] private int score;
    [SerializeField] Arms Arms;
    [SerializeField] private bool ability = false;
    [SerializeField] string current = "";
    [SerializeField] float duration = 2f;
    [SerializeField] bool abilitysucces=false;
    [SerializeField] GameObject prefabPArticle;
    [SerializeField] GameObject prefabEx;
    [SerializeField] AudioClip soundExplosition;

    [SerializeField] DamageNumberGUI TextH;


    [SerializeField] DamageNumberGUI options;

    [SerializeField] DamageNumberGUI optionsB;

    [SerializeField] DamageNumberGUI Slow;
    [SerializeField] RectTransform spawn;

    [SerializeField] RectTransform middle;
    [SerializeField] RectTransform rightTR;
    [SerializeField] RectTransform leftTR;
    //tiempo
    private TimeSlow timeSlow;

    [SerializeField] float explosionRadius = 8f;
    [SerializeField] float explosionForce = 1200f;
    [SerializeField] float cooldown = 5f;
    [SerializeField] Image vooldownimage;
   [SerializeField] bool iscooldown = false;
    [SerializeField]float currentcoldown = 0;

    private void Start()
    {
        timeSlow = TimeSlow.instance;
        if (vooldownimage != null)
        {
            vooldownimage.fillAmount = 0f;
        }
    }
    private void Update()
    {
        if (iscooldown)
        {
            currentcoldown -= Time.deltaTime;
            if (vooldownimage != null)
            {
                vooldownimage.fillAmount = currentcoldown / cooldown;
            }
            if (currentcoldown <= 0f)
            {
                iscooldown = false;
                currentcoldown = 0f;
                if (vooldownimage != null)
                {
                    vooldownimage.fillAmount = 0f;
                }
            }
        }
    }
    public void OnPressHability(InputAction.CallbackContext context)
    {
        if (context.performed && !iscooldown)
        {
            Slow.SpawnGUI(spawn,Vector2.zero);
            activate();
        }
    }
    public void OnmousebuttonLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            trypunch("Trick");
            
        }
    }
    public void OnMouseButtonRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            trypunch("Treat");
 
        }
    }
    public void trypunch(string ToT)
    {
        if (!ability)
        {
            Fist stay = Arms.GetFist(ToT);
            return;
        }
        if (CheckAbility(ToT))
        {
            abilitysucces = true;
            ability = false;
            if(timeSlow != null)
            {
                timeSlow.StopTimeSlow();
            }
            Debug.Log("Correcto");
            ActivateExplosion();
        }
        else
        {
            Debug.Log("Incorrecto");
            ability = false;
            if (timeSlow != null)
                timeSlow.StopTimeSlow();
        }
    }
    void activate()
    {
        if (ability) return;
        ability = true;
        abilitysucces = false;
        StartCooldown();
        if (Random.Range(0f,1f) < 0.5f)
        {
            current = "Trick";
            DamageNumber gen = TextH.SpawnGUI(middle, Vector2.zero);
            gen.topText = current;


            DamageNumber rightk = options.SpawnGUI(rightTR, Vector2.zero);
            rightk.topText = "Treat";
            DamageNumber leftk = optionsB.SpawnGUI(leftTR, Vector2.zero);
            leftk.topText = "Trick";
        }
        else
        {
            current = "Treat";
            DamageNumber gen = TextH.SpawnGUI(middle, Vector2.zero);
            gen.topText = current;

            DamageNumber rightt = optionsB.SpawnGUI(rightTR, Vector2.zero);
            rightt.topText = "Treat";
            DamageNumber leftt = options.SpawnGUI(leftTR, Vector2.zero);
            leftt.topText = "Trick";
        }

        if (timeSlow != null)
            timeSlow.ActivateTimeSlow(duration);
        StartCoroutine(AbilityTimer());
    }
    bool CheckAbility(string punchType)
    {

        if (current == "Trick" && punchType == "Treat")
            return true;
        if (current == "Treat" && punchType == "Trick")
            return true;

        return false;
    }
    void StartCooldown()
    {
        iscooldown = true;
        currentcoldown = cooldown;

        if (vooldownimage != null)
        {
            vooldownimage.fillAmount = 1f; // Lleno al inicio
        }
    }
    IEnumerator AbilityTimer()
    {
        yield return new WaitForSeconds(duration);
        if (!abilitysucces)
        {
            ability = false;
            //murio la habilidad
        }
    }
    void ActivateExplosion()
    {
        if (prefabEx != null)
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFX(soundExplosition);
            Instantiate(prefabPArticle,this.transform);
            PunchManager.Instance.CreateSpecialExplosion(this.gameObject.transform.position);
        }
    }
}
