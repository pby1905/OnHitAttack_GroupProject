using EthanTheHero;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D trig)
    {

        if (trig.CompareTag("Player"))
        {
            Debug.Log("Enemy hit:" + trig.gameObject.name);
            PlayerMovement targetHealth = trig.GetComponent<PlayerMovement>();
            if (targetHealth != null)
            {
               /* targetHealth.TakeDamage(10);*/
            }
        }

    }

}
