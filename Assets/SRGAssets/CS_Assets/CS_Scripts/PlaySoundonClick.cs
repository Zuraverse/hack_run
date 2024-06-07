using UnityEngine;
using UnityEngine.UI;

public class PlaySoundOnClick : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component

    // This method will be called when the button is clicked
    public void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play(); // Play the assigned sound
        }
        else
        {
            Debug.LogError("AudioSource is not assigned.");
        }
    }
}
