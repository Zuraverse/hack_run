using UnityEngine;

public class PlayerSelectors : MonoBehaviour
{
    public Transform[] playerPositions; // An array of the positions of the player objects
    public float swipeThreshold = 100f; // Minimum distance to consider a swipe
    public float snapSpeed = 10f; // Speed at which the selector snaps to the closest position

    private Vector2 startPos;
    private Vector2 endPos;
    private int currentIndex = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            CheckSwipe();
        }
    }

    private void CheckSwipe()
    {
        float swipeDistance = (endPos - startPos).magnitude;

        if (swipeDistance > swipeThreshold)
        {
            // Swipe right
            if (endPos.x > startPos.x)
            {
                ShowPreviousPlayer();
            }
            // Swipe left
            else
            {
                ShowNextPlayer();
            }
        }
    }

    private void ShowPreviousPlayer()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = playerPositions.Length - 1;

        MoveToCurrentPlayer();
    }

    private void ShowNextPlayer()
    {
        currentIndex++;
        if (currentIndex >= playerPositions.Length)
            currentIndex = 0;

        MoveToCurrentPlayer();
    }

    private void MoveToCurrentPlayer()
        {
            // Get the target position (the position of the current player position)
            Vector3 targetPosition = playerPositions[currentIndex].position;

            // Move the selector object to the target position using linear interpolation (Lerp)
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * snapSpeed);
        }
}
