using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    [SerializeField] int health, maxhealth = 100;
    [SerializeField] FloatingHeathbar healthbar;
    Rigidbody2D rb;
    Vector2 moveDirection;

    private void Start()
    {
        health = maxhealth;
        healthbar.UpdateHealthBar(health, maxhealth);
        player = GameObject.Find("Player").transform;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.transform;
            Flip();
        }
    }



    private void Awake()
    {
        healthbar = GetComponentInChildren<FloatingHeathbar>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthbar.UpdateHealthBar(health, maxhealth);
        if(health <= 0)
        {
            Die();
        }
    }

    private void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > player.position.x)
        {
            rotation.y = 180;
        }
        else
        {
            rotation.y = 0;
        }

        transform.eulerAngles = rotation;
    }
    void Die()
    {
        Destroy(gameObject);
    }




}
