using UnityEngine;
using UnityEngine.SceneManagement;

public class FindAndActivate : MonoBehaviour
{
    public GameObject myGameObject;

    void Start()
    {
        // Mark the GameObject as DontDestroyOnLoad
        DontDestroyOnLoad(gameObject);
    }

    public void ActivateCanvas(){
        myGameObject.SetActive(true);
    }


    void Update()
    {
        // Deactivate the GameObject when the 'D' key is pressed
        if (Input.GetKeyDown(KeyCode.D))
        {
            myGameObject.SetActive(false);
        }

        // Activate the GameObject when the 'A' key is pressed
        if (Input.GetKeyDown(KeyCode.A))
        {
            myGameObject.SetActive(true);
        }

        // Load a new scene when the 'L' key is pressed
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("MyScene");
        }
    }
}