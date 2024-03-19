using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S3_Enemy_Hit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D trig)
    {

        if (trig.CompareTag("Ethan"))
        {
            Debug.Log("Enemy hit:" + trig.gameObject.name);
            S3_MovementEthan targetHealth = trig.GetComponent<S3_MovementEthan>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(10);
            }
        }

    }
}
