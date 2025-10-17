using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
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
    [SerializeField] GameObject prefabEx;
    //tiempo
    private TimeSlow timeSlow;

    [SerializeField] private float explosionRadius = 8f;
    [SerializeField] private float explosionForce = 1200f;

    private void Start()
    {
        timeSlow = TimeSlow.instance;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
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
            //activarexplocion()
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

        if (Random.Range(0f,1f) < 0.5f)
        {
            current = "Trick";
        }
        else
        {
            current = "Treat";
        }
        if (timeSlow != null)
            timeSlow.ActivateTimeSlowForAbility(duration);
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
    IEnumerator AbilityTimer()
    {
        yield return new WaitForSeconds(duration);

        // Si se acaba el tiempo sin éxito
        if (!abilitysucces)
        {
            ability = false;
            Debug.Log("⏰ Se acabó el tiempo de la habilidad.");
        }
    }
    void ActivateExplosion()
    {
        if (prefabEx != null)
        {
            // Crear la explosión en la posición del jugador
            GameObject explosion = Instantiate(prefabEx, transform.position, Quaternion.identity);

            // Configurar la explosión
            Explosition explosionController = explosion.GetComponent<Explosition>();
            if (explosionController != null)
            {
                explosionController.TriggerExplosion(transform.position, explosionForce, explosionRadius);
            }
        }
        else
        {
            Debug.LogWarning("No hay prefab de explosión asignado");
        }
    }
}
