using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing_Ethan_S2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Collision with: " + collision.gameObject.name);
        if (collision.tag == "Ethan")
        {

            Scence2_MovementEthan targetHealth = collision.GetComponent<Scence2_MovementEthan>();
            if (targetHealth != null)
            {
                targetHealth.Healing(10);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Scence1_MovementEthan component is missing on Ethan object.");
            }


        }
    }
}
