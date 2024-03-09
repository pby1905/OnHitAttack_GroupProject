using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    private Rigidbody2D body;
    private SpriteRenderer mask;
    private float speed = 7;
    public bool allowjump;


    private Collider2D collider2D;
    private float wallJumpCooldown;
    public GameObject prefabToInstantiate;
    private Animator anim;

    private enum MovemenState { idle, running, jumping, hurt, attack1, attack2, attack3, death}; 
    private MovemenState state = MovemenState.idle;

    public FillBar BloodBar;
    [SerializeReference]public int bloodpre;
    public int maxblood = 100;
    Scene1_AudioManager audioManager;
    void Start()
    {
        bloodpre = maxblood;
        BloodBar.UpdateBar(bloodpre, maxblood);
    }

    private void OnMouseDown()
    {
        bloodpre -= 10;
        BloodBar.UpdateBar(bloodpre, maxblood);
    }
    public void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        mask = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Scene1_AudioManager>();
    }

    void Update()
    {
        float direcX = Input.GetAxisRaw("Horizontal");

        body.velocity = new Vector2(direcX * 7f, body.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && allowjump)
        {
            body.AddForce(Vector2.up *7f, ForceMode2D.Impulse);
        }
        
        UpdateAnimationStatde();

    }
    private void OnTriggerEnter2D(Collider2D collisionkhac)
    {
        if (collisionkhac.gameObject.tag == "ground")
        {
            allowjump = true;
        }
        else if (collisionkhac.gameObject.CompareTag("hitBox"))
        {
            audioManager.PlaySFX(audioManager.attack);
            bloodpre -= 10;
            BloodBar.UpdateBar(bloodpre, maxblood);
            state = MovemenState.hurt;
            anim.SetInteger("state", (int)state);
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            allowjump = false;
        }

    }
    public void UpdateAnimationStatde() 
    {
        MovemenState state;
        if (body.velocity.x > 0f)
        {
            state = MovemenState.running;
            mask.flipX = false;
        }
        else if (body.velocity.x < 0f)
        {
            state = MovemenState.running;
            mask.flipX = true;
        }
        else
        {
            state = MovemenState.idle;
        }

        if (body.velocity.y > .1f && allowjump == true)
        {
            state = MovemenState.jumping;
        }
        else if ( bloodpre <= 0)
        {
            state = MovemenState.death;
        }
        //if (body.velocity.y > .1f)
        //{
        //    state = MovemenState.death;
        //}
        else if (Input.GetKey(KeyCode.Alpha1))
        {
            state |= MovemenState.attack1;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            state |= MovemenState.attack2;
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            state |= MovemenState.attack3;
        }
        anim.SetInteger("state", (int)state);
    }
}
