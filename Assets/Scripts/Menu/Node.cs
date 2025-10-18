using UnityEngine;

public class Node
{
    public GameObject hoja; 
    public Node next;

    public Node(GameObject hojaObj)
    {
        hoja = hojaObj;
        next = null;
    }
}
