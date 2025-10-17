using UnityEngine;

public interface Iinteractable
{
    void Interact();
}

public class Objects : MonoBehaviour,Iinteractable
{
    [SerializeField]protected bool CanInteract = true;

     public virtual void Interact()
     {
        if (CanInteract)
        {
            Debug.Log("interactuaste");
        }
     }

}
