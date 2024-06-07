using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreScene : MonoBehaviour
{

    public GameObject[] DontDestroy;
    public void ShowScoreScene()
    {
        SceneManager.LoadScene("Leaderboard", LoadSceneMode.Additive);
    }


    public void BackToMain()
    {
 
        SceneManager.LoadScene("SRGMenu");

    }


    public void Home()
    {
        SceneManager.LoadScene("ConnectWallet");
    }
    public void NFTScene()
    {
        SceneManager.LoadScene("HovershipSelector");
    }


    public void TestScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
