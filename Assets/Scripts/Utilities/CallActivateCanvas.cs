using UnityEngine;

public class CallActivateCanvas : MonoBehaviour
{
    public void Activate()
    {
        // Find the instance of the prefab in the scene
        GameObject prefabInstance = GameObject.Find("DontDestroyConnectionPersistent");

        // Get a reference to its DontDestroy component
        DontDestroy findAndActivate = prefabInstance.GetComponent<DontDestroy>();

        // Call the ActivateCanvas method
        findAndActivate.ActivateCanvas();
    }
}
