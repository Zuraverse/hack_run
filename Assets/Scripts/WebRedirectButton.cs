using UnityEngine;

public class WebRedirectButton : MonoBehaviour
{
    [SerializeField]
    private string webLink;

    public void OpenWebLink()
    {
        Application.OpenURL(webLink);
    }
}
