using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D trig)
    {

        if (trig.CompareTag("Ethan"))
        {
            Debug.Log("Enemy hit:" + trig.gameObject.name);
            Scence1_MovementEthan targetHealth = trig.GetComponent<Scence1_MovementEthan>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(20);
            }
        }

    }
}
