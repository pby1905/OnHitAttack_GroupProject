using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarFollowing : MonoBehaviour
{
    public Transform characterTransform; // Reference to the character's Transform
    public Vector3 offset; // Offset from the character's position

    void LateUpdate()
    {
        if (characterTransform != null)
        {
            // Calculate the position with offset
            Vector3 targetPosition = characterTransform.position + offset;

            // Convert the world position to screen space
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetPosition);

            // Update the health bar's position
            transform.position = screenPosition;
        }
    }

}
