using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControllManager : MonoBehaviour
{

    public void ChangeScene(string sceneName)
    {
        GameObject MusicSource = GameObject.Find("MusicSource");
        Destroy(MusicSource);
        SceneManager.LoadScene(sceneName);
    }
}
