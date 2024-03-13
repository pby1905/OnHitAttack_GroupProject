using EthanTheHero;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthInteraction : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public FillBar healthbar;

   

    void Start()
    {
        currentHealth = ManageEthanBlood.instance.scene1_CurrentHealth;
        healthbar.UpdateBar(currentHealth, maxHealth);
    }

    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
            Console.WriteLine("Hitted");
        }
    }*/
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        healthbar.UpdateBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Healing(int plusmark)
    {
        if (currentHealth < 100)
        {
            currentHealth += plusmark;
            healthbar.UpdateBar(currentHealth, maxHealth);

            if (currentHealth >= 100)
            {
                currentHealth = 100;
                healthbar.UpdateBar(currentHealth, maxHealth);
            }
        }

    }



    public void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Scene1");
    }
}
