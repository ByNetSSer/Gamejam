using System.Security;
using UnityEngine;

public class Lever : Objects
{
    [SerializeField] bool isactive = false;
    [SerializeField] GameObject[] activate;
    [SerializeField] Animator animator;
    //animator

    public override void Interact()
    {
        if (isactive) return;

        if (CanInteract)
          onLever();

    }
    private void onLever()
    {
        isactive = !isactive;
        foreach (GameObject obj in activate)
        {
            if (obj != null)
                obj.SetActive(isactive);

            //su animator en cada uno
        }
    }
}
