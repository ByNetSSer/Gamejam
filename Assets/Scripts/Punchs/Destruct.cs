using UnityEngine;

public class Destruct : Objects
{
    [SerializeField] int Health = 3;
    [SerializeField] int current;

    [SerializeField] GameObject destroy;
    [SerializeField] GameObject drops;
    [SerializeField] int dropcount;
    [SerializeField] float force;

    private void Start()
    {

        current = Health;
    }
    public override void Interact()
    {
        if (!CanInteract) return;
        current -= 1;
        if (current < 0)
        {
            DestroyObject();
        }

    }
    public void Interact(int damage)
    {
        if (!CanInteract) return;
        current -= damage;
        if (current < 0)
        {
            DestroyObject();
        }
    }
    public void DestroyObject()
    {
        if (!CanInteract) return;
        CanInteract = false;
        if (destroy != null)
        {
            Instantiate(destroy, transform.position, Quaternion.identity);
        }
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
