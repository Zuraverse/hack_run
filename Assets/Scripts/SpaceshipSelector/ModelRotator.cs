using UnityEngine;
using System.Collections;

public class ModelRotator : MonoBehaviour
{
    public float rotationSpeed = 30f; // Speed of rotation in degrees per second
    public GameObject objectToRotate; // Object to rotate
    public float resetDuration = 1f; // Duration of smooth reset in seconds

    private bool isRotatingWithMouse = false;
    private Vector3 mouseStartPosition;
    private Quaternion initialRotation;
    private Vector3 rotation;

    private void Start()
    {
        // Store the initial rotation of the model
        initialRotation = objectToRotate.transform.rotation;
    }

    private void Update()
    {
        MouseRotation();
    }

    IEnumerator SmoothReset()
    {
        Quaternion currentRotation = objectToRotate.transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < resetDuration)
        {
            elapsedTime += Time.deltaTime;
            objectToRotate.transform.rotation = Quaternion.Slerp(currentRotation, initialRotation, elapsedTime / resetDuration);
            yield return null;
        }

        objectToRotate.transform.rotation = initialRotation;
    }

    void MouseRotation()
    {
        if (Input.GetMouseButtonDown(0))
                {
                    // Mouse button is pressed, start rotating with mouse
                    isRotatingWithMouse = true;
                    mouseStartPosition = Input.mousePosition;
                    StopCoroutine("SmoothReset"); // Stop any ongoing smooth reset
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    // Mouse button is released, stop rotating with mouse and start smooth reset
                    isRotatingWithMouse = false;
                    StartCoroutine("SmoothReset");
                }

                if (isRotatingWithMouse)
                {
                    // Rotate the model with mouse movement
                    float mouseX = Input.mousePosition.x - mouseStartPosition.x;
                    float mouseY = Input.mousePosition.y - mouseStartPosition.y;
                    objectToRotate.transform.Rotate(Vector3.up, -mouseX * rotationSpeed * Time.deltaTime);
                    objectToRotate.transform.Rotate(Vector3.right, -mouseY * rotationSpeed * Time.deltaTime);
                    mouseStartPosition = Input.mousePosition;
                }
                else
                {
                    // Rotate the model automatically
                    objectToRotate.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
                }
            }
}
