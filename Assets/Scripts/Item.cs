using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int value;

    //caramelos,puntaje
    //items collecionables
    //powerups
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollecItem();
        }
    }
    private void CollecItem()
    {

      // 
        if (Combo.Instance != null && Combo.Instance.CurrentCombo > 0)
        {
            Combo.Instance.RegisterCandyDuringCombo(value);
        }
        else
        {
            GameManager.CollectItem(value);
        }
        Destroy(this.gameObject);
    }
}
