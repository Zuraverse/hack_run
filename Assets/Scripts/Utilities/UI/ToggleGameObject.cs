using UnityEngine;
using UnityEngine.UI;

public class ToggleGameObject : MonoBehaviour
{
    // Reference to the GameObject you want to toggle
    public GameObject targetObject;

    // Reference to the Button component
    private Button toggleButton;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Button component attached to this GameObject
        toggleButton = GetComponent<Button>();

        // Add a listener to the button's click event
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleTargetObject);
        }
    }

    // Method to toggle the active state of the target GameObject
    void ToggleTargetObject()
    {
        if (targetObject != null)
        {
            // Toggle the active state
            bool isActive = targetObject.activeSelf; // Get current active state
            targetObject.SetActive(!isActive); // Set it to the opposite
        }
    }
}
