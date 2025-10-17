using System.Collections;
using UnityEngine;

public class Explosition : MonoBehaviour
{
    [Header("Configuraci�n de Explosi�n")]
    [SerializeField] private float explosionRadius = 10f;
    [SerializeField] private float explosionForce = 1000f;
    [SerializeField] private float explosionGrowthSpeed = 8f;
    [SerializeField] private float explosionDuration = 0.3f;
    [SerializeField] private LayerMask affectedLayers = -1;
    [SerializeField] private string playerTag = "Player";

    private SphereCollider sphereCollider;
    private bool isExploding = false;
    private float currentRadius = 0f;
    private float explosionStartTime;
    private void Awake()
    {
        // Crear collider
        sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = 0f;

        // Hacer el objeto invisible
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null)
            renderer.enabled = false;
    }
    public void TriggerExplosion(Vector3 position, float force, float radius)
    {
        transform.position = position;
        explosionRadius = radius;
        explosionForce = force;

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

            // Actualizar tama�o de la esfera
            currentRadius = Mathf.Lerp(0f, explosionRadius, progress * explosionGrowthSpeed);
            sphereCollider.radius = currentRadius;

            yield return null;
        }

        // Finalizar explosi�n
        EndExplosion();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isExploding) return;

        // No afectar al jugador
        if (other.CompareTag(playerTag)) return;

        // Verificar si el objeto est� en las layers afectadas
        if (((1 << other.gameObject.layer) & affectedLayers) != 0)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null && !rb.isKinematic)
            {
                ApplyExplosionForce(rb, other.transform.position);
            }
        }
    }

    private void ApplyExplosionForce(Rigidbody rb, Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        float distance = Vector3.Distance(targetPosition, transform.position);

        // Calcular fuerza basada en la distancia (menor fuerza cuanto m�s lejos)
        float distanceFactor = 1f - (distance / explosionRadius);
        float actualForce = explosionForce * distanceFactor;

        // Aplicar fuerza
        rb.AddForce(direction * actualForce, ForceMode.Impulse);
    }

    private void EndExplosion()
    {
        isExploding = false;
        sphereCollider.radius = 0f;

        // Destruir el objeto despu�s de un tiempo
        Destroy(gameObject, 0.1f);
    }

    // M�todo para debug visual
    private void OnDrawGizmos()
    {
        if (isExploding)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, currentRadius);
        }
    }
}
