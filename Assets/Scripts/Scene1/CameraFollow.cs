using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 setting;
    public float smoothFactor = 3;
    private void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 playerPosition = player.position + setting;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, playerPosition, smoothFactor * Time.fixedDeltaTime);
        if (smoothPosition.x > 28)
        {
            smoothPosition.x = 28;
        }
        else if (smoothPosition.x < -18)
        {
            smoothPosition.x = -18;
        }

        if (smoothPosition.y > 1)
        {
            smoothPosition.y = 1;
        }
        else if (smoothPosition.y < -5)
        {
            smoothPosition.y = -5;
        }
        transform.position = smoothPosition;
    }
}
