using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthInteraction : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public PlayerHealth healthbar;

    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
            Console.WriteLine("Hitted");
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);

        if(currentHealth < 0)
        {
            Destroy(gameObject);
        }
    }
}
