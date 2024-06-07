using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseCarousel : MonoBehaviour
{
    public List<GameObject> characterPrefabs; // List of character prefabs
    public GameObject characterIconPrefab; // Optional: Prefab for character icons (if used)
    public float scrollSpeed; // Speed of carousel movement
    public int selectedCharacterIndex; // Index of the currently selected character

    private RectTransform contentRect; // RectTransform of the Content Panel
    private float contentWidth; // Calculated width of the content based on character prefabs

    void Start()
    {
        contentRect = GetComponent<RectTransform>();
        contentWidth = CalculateContentWidth(); // Function to calculate total width of characters
    }

    void Update()
    {
        // Implement logic for scrolling the content based on user input (left/right buttons)
        contentRect.offsetMin = new Vector2(contentRect.offsetMin.x + scrollSpeed * Time.deltaTime, contentRect.offsetMin.y);
        ClampContentPosition(); // Function to ensure content stays within bounds
    }

    public void OnCharacterSelected(int index) // Function called from character selection logic
    {
        selectedCharacterIndex = index;
        // Implement logic to visually indicate selected character (highlight, icon change)
    }

    private float CalculateContentWidth()
    {
        // Calculate the total width needed to display all characters side-by-side
        float totalWidth = 0;
        foreach (GameObject prefab in characterPrefabs)
        {
            totalWidth += prefab.GetComponent<RectTransform>().rect.width;
        }
        return totalWidth;
    }

    private void ClampContentPosition()
    {
        // Ensure the content doesn't scroll beyond the edges of the Content Panel
        contentRect.offsetMin = new Vector2(Mathf.Clamp(contentRect.offsetMin.x, 0, contentWidth - contentRect.rect.width), contentRect.offsetMin.y);
    }
}
