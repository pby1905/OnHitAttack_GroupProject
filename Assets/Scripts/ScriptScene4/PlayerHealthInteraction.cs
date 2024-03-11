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
        currentHealth = maxHealth;
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

    public void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Scene1");
    }
}
