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
        if (smoothPosition.x > 30)
        {
            smoothPosition.x = 30;
        }
        else if (smoothPosition.x < -23)
        {
            smoothPosition.x = -23;
        }

        if (smoothPosition.y > 1)
        {
            smoothPosition.y = 1;
        }
        else if (smoothPosition.y < -6)
        {
            smoothPosition.y = -6;
        }
        transform.position = smoothPosition;
    }
}
