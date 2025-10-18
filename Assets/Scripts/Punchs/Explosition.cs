using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public class Explosition : MonoBehaviour
{
    [Header("Configuración de Explosión")]
    [SerializeField] private float explosionRadius = 10f;
    [SerializeField] private float explosionForce = 1000f;
    [SerializeField] private float explosionGrowthSpeed = 8f;
    [SerializeField] private float explosionDuration = 0.3f;
    [SerializeField] private LayerMask affectedLayers = -1;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private int explosionDamage = 1;
    private SphereCollider sphereCollider;
    private bool isExploding = false;
    private float currentRadius = 0f;
    private float explosionStartTime;
    public List<GameObject> efectsobjects = new();
    private void Awake()
    {
        sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = 0f;
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null)
            renderer.enabled = false;
    }
    public void TriggerExplosion(Vector3 position, float force, float radius, int damage = 1)
    {
        transform.position = position;
        explosionRadius = radius;
        explosionForce = force;
        explosionDamage = damage;
        efectsobjects.Clear();
        gameObject.SetActive(true);
        isExploding = true;
        currentRadius = 0f;
        explosionStartTime = Time.time;

        StartCoroutine(ExplosionCoroutine());
    }

    private IEnumerator ExplosionCoroutine()
    {
        while (isExploding)
        {
            float elapsedTime = Time.time - explosionStartTime;
            float progress = elapsedTime / explosionDuration;

            if (progress >= 1f)
            {
                isExploding = false;
                break;
            }
            currentRadius = Mathf.Lerp(0f, explosionRadius, progress * explosionGrowthSpeed);
            sphereCollider.radius = currentRadius;

            yield return null;
        }
        EndExplosion();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isExploding) return;
        if (efectsobjects.Contains(other.gameObject) && other.CompareTag(playerTag)) return;
        if (((1 << other.gameObject.layer) & affectedLayers) != 0)
        {
            ApplyPhysicsForce(other);
            ApplyDestruction(other);
            efectsobjects.Add(other.gameObject);
        }
    }

    private void ApplyPhysicsForce(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null && !rb.isKinematic)
        {
            ApplyExplosionForce(rb, other.transform.position);
        }
    }

    private void ApplyDestruction(Collider other)
    {
        Iinteractable interactable = other.GetComponent<Iinteractable>();
        if (interactable != null)
        {
            Destruct destruct = other.GetComponent<Destruct>();
            if (destruct != null)
            {
                Debug.Log("hizo daño");
                destruct.Interact(explosionDamage);
            }
            else
            {
                Debug.Log("interactuo");
                interactable.Interact();
            }
            return;
        }
    }



    private void ApplyExplosionForce(Rigidbody rb, Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        float distance = Vector3.Distance(targetPosition, transform.position);
        float distanceFactor = 1f - (distance / explosionRadius);
        float actualForce = explosionForce * distanceFactor;
        rb.AddForce(direction * actualForce, ForceMode.Impulse);
    }

    private void EndExplosion()
    {
        isExploding = false;
        sphereCollider.radius = 0f;
        Destroy(gameObject, 0.1f);
    }

    // Método para debug visual
    private void OnDrawGizmos()
    {
        if (isExploding)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, currentRadius);
        }
    }
}
