using UnityEngine;
using UnityEngine.SceneManagement;
using TunnelEffect;

public class TunnelPresetManager : MonoBehaviour
{
    // Array to hold all the presets you want to cycle through
    public TUNNEL_PRESET[] presetsToChooseFrom = {
        TUNNEL_PRESET.SpaceTravel,
        TUNNEL_PRESET.MagmaTunnel,
        TUNNEL_PRESET.MetalStructure,
        TUNNEL_PRESET.WaterTunnel,
        TUNNEL_PRESET.Twightlight,
        TUNNEL_PRESET.Chromatic
    };

    void Start()
    {
        // Get a random index within the range of the preset array
        int randomIndex = Random.Range(0, presetsToChooseFrom.Length);

        // Get the TunnelFX2 instance
        TunnelFX2 tunnel = TunnelFX2.instance;

        // Set the preset to the randomly chosen preset
        tunnel.preset = presetsToChooseFrom[randomIndex];

        SetPreset();
    }

    void Update()
    {
        //// Check if the scene is reloading
        //if (SceneManager.GetActiveScene().isLoaded)
        //{
        //    // Get a random index within the range of the preset array
        //    int randomIndex = Random.Range(0, presetsToChooseFrom.Length);

        //    // Get the TunnelFX2 instance
        //    TunnelFX2 tunnel = TunnelFX2.instance;

        //    // Set the preset to the randomly chosen preset
        //    tunnel.preset = presetsToChooseFrom[randomIndex];
        //}
    }

    void SetPreset()
    {
        if (SceneManager.GetActiveScene().isLoaded)
        {
            // Get a random index within the range of the preset array
            int randomIndex = Random.Range(0, presetsToChooseFrom.Length);

            // Get the TunnelFX2 instance
            TunnelFX2 tunnel = TunnelFX2.instance;

            // Set the preset to the randomly chosen preset
            tunnel.preset = presetsToChooseFrom[randomIndex];
        }
    }
}
