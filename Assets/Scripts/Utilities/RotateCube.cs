using UnityEngine;

public class RotateCube : MonoBehaviour
{
    // Define the axis around which the cube will rotate
    public Vector3 rotationAxis = Vector3.up;

    // Define the rotation speed
    public float rotationSpeed = 50f;

    // Update is called once per frame
    void Update()
    {
        // Calculate the rotation angle based on time and speed
        float angle = rotationSpeed * Time.deltaTime;

        // Rotate the cube around its center along the specified axis
        transform.RotateAround(transform.position, rotationAxis, angle);
    }
}
