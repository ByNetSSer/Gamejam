using UnityEngine;

public class Hojas : MonoBehaviour
{
    [SerializeField] private GameObject[] hojas;
    [SerializeField] private SimpleList simple;

    public GameObject[] GetHojas()
    {
        return hojas;
    }
    public void NextPage()
    {
        simple.NextPage();
    }
    public void ResetPages()
    {
        simple.ResetList();
    }
}
