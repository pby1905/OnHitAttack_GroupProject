using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GobinMove;

public class GobinMove : MonoBehaviour
{
    private SpriteRenderer mask;
    private Rigidbody2D rg;
    private enum Movementstate { idle, run, takehit, death, attack1, attack2 };
    private Movementstate state = Movementstate.idle;

    public GameObject enemyToActivate;

    #region Public Variables
    public Transform rayCast;
    public LayerMask rayCastMask;
    public float rayCastLenght;
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    #endregion

    #region Private Variables
    private RaycastHit2D hit;
    private GameObject target;
    private Animator anim;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;
    #endregion
    private int collisionCount = 0;
    public bool IsDied = false;


    void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
        
        mask = GetComponent<SpriteRenderer>();

        intTimer = timer;
        anim = GetComponent<Animator>();

    }
    void Update()
    {
        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLenght, rayCastMask);
            RayCastDebug();
        }

        if(hit.collider != null)
        {
            EnemyLogic();
        }else if(hit.collider != null)
        {
            inRange = false;
        }

        if(inRange == false)
        {
            state = Movementstate.run;
            StopAwake();
        }
        if(IsDied == true)
        {
            state = Movementstate.death;
            anim.SetInteger("state", (int)state);
            Destroy(gameObject, 1f);
        }
    }

    private void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        if(distance > attackDistance)
        {
            Move();
            StopAwake();
        }else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            CoolDown();
            state = Movementstate.idle;
            anim.SetInteger("state", (int)state);
        }
    }

    private void StopAwake()
    {
        cooling = false;
        attackMode = false;
        state = Movementstate.idle;
        anim.SetInteger("state", (int)state);
    }

    private void Attack()
    {
        timer = intTimer;
        attackMode = true;
        state = Movementstate.attack2;
        anim.SetInteger("state", (int)state);
    }

    private void Move()
    {
        state = Movementstate.run;
        anim.SetInteger("state", (int)state);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed *Time.deltaTime);
        }
    }

    void CoolDown()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    private void RayCastDebug()
    {
        if(distance < attackDistance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLenght, Color.red);
        }
        else if (attackDistance > distance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLenght, Color.green);
        }

    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.CompareTag("Player")) {
            target = trig.gameObject;
            inRange = true;
        }

        if (trig.CompareTag("Player_Dame"))
        {
            collisionCount++;
            if (collisionCount == 3)
            {
                IsDied = true;
            }
        }
    }

    public void TriggerCooling()
    {
        cooling = false;
    }

}
