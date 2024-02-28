using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GobinMove : MonoBehaviour
{
    private SpriteRenderer mask;
    private Rigidbody2D rg;
    private Animator anim;
    public enum Movementstate { idle, run, takehit, death, attack1, attack2 };
    public Movementstate state;
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        mask = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float direcX = Input.GetAxisRaw("Horizontal");

        rg.velocity = new Vector2(direcX * 7f, rg.velocity.y);

        //UpdateAnimationStatde();
    }
    //public void UpdateAnimationStatde()
    //{
    //    Movementstate state;

    //    anim.SetInteger("state", (int)state);
    //}
}
