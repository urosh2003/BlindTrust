using UnityEngine;

public class CameraFollowPlayerScript : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset;   // Offset from the player's position

    private void Start()
    {
        // Initialize the offset based on initial positions
        if (player != null)
        {
            transform.position = offset + player.position;
        }
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            // Update the camera's position while keeping the rotation fixed
            transform.position = player.position + offset;
        }
    }
}
