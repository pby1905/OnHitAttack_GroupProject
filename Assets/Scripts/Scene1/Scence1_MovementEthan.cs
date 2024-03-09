using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Timeline;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;
using UnityEngine.UI;

public class Scence1_MovementEthan : MonoBehaviour
{
    Rigidbody2D rb;
    public Animator animator;
    [SerializeField] Collider2D standingCollider;
    [SerializeField] Transform foreCheckCollider;

    //[SerializeField] Transform overheadCheckCollider;
    [SerializeField] LayerMask foreLayer;
    [SerializeField] bool isGrounded;

    const float foreCheckRadius = 0.2f;
    //const float overheadCheckRadius = 0.2f;
    [SerializeField] float speed = 300;
    [SerializeField] float jumpPower = 150;
    bool jump;
    public int maxHealth = 100;
    int currentHealth;

    public Transform attackPoint;
    public float attackRange = 0.35f;
    //public int attackDamage = 30;
    public LayerMask enemyLayers;
    //[SerializeField] bool isCrouched;
    public FillBar fillBar;
    
    float horizontalValue;
    //float crouchSpeedModifier = 0.5f;
    bool facingRight = true;
    //bool crouchPressed;
    Scene1_AudioManager audioManager;

    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Scene1_AudioManager>();
    }
    void Start()
    {
        currentHealth = maxHealth;
        fillBar.UpdateBar(currentHealth, maxHealth);
    }

   
    void Update()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");

        
        if (Input.GetButtonDown("Jump"))
            jump = true;
        else if (Input.GetButtonUp("Jump"))
            jump = false;

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
         

    }

    private void FixedUpdate()
    {
        GroundCheck();
        Movement(horizontalValue, jump);
    }

    void GroundCheck()
    {
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(foreCheckCollider.position, foreCheckRadius, foreLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
        }
    }

    void Movement(float dir, bool jumpFlag)
    {
        #region Jump && Crouch
       
        

        //If we press Crouch we disable standing collider + player crouching
        //Reduce the speed
        //If release resume the original speed + enable the standing collider + disable crounch player
        if (isGrounded)
        {

            //if the player is grounded and pressed space Jump
            if (jumpFlag)
            {
                isGrounded = false;
                jumpFlag = false;
                rb.AddForce(new Vector2(0f, jumpPower));
            }

        }

       
        animator.SetBool("Jump", jumpFlag);

        #endregion

        #region Move 
        
        float xVal = dir * speed * Time.fixedDeltaTime;

     
        
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        
        rb.velocity = targetVelocity;


        
        if (facingRight && dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // quay ve ben trai
            facingRight = false;
        }
        else if (!facingRight && dir > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
        
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        audioManager.PlaySFX(audioManager.attack);
        Collider2D[] hitEnemies =  Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            Scence1_Enemy_Behaviour enemyBehaviour = enemy.GetComponent<Scence1_Enemy_Behaviour>();
            if (enemyBehaviour != null)
            {
                
                enemyBehaviour.TakeDamage(50);
                Debug.Log("Hit: " + enemy.name);
            }
            else
            {
                
                Scence1_Boss bossBehaviour = enemy.GetComponent<Scence1_Boss>();
                if (bossBehaviour != null)
                {
                    bossBehaviour.TakeDamage(50);
                    Debug.Log("Hit boss: " + enemy.name);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        fillBar.UpdateBar(currentHealth, maxHealth);
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Debug.Log("Player died!");
            Die();
        }
    }

    public void Healing(int plusmark)
    {
        if (currentHealth < 100)
        {
            currentHealth += plusmark;
            fillBar.UpdateBar(currentHealth, maxHealth);

            if (currentHealth >= 100)
            {
                currentHealth = 100;
                fillBar.UpdateBar(currentHealth, maxHealth);
            }
        }

    }

    void Die()
    {
        animator.SetBool("Death", true);
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        //this.enabled = false;


    }
    #endregion

}
