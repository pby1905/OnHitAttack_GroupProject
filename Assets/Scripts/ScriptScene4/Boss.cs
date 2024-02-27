using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    [SerializeField] float health, maxhealth = 3;
    [SerializeField] FloatingHeathbar healthbar;
    Rigidbody2D rb;
    Vector2 moveDirection;

    private void Start()
    {
        health = maxhealth;
        healthbar.UpdateHealthBar(health, maxhealth);
        player = GameObject.Find("Player").transform;
    }


    private void Awake()
    {
        healthbar = GetComponentInChildren<FloatingHeathbar>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthbar.UpdateHealthBar(health, maxhealth);
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }



    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}
