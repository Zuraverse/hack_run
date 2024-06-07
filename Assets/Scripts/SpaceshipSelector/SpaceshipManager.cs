using UnityEngine;

public class SpaceshipManager : MonoBehaviour
{
    public string playerTag = "Player"; // Tag of the player game object
    public Material[] jetMaterials; // Array of materials for the jet

    private int selectedMaterialIndex = 0; // Default material index

    void Start()
    {
        // Retrieve the selected material index from PlayerPrefs
        selectedMaterialIndex = PlayerPrefs.GetInt("SelectedMaterialIndex", 0);

        // Find the player game object using tag
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);

        if (playerObject != null)
        {
            // Find the JetRenderer component as a child of the player object
            Renderer jetRenderer = playerObject.GetComponentInChildren<Renderer>();

            // Apply the material to the jet model
            if (jetRenderer != null && jetMaterials.Length > selectedMaterialIndex)
            {
                jetRenderer.material = jetMaterials[selectedMaterialIndex];
            }
        }
    }
}
