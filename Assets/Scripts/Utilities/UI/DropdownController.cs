using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownController : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public GameObject[] gameObjectsToToggle;
    public Image backgroundImage;
    public Sprite[] backgroundSprites; // Array of sprites to be used as backgrounds

    private void Start()
    {
        // Register a callback for when the dropdown value changes
        dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged();
        });

        // Set initial background image
        DropdownValueChanged(0);
    }

    // This method is called when the dropdown value changes
    void DropdownValueChanged()
    {
        int selectedIndex = dropdown.value;
        DropdownValueChanged(selectedIndex);
    }

    // Overloaded method to directly set the background image based on the selected index
    void DropdownValueChanged(int selectedIndex)
    {
        // Iterate through the game objects and set their setActive state
        for (int i = 0; i < gameObjectsToToggle.Length; i++)
        {
            if (i == selectedIndex)
            {
                gameObjectsToToggle[i].SetActive(true);
            }
            else
            {
                gameObjectsToToggle[i].SetActive(false);
            }
        }

        // Change background image based on the selected index
        if (selectedIndex < backgroundSprites.Length && selectedIndex >= 0)
        {
            backgroundImage.sprite = backgroundSprites[selectedIndex];
        }
        else
        {
            Debug.LogWarning("Selected index out of range for backgroundSprites array.");
        }
    }
}
