using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseRankLeaderboard : MonoBehaviour
{
    // Reference to the parent GameObject with the VerticalLayoutGroup component
    public GameObject parentGameObject;

    public void startSorting()
    {
        Debug.Log("HouseRank Called");
        // Get all child GameObjects
        Transform[] children = new Transform[parentGameObject.transform.childCount];
        for (int i = 0; i < parentGameObject.transform.childCount; i++)
        {
            children[i] = parentGameObject.transform.GetChild(i);
            Debug.Log(children[i].name);
        }

        // Sort children based on score text values
        System.Array.Sort(children, CompareByScore);

        // Rearrange sorted children under the parent
        for (int i = 0; i < children.Length; i++)
        {
            children[i].SetSiblingIndex(i);
        }
    }

    // Comparison function to sort by score text values
    private int CompareByScore(Transform a, Transform b)
    {
        Text scoreTextA = a.Find("ScoreText")?.GetComponent<Text>();
        Text scoreTextB = b.Find("ScoreText")?.GetComponent<Text>();

        // Check for null references
        if (scoreTextA == null || scoreTextB == null)
        {
            // Handle the case where the score text component is not found
            Debug.LogError("ScoreText component not found in one or both GameObjects.");
            return 0; // or any default value based on your requirements
        }

        // Parse score values
        int scoreA = int.Parse(scoreTextA.text);
        int scoreB = int.Parse(scoreTextB.text);

        // Compare scores
        return scoreB.CompareTo(scoreA); // Sort in descending order
    }
}
