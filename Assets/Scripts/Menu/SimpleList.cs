using UnityEngine;

public class SimpleList : MonoBehaviour
{
    [SerializeField] private Hojas hojasRef;
    private Node head;
    private Node current;
    private bool firstClick = true;

    void Start()
    {
        CreateList();

        if (head != null)
        {
            current = head;
            current.hoja.SetActive(false); 
        }
    }

    void CreateList()
    {
        GameObject[] hojas = hojasRef.GetHojas();
        Node prev = null;

        for (int i = 0; i < hojas.Length; i++)
        {
            Node nuevo = new Node(hojas[i]);
            hojas[i].SetActive(false);

            if (head == null)
                head = nuevo;
            else
                prev.next = nuevo;

            prev = nuevo;
        }
    }

    public void NextPage()
    {
        if (current == null) return;

        if (firstClick)
        {
            current.hoja.SetActive(true);
            firstClick = false;
            return;
        }

        current.hoja.SetActive(false);

        if (current.next != null)
        {
            current = current.next;
            current.hoja.SetActive(true);
        }
    }
    public void ResetList()
    {
        firstClick = true;
        current = head;

        GameObject[] hojas = hojasRef.GetHojas();
        foreach (GameObject hoja in hojas)
        {
            hoja.SetActive(false);
        }
    }
}
