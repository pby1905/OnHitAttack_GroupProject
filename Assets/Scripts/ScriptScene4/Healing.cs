using EthanTheHero;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision with: " + collision.gameObject.name);
        if (collision.tag == "Player")
        {

            PlayerHealthInteraction targetHealth = collision.GetComponent<PlayerHealthInteraction>();
            if (targetHealth != null)
            {
                targetHealth.Healing(40);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Scene4 component is missing on Ethan object.");
            }


        }
    }
}
