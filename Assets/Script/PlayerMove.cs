using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    private Rigidbody2D body;
    private SpriteRenderer mask;
    private float speed = 7;

    private Collider2D collider2D;
    private float wallJumpCooldown;
    public GameObject prefabToInstantiate;
    void Start()
    {
        
    }
    public void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        mask = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
    }

    void Update()
    {
        float direcX = Input.GetAxisRaw("Horizontal");

        body.velocity = new Vector2(direcX * 7f, body.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            body.velocity = new Vector2(body.velocity.x, 7f);
        }
        if (body.velocity != Vector2.zero)
        {
            if (body.velocity.x > 0)
            {
                mask.flipX = false;
            }
            else if (body.velocity.x < 0)
            {
                mask.flipX = true;
            }
        }
    }
}
