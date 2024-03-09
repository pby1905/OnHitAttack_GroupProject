using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EthanHit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D trig)
    {

        if (trig.CompareTag("Ethan"))
        {

            Scence1_Enemy_Behaviour targetHealth = trig.GetComponent<Scence1_Enemy_Behaviour>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(50);
            }
        }

    }
}
