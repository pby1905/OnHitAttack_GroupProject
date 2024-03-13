using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2_BossHit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D trig)
    {

        if (trig.CompareTag("Ethan"))
        {
            Debug.Log("Enemy hit:" + trig.gameObject.name);
            Scence2_MovementEthan targetHealth = trig.GetComponent<Scence2_MovementEthan>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(25);
            }
        }

    }
}
