using UnityEngine;

public class PunchManager : MonoBehaviour
{
    public static PunchManager Instance;
    [SerializeField] private GameObject explosionPrefab;

    private void Awake()
    {
        // Implementación del Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void CreateExplosion(Vector3 position, float force, float radius, int damage)
    {
        if (explosionPrefab == null) return;


        GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        Explosition explosionController = explosion.GetComponent<Explosition>();

        if (explosionController != null)
        {
            explosionController.TriggerExplosion(position, force, radius, damage);
        }
    }
    public void CreatePunchExplosion(Vector3 position)
    {
        CreateExplosion(position, 20f, 0.8f, 1);
    }
    public void CreateSpecialExplosion(Vector3 position)
    {
        CreateExplosion(position, 90f, 5f, 3);
    }
    public void CreateHeavyExplosion(Vector3 position, int customDamage = 5)
    {
        CreateExplosion(position, 1500f, 8f, customDamage);
    }
    public void CreateDirectionalExplosion(Vector3 origin, Vector3 direction, float distance, float force, float radius, int damage)
    {
        Vector3 explosionPosition = origin + direction.normalized * distance;
        CreateExplosion(explosionPosition, force, radius, damage);
    }
}
