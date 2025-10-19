using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] AudioClip AudioDestruc;
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CollecItem();
        }
    }

        
    
    private void CollecItem()
    {

        // Cuando recoge un caramelo /////////////////////////
        if (Combo.Instance != null && Combo.Instance.CurrentCombo > 0)
        {
            Combo.Instance.RegisterCandyDuringCombo(value);
        }
        else
        {
            GameManager.CollectItem(value);
        }

        // Usar el AudioManager exclusivo para monedas
        if (CoinSound.Instance != null)
        {
            if (AudioDestruc == null)
                CoinSound.Instance.PlayCoinSound(); 
            else
                CoinSound.Instance.PlayCoinSound(AudioDestruc); 
        }

        Destroy(this.gameObject);
    }
}
