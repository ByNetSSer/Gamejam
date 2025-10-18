using System.Security;
using UnityEngine;

public class Lever : Objects
{
    [SerializeField] bool isactive = false;
    [SerializeField] GameObject[] activate;
    [SerializeField] Animator animator;//propia palanca
    //animator

    public override void Interact()
    {
        Debug.Log("interactuaron con la palanca");
        if (isactive) return;

        if (CanInteract)
          onLever();

    }
    private void onLever()
    {
        //isactive = !isactive;
        foreach (GameObject obj in activate)
        {
            CanInteract = false; 
            if (obj != null)
            obj.GetComponent<Animator>().enabled = true;
            //su animator en cada uno
        }
    }
}
