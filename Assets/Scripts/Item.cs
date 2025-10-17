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

        GameManager.CollectItem(value);
       // SpawnDamageNumber();
        //GameManager.instance.spawn(value,this.transform.position);
        Destroy(this.gameObject);
    }
    private void SpawnDamageNumber()
    {
        if (Ui.instance != null)
        {
            Ui.instance.SpawnDamageNumber(value, transform);
        }
        else
        {
           
        }
    }

}
