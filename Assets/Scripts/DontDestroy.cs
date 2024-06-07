using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static bool created = false;
    private static GameObject instance;

    public GameObject myGameObject;

    void Start()
    {
        ActivateCanvas();
        if (!created)
        {
            DontDestroyOnLoad(gameObject);
            instance = gameObject;
            created = true;
        }
        else if (instance != gameObject)
        {
            Destroy(gameObject);
        }
    }

    
    public void ActivateCanvas(){
        myGameObject.SetActive(true);
    }
}
