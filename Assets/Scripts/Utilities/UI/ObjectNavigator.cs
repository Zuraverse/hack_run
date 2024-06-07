using UnityEngine;

public class ObjectNavigator : MonoBehaviour
{
    public GameObject parentObject;
    private Transform[] childObjects;
    private int currentIndex = 0;

    void Start()
    {
        // Get all child objects of the parent
        int childCount = parentObject.transform.childCount;
        childObjects = new Transform[childCount];
        for (int i = 0; i < childCount; i++)
        {
            childObjects[i] = parentObject.transform.GetChild(i);
            childObjects[i].gameObject.SetActive(false);
        }

        // Show the first child object
        ShowObject(currentIndex);
    }


    public void NextObject()
    {
        // Hide the current object
        HideObject(currentIndex);

        // Move to the next object
        currentIndex++;
        if (currentIndex >= childObjects.Length)
        {
            currentIndex = 0; // Wrap around to the first object
        }

        // Show the next object
        ShowObject(currentIndex);
    }

    public void PreviousObject()
    {
        // Hide the current object
        HideObject(currentIndex);

        // Move to the previous object
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = childObjects.Length - 1; // Wrap around to the last object
        }

        // Show the previous object
        ShowObject(currentIndex);
    }

    private void ShowObject(int index)
    {
        childObjects[index].gameObject.SetActive(true);
    }

    private void HideObject(int index)
    {
        childObjects[index].gameObject.SetActive(false);
    }
}
