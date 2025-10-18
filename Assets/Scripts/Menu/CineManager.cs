using UnityEngine;
using Unity.Cinemachine;

public class CineManager : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    [SerializeField] private CinemachineCamera[] cameras;
    [SerializeField] private Hojas hoja;
    void Start()
    {
        objects[0].SetActive(true);
        objects[1].SetActive(false);
        objects[2].SetActive(false);
        hoja.GetHojas()[0].SetActive(false);
        hoja.GetHojas()[1].SetActive(false);
        hoja.GetHojas()[2].SetActive(false);    
        hoja.GetHojas()[3].SetActive(false);    
        cameras[0].gameObject.SetActive(true);
        cameras[1].gameObject.SetActive(false);
        cameras[2].gameObject.SetActive(false);
        cameras[3].gameObject.SetActive(false);
    }
    public void Options()
    {
        objects[0].SetActive(false);
        objects[1].SetActive(true);
        cameras[0].gameObject.SetActive(false);
        cameras[1].gameObject.SetActive(true);
    }
    public void Records()
    {
        objects[0].SetActive(false);
        objects[2].SetActive(true);
        cameras[0].gameObject.SetActive(false);
        cameras[2].gameObject.SetActive(true);
    }
    public void Play()
    {
        hoja.ResetPages();
        objects[0].SetActive(false);
        cameras[0].gameObject.SetActive(false);
        cameras[3].gameObject.SetActive(true);
        hoja.NextPage();
    }
    public void Back()
    {
        if (objects[1].activeSelf)
        {
            objects[1].SetActive(false);
            objects[0].SetActive(true);
            cameras[1].gameObject.SetActive(false);
            cameras[0].gameObject.SetActive(true);
        }
        if (objects[2].activeSelf)
        {
            objects[2].SetActive(false);
            objects[0].SetActive(true);
            cameras[2].gameObject.SetActive(false);
            cameras[0].gameObject.SetActive(true);
        }
        if (objects[3].activeSelf)
        {
            objects[3].SetActive(false);
            objects[0].SetActive(true);
            cameras[3].gameObject.SetActive(false);
            cameras[0].gameObject.SetActive(true);
        }
    }
}
