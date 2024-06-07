using UnityEngine;
using UnityEngine.UI;

public class SoundToggleButton : MonoBehaviour
{
    public Button toggleButton; // Connect this in the inspector

    private void Start()
    {
        toggleButton.onClick.AddListener(OnToggleButtonClicked);
        UpdateButtonLabel();
    }

    private void OnToggleButtonClicked()
    {
        SoundManager.Instance.ToggleMute();
        UpdateButtonLabel();
    }

    private void UpdateButtonLabel()
    {
        var isMuted = SoundManager.Instance.IsMuted();
        toggleButton.GetComponentInChildren<Text>().text = isMuted ? "Unmute" : "Mute";
    }
}
