using EthanTheHero;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackEnemy : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;
    private EnemyBehavior enemyBehavior;
    private PlayerAttackMethod method;


    void Awake()
    {
        enemyBehavior = GetComponent<EnemyBehavior>();
    }


    public void Attack()
    {
        if(timeBtwAttack <= 0)
        {
            // Then you can attack
            if(Input.GetMouseButtonDown(0))
            {
                Collider2D[] collider = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for(int i = 0; i < collider.Length; i++)
                {
                    collider[i].GetComponent<EnemyBehavior>().TakeDamage(enemyBehavior.currentHealth);
                    
                }            
            }
            timeBtwAttack = startTimeBtwAttack;

        } else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
