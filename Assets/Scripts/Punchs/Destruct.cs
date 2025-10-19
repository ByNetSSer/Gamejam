using DamageNumbersPro;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Destruct : Objects
{
    [SerializeField] int Health = 3;
    [SerializeField] int current;

    [SerializeField] GameObject destroy;
    [SerializeField] GameObject drops;
    [SerializeField] int dropcount;
    [SerializeField] float force;
    [SerializeField] DamageNumber prefab;
    [SerializeField] GameObject particle;
    [SerializeField] AudioClip audio;
    private void Start()
    {

        current = Health;
    }
    public override void Interact()
    {


        if (!CanInteract) return;
        //Recibe Accion
        current -= 1;
        if (current < 0)
        {
            DestroyObject();
        }

    }
    public void Interact(int damage)
    {
        if (!CanInteract) return;
        //recibe daño///////////////////////////
        current -= damage;
        if (prefab != null)
        {
            DamageNumber damageNumber = prefab.Spawn(transform.position, damage);
        }
        if (current < 0)
        {
            DestroyObject();
        }
    }
    public void DestroyObject()
    {
        if (!CanInteract) return;
        //al ser destruido//////////////////////////
        CanInteract = false;
        if (destroy != null)
        {
            Instantiate(destroy, transform.position, Quaternion.identity);
        }
        TimeSlow.instance.ActivateTimeSlow(0.2f);
        if (Combo.Instance != null)
        {
            //puntosadicionales
           // Combo.Instance.AddScore(); // Puntos base
            Combo.Instance.Registr();
        }
        if(particle != null)
        Instantiate(particle,transform,true);

        GenerateCircleDrops();
       
        Destroy(gameObject);
    }
    private void GenerateCircleDrops()
    {
        if (drops == null || dropcount == 0) return;

        for (int i = 0; i < dropcount; i++)
        {
            float angle = i * (360f / dropcount);

            if (drops != null)
            {
                GameObject drop = Instantiate(drops, transform.position, Quaternion.identity);
                ApplyRadialForce(drop, angle);
            }
        }
    }
    private void ApplyRadialForce(GameObject drop, float angle)
    {
        Rigidbody rb = drop.GetComponent<Rigidbody>();
        if (rb != null)
        {
            float angleRad = angle * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angleRad), 0f, Mathf.Sin(angleRad));
            rb.AddForce(direction * force, ForceMode.Impulse);
            rb.AddForce(Vector3.up * (force * 0.5f), ForceMode.Impulse);
        }
    }
}
