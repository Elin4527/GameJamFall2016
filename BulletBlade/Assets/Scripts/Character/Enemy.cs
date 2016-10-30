using UnityEngine;
using System.Collections;
using System;

public class Enemy : BaseCharacter {

    public enum EnemyAI
    {
        STATIC, TRACKING
    }
    public EnemyAI type;
    public BaseCharacter following;
    public float distance;
    public int maxHealth = 1;
    int currHealth;
    Animator anim;


    protected override void init()
    {
        currHealth = maxHealth;
        anim = GetComponent<Animator>();
        anim.SetBool("spawn", true);
    }

    public override Vector2 getInput()
    {
        if (type == EnemyAI.TRACKING && following)
        {
            // Go towards a player if further than following distance
            direction = following.transform.position - transform.position;
            if (direction.magnitude > distance)
            {
                return direction.normalized * speed;
            }
        }

        return Vector2.zero;

    }

    public override void logic()
    {
        if (currHealth <= 0)
        {
            anim.SetBool("spawn", false);
            Destroy(gameObject, anim.GetNextAnimatorClipInfo(0)[0].clip.length);
        }
    }

    public void hit(int damage)
    {
        currHealth -= damage;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered");
        Attack a = collision.gameObject.GetComponent<Attack>();
        if (a)
        {
            hit(a.damage);
        }
    }
}
