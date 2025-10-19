using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Fist : MonoBehaviour
{
    public bool EsActive = false;
    public float Rencoil = 0.2f;
    public BoxCollider box;
    public Transform position;
    public void punch()
    {
        if (EsActive) return;
        //Golpea///////////////////
        EsActive = true;
        //general
        PunchManager.Instance.CreatePunchExplosion(position.position);
        StartCoroutine( rencoil());
        
    }
    IEnumerator rencoil()
    {
        yield return new WaitForSeconds(Rencoil);
        EsActive=false;
      
    }
    private void OnCollisionEnter(Collision collision)
    {
        Iinteractable interactable = collision.gameObject.GetComponent<Iinteractable>();
        Debug.Log("colicione");


        if (interactable != null)
        {
            Debug.Log("colicione con un interactable");
            Destruct destruct = collision.gameObject.GetComponent<Destruct>();
            if (destruct != null)
            {
                destruct.Interact(1); 
            }
            else
            {
                interactable.Interact();
            }
        }
    
    }
    private void OnTriggerEnter(Collider other)
    {
        Iinteractable interactable = other.gameObject.GetComponent<Iinteractable>();
        Debug.Log("trigger");


        if (interactable != null)
        {
            Debug.Log("trigger con un interactable");
            Destruct destruct = other.gameObject.GetComponent<Destruct>();
            if (destruct != null)
            {
                destruct.Interact(1);
            }
            else
            {
                interactable.Interact();
            }
        }
    }
}
