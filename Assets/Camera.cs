using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player’s transform
    public float smoothSpeed = 0.125f;  // Smoothing factor for the camera's movement
    public Vector3 offset;  // Offset to adjust the camera's position from the player

    void LateUpdate()
    {
        if (player == null)
            return;
        // Calculate the desired position of the camera (player's position + offset)
        Vector3 desiredPosition = player.position + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Apply the smoothed position to the camera
        transform.position = smoothedPosition;
    }
}
