using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEthan : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    //[SerializeField] Collider2D standingCollider;
    //[SerializeField] Transform foreCheckCollider;

    //[SerializeField] Transform overheadCheckCollider;
    //[SerializeField] LayerMask foreLayer;
    //[SerializeField] LayerMask enemyLayer;
    //[SerializeField] bool isGrounded;

    //const float foreCheckRadius = 0.2f;
    //const float overheadCheckRadius = 0.2f;
    [SerializeField] float speed = 300;
    [SerializeField] float jumpPower = 6;
    bool jump;
    //[SerializeField] bool isCrouched;

    float horizontalValue;
    //float crouchSpeedModifier = 0.5f;
    bool facingRight = true;
    //bool crouchPressed;

    void Start()
    {

    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
            jump = true;
        else if (Input.GetButtonUp("Jump"))
            jump = false;

        Move(horizontalValue, jump);

    }

    void FixUpdate()
    {

    }

    void Move(float dir, bool jumpFlag)
    {
        float xVal = dir * speed * Time.fixedDeltaTime;
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        //Set the player's veclocity
        rb.velocity = targetVelocity;

        if (jumpFlag)
        {

            jumpFlag = false;
            rb.AddForce(new Vector2(0f, jumpPower));
        }


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
}
