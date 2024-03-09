using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;

public class Scence2_MovementEthan : MonoBehaviour
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
    [SerializeField] float jumpPower = 130;
    bool jump;
    public int maxHealth = 100;
    int currentHealth;

    public Transform attackPoint;
    public float attackRange = 0.35f;
    public int attackDamage = 30;
    public LayerMask enemyLayers;
    //[SerializeField] bool isCrouched;
    public FillBar fillBar;

    float horizontalValue;
    //float crouchSpeedModifier = 0.5f;
    bool facingRight = true;
    //bool crouchPressed;
    Scene2_AudioManager audioManager;

    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Scene2_AudioManager>();
    }
    void Start()
    {
        currentHealth = maxHealth;
        fillBar.UpdateBar(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
      
        horizontalValue = Input.GetAxisRaw("Horizontal");

        //if we press Jump button enable jump
        if (Input.GetButtonDown("Jump"))
            jump = true;
        else if (Input.GetButtonUp("Jump"))
            jump = false;

        //if we press Crouch button enable jump
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        //    isCrouched = true;
        //else if (Input.GetButtonUp("Crouch"))
        //    isCrouched = false;

    }

    private void FixedUpdate()
    {
        GroundCheck();
        Movement(horizontalValue, jump);
    }

    void GroundCheck()
    {
        isGrounded = false;

        //Check if the GroundCheckObject is colliding with other
        //2D Colliders that are in the "Fore" Layer
        //If yes(isGrounded true) else(isGrounded false)
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
                rb.AddForce(new Vector2(12f, jumpPower));
            }

        }

       
        animator.SetBool("Jump", jumpFlag);

        #endregion

        #region Move 
        //Set value of x using dir and speed
        float xVal = dir * speed * Time.fixedDeltaTime;

     
        //Create Vec2 for the velocity
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        //Set the player's veclocity
        rb.velocity = targetVelocity;


        //If looking right and clicked left(flip to the left)
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
        //Set the float xVelocity according to the x value of the RigiBody2D velocity
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
    }

    void Attack()
    {
       
        animator.SetTrigger("Attack");
        audioManager.PlaySFX(audioManager.attack);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {

          enemy.GetComponent<Monster_behavior>().TakeDamage(attackDamage);
            Debug.Log("Hit: " + enemy.name);
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
            Debug.Log("Ethan died!");
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("Death", true);
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;


    }
    #endregion

}
