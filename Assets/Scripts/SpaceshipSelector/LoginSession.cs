using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginSession : MonoBehaviour
{
    public GameObject objectToPersist;

        void Awake()
        {
            GameObject parent = new GameObject("Persisting Object");
            objectToPersist.transform.SetParent(parent.transform);
            DontDestroyOnLoad(parent);
        }

}
