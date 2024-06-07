using UnityEngine;

public class FindAndDeactivate : MonoBehaviour
{
    public string objectName;

    void Start()
    {
        GameObject obj = GameObject.Find(objectName);
        if (obj != null)
        {
            obj.SetActive(false);
        }
    }


}
