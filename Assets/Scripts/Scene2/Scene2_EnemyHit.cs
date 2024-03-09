using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scene2_EnemyHit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.CompareTag("Ethan"))
        {
            Scence2_MovementEthan health = trig.GetComponent<Scence2_MovementEthan>();  
            if (health != null)
            {
                health.TakeDamage(10);
            }
        }

    }
}