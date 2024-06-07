using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SpeedarSelector : MonoBehaviour
{
    public Renderer[] jetRenderers; // Array of Renderers for the jet models
    public Button[] materialButtons; // An array of buttons for selecting materials
    public Material[] jetMaterials; // An array of materials to apply to the jet

    public TextMeshProUGUI descriptionText; // Text component to display the description
    public float typewriterSpeed = 0.05f; // Speed at which the text is revealed

    public AudioSource typingAudioSource; // Audio source for typing sound
    public AudioClip typingAudioClip; // Typing sound clip

    private int currentMaterialIndex = 0; // The index of the currently selected material
    private string[] descriptions; // Array of descriptions for each material
    private Coroutine typewriterCoroutine; // Coroutine for typewriting effect

    // Key for PlayerPrefs
    private const string SelectedMaterialIndexKey = "SelectedMaterialIndex";

    void Start()
    {
        // Set the initial material
        currentMaterialIndex = PlayerPrefs.GetInt(SelectedMaterialIndexKey, 0);
        ApplyMaterial();


        // Initialize the descriptions array with sample descriptions
        descriptions = new string[]
        {
            "Hippie Alien Cruiser: Chilled out and Useful to Navigate the Wormhole with its special sensors.",
            "LYNC Cruiser: Connecting link between different starships and Space Stations.",
            "BlackHistoryDAO Cruiser: This is an ancient one, spreading the message of culture and oneness.",
            "Cosmic Exodus Cruiser: This cruiser is set on to a long cosmic voyage with a fun crew.",
            "Innovaz Cruiser: Innovaz Cruiser is built for passionate people who like cool nicknames. ",
            "Pixelbomb Cruiser: An AI driven cruiser which is set to explore fertile planets.",
            // Add more descriptions for each material button
        };

        // Attach the OnClick event listener to each material button
        for (int i = 0; i < materialButtons.Length; i++)
        {
            int buttonIndex = i; // Store the index value for the closure
            materialButtons[i].onClick.AddListener(() => MaterialButtonClicked(buttonIndex));
        }

        // Display the description of the initially selected material
        UpdateDescriptionText();
    }

    void MaterialButtonClicked(int buttonIndex)
    {
        // Check if the selected material is the same as the current material
        if (currentMaterialIndex != buttonIndex)
        {
            // Stop the current typewriter coroutine if running
            if (typewriterCoroutine != null)
            {
                StopCoroutine(typewriterCoroutine);
            }

            currentMaterialIndex = buttonIndex; // Set the selected material index
            ApplyMaterial();

            // Clear the description text
            descriptionText.text = "";

            // Start the typewriter coroutine to display the new description
            typewriterCoroutine = StartCoroutine(TypewriterEffect(descriptions[currentMaterialIndex]));

            // Save the selected material index
            PlayerPrefs.SetInt(SelectedMaterialIndexKey, currentMaterialIndex);
        }
    }

    private void ApplyMaterial()
    {
        // Apply the selected material to each jet model in the array
        for (int i = 0; i < jetRenderers.Length; i++)
        {
            jetRenderers[i].material = jetMaterials[currentMaterialIndex];
        }
    }

    private void UpdateDescriptionText()
    {
        // Clear the current description
        descriptionText.text = "";

        // Start the typewriter coroutine to display the description
        typewriterCoroutine = StartCoroutine(TypewriterEffect(descriptions[currentMaterialIndex]));
    }

    IEnumerator TypewriterEffect(string description)
    {
        typingAudioSource.clip = typingAudioClip; // Set the typing sound clip

        // Iterate through each character in the description
        for (int i = 0; i < description.Length; i++)
        {
            descriptionText.text += description[i]; // Append the current character

            // Play the typing sound
            if (typingAudioSource != null && typingAudioClip != null)
            {
                typingAudioSource.PlayOneShot(typingAudioClip);
            }

            yield return new WaitForSeconds(typewriterSpeed); // Wait for the specified typewriter speed
        }

        // Typewriter effect finished, stop the typing audio
        if (typingAudioSource != null)
        {
            typingAudioSource.Stop();
        }
    }
}



