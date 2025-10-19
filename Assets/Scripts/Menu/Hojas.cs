using UnityEngine;
using UnityEngine.SceneManagement;

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
    public void SceneGame()
    {
        SceneManager.LoadScene("Demo_01");
    }

}
