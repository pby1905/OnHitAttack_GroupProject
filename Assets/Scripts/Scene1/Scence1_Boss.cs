using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scence1_Boss : MonoBehaviour
{

    public Animator anim;
    public Transform rayCast;
    public LayerMask rayCastMask;
    public float rayCastLength;
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    public Transform leftLimit;
    public Transform rightLimit;
    public int maxHealth = 200;
    int currentHealth;
    //public Transform attackPoint;
    //public float attackRange = 0.35f;
    //public int attackDamage = 10;
    public LayerMask EthanLayers;
    //public Transform triggerArea;

    private RaycastHit2D hit;
    private Transform target;

    private float distance;
    private bool attackModel;
    private bool inRange;
    private bool cooling;
    private float inTimer;


    private void Awake()
    {
        SelectTarget();
        inTimer = timer;
        anim = GetComponent<Animator>();

    }


    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {

        if (!attackModel)
        {
            Move();
        }

        if (!InsideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("S1_Boss_Attack"))
        {
            SelectTarget();

        }
        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, transform.right, rayCastLength, rayCastMask);
            RayCastDebugger();
        }

        if (hit.collider != null)
        {
            EnemyLogic();
        }
        else if (hit.collider == null)
        {
            inRange = false;
        }
        if (inRange == false)
        {

            StopAttack();
        }
    }



    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Ethan")
        {
            target = trig.transform;
            inRange = true;
            Flip();
        }
    }

    void RayCastDebugger()
    {
        if (distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.green);
        }
        else if (attackDistance > distance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.green);
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);
        if (distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            AttackEthan();
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
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("S1_Boss_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void AttackEthan()
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
        if (timer <= 0 && cooling && attackModel)
        {
            cooling = false;
            timer = inTimer;
        }
    }
    public void TriggerCooling()
    {
        cooling = true;
    }

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    private void SelectTarget()
    {
        float distanceToLeft = Vector3.Distance(transform.position, leftLimit.position);
        float distanceRight = Vector3.Distance(transform.position, rightLimit.position);
        if (distanceToLeft > distanceRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }
        Flip();
    }

    private void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180;
        }
        else
        {
            rotation.y = 0;
        }

        transform.eulerAngles = rotation;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Debug.Log("Boss died!");
            Die();
        }
    }

    void Die()
    {
        anim.SetBool("Death", true);
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;


    }
}
