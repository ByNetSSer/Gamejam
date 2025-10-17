using UnityEngine;

public class Arms : MonoBehaviour
{
    public Fist left;
    public Fist right;
    public Animator LeftAnim;
    public Animator RightAnim;

    public Fist GetFist(string type)
    {
        Debug.Log("call ");
        if (type == "Trick" && !left.EsActive)
        {
            LeftAnim.SetTrigger("iz");
            return left;
        }
        else if (type == "Treat" && !right.EsActive)
        {
            RightAnim.SetTrigger("dr");
            return right;
        }
        return null;
    }
    public void SpawnLeftFist()
    {
        if (left != null)
            left.punch();
    }

    public void SpawnRightFist()
    {
        if (right != null)
            right.punch();
    }
}
