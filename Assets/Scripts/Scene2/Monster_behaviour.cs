using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_behavior : MonoBehaviour
{
    public Animator anim;
    public Transform rayCast;
    public LayerMask rayCastMask;
    public float rayCastLength;
    public float attackDistance; //Minimun distance for attack
    public float moveSpeed;
    public float timer; //Timer for cooldown beetween attacks
    public Transform leftLimit;
    public Transform rightLimit;
    public int maxHealth = 100;
    int currentHealth;
    public Transform attackPoint;
    public float attackRange = 0.35f;
    public int attackDamage = 10;
    public LayerMask EthanLayers;

    private RaycastHit2D hit;
    private Transform target;

    private float distance;  //Store the distance b/w enemy and palyer
    private bool attackModel;
    private bool inRange;  //check if player is in range
    private bool cooling; //check if Enemy is cooling after attack
    private float inTimer;


    private void Awake()
    {
        SelectTarget();
        inTimer = timer; //Store the inital value of timer
        anim = GetComponent<Animator>();

    }


    void Start()
    {
        currentHealth = maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        if (!attackModel)
        {
            Move();
        }

        if (!InsideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("S2_Flying_Attack"))
        {
            SelectTarget();
        }
        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, transform.right, rayCastLength, rayCastMask);
            RayCastDebugger();
        }
        //When player is deteced
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

    //When player is deteced

    void OnTriggerEnter2D(Collider2D trig)
    {
        Debug.Log("trigger:" + trig.gameObject.tag == "Enemy");
        if (trig.gameObject.tag == "Enemy")
        {
            target = trig.transform;
            Debug.Log("target:" + target);
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
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("S2_Flying_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = inTimer;
        attackModel = true;
        anim.SetBool("CanWalk", false);
        anim.SetBool("Attack", true);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, EthanLayers);
        foreach (Collider2D ethan in hitEnemies)
        {
            ethan.GetComponent<Scence1_MovementEthan>().TakeDamage(attackDamage);
            Debug.Log("Hit: " + ethan.name);
        }
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
            Debug.Log("Enemy died!");
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

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}




