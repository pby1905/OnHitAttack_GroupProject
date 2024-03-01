using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scence1_Enemy_Behaviour : MonoBehaviour
{
    
    public Transform rayCast;
    public LayerMask rayCastMask;
    public float rayCastLength;
    public float attackDistance; //Minimun distance for attack
    public float moveSpeed;
    public float timer; //Timer for cooldown beetween attacks


    private RaycastHit2D hit;
    private GameObject target;
    private Animator anim;
    private float distance;  //Store the distance b/w enemy and palyer
    private bool attackModel;
    private bool inRange;  //check if player is in range
    private bool cooling; //check if Enemy is cooling after attack
    private float inTimer;

    private void Awake()
    {
        inTimer = timer; //Store the inital value of timer
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLength, rayCastMask);
            RayCastDebugger();
        }
        //When player is deteced
        if(hit.collider != null)
        {
            EnemyLogic();
        }
        else if(hit.collider == null)
        {
            inRange = false;
        }
        if(inRange == false)
        {
            anim.SetBool("CanWalk", false);
            StopAttack();
        }
    }

    //When player is deteced

    void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.tag == "Ethan")
        {
            target = trig.gameObject;
            inRange = true;
        }
    }

    void RayCastDebugger()
    {
        if(distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.green);
        }
        else if(attackDistance > distance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.green);
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        if(distance > attackDistance) 
        {
            Move();
            StopAttack();
        }
        else if( attackDistance >= distance && cooling == false )
        {
            Attack();
        }
        if (cooling)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }
    }

    void Move()
    {
        anim.SetBool("CanWalk", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Scene1_Enemy_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = inTimer;
        attackModel = true;
        anim.SetBool("CanWalk", false);
        anim.SetBool("Attack", true);
    }

    void StopAttack()
    {
        cooling = false;
        attackModel = false;
        anim.SetBool("Attack", false);

    }

    void Cooldown()
    {
        timer -= Time.deltaTime;
        if( timer <= 0 && cooling && attackModel)
        {
            cooling = false;
            timer = inTimer;
        }
    }
    public void TriggerCooling()
    {
        cooling = true;
    }
}

