using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class CineManager : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    [SerializeField] private CinemachineCamera[] cameras;
    [SerializeField] private Hojas hoja;
    [SerializeField] private float fadeDuration = 1.0f;

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

        StartCoroutine(FadeIn(objects[0]));
    }

    public void Options()
    {
        StartCoroutine(SwitchWithFade(objects[0], objects[1]));
        cameras[0].gameObject.SetActive(false);
        cameras[1].gameObject.SetActive(true);
    }

    public void Records()
    {
        StartCoroutine(SwitchWithFade(objects[0], objects[2]));
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
            StartCoroutine(SwitchWithFade(objects[1], objects[0], cameras[1], cameras[0]));
        else if (objects[2].activeSelf)
            StartCoroutine(SwitchWithFade(objects[2], objects[0], cameras[2], cameras[0]));
        else if (objects[3].activeSelf)
            StartCoroutine(SwitchWithFade(objects[3], objects[0], cameras[3], cameras[0]));
    }
    private IEnumerator FadeIn(GameObject obj)
    {
        if (!obj) yield break;

        obj.SetActive(true);
        CanvasGroup cg = obj.GetComponent<CanvasGroup>();
        if (cg == null)
            cg = obj.AddComponent<CanvasGroup>();

        cg.alpha = 0;
        while (cg.alpha < 1f)
        {
            cg.alpha += Time.deltaTime / fadeDuration;
            yield return null;
        }
        cg.alpha = 1f;
    }

    private IEnumerator SwitchWithFade(GameObject from, GameObject to, CinemachineCamera camOff = null, CinemachineCamera camOn = null)
    {
        if (from) from.SetActive(false);
        if (camOff) camOff.gameObject.SetActive(false);
        if (camOn) camOn.gameObject.SetActive(true);

        yield return FadeIn(to);
    }

}
