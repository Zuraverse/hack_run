using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioMixer mainMixer; // Connect this to your Audio Mixer
    private const string VolumeParameter = "MasterVolume";
    private bool isMuted = false;

    private void Awake()
    {
        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object when changing scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        if (isMuted)
        {
            // Set volume to minimum (-80 dB is typically silence)
            mainMixer.SetFloat(VolumeParameter, -80f);
        }
        else
        {
            // Set volume back to normal (0 dB)
            mainMixer.SetFloat(VolumeParameter, 0f);
        }
    }

    public bool IsMuted()
    {
        return isMuted;
    }
}
