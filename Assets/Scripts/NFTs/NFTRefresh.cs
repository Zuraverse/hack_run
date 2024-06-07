using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NFTRefresh : MonoBehaviour
{
    public string scriptName;
    public string functionName;
    // Start is called before the first frame update
    public void Refresnft()
    {
        // Find the script in the scene
        var script = FindObjectOfType(Type.GetType(scriptName));
        if (script != null)
        {
            // Call the function on the script
            script.GetType().GetMethod(functionName).Invoke(script, null);
        }
        
    }

}
