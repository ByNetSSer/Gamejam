using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManagers : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("Menu"); 
    }
    public void Exit()
    {
        Application.Quit();
    }
}
