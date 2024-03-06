using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GobinMove;

public class GobinMove : MonoBehaviour
{
    private SpriteRenderer mask;
    private Rigidbody2D rg;
    private Animator anim;
    private enum Movementstate { idle, run, takehit, death, attack1, attack2 };
    private Movementstate state = Movementstate.idle;

    public Transform player;
    public float detectionRange = 5f;
    public float attackRange = 1.0f;
    private bool playerDetected = false;

    public GameObject enemyToActivate;
    void Start()
    {
        

    }
    private void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        mask = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            playerDetected = true;
        }
        else
        {
            playerDetected = false;
        }

        if (playerDetected)
        {
            if (distanceToPlayer <= attackRange)
            {
                state = Movementstate.attack1;
                anim.SetInteger("state", (int)state);
            }
            else
            {
                Vector2 direction = player.position - transform.position;
                direction.y = 0;
                direction.Normalize();
                transform.Translate(direction * 2f * Time.deltaTime);
                UpdateAnimationStatde();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            enemyToActivate.SetActive(true);
        }
    }

    public void UpdateAnimationStatde()
    {
        if (rg.velocity.x > 0f)
        {
            state = Movementstate.run;
            mask.flipX = false;
        }else if (rg.velocity.x < 0f)
        {
            state = Movementstate.run;
            mask.flipX = true;
        }
        anim.SetInteger("state", (int)state);
    }
}
