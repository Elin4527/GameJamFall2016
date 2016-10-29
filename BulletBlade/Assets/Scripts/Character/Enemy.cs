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

    protected override void init()
    {
        currHealth = maxHealth;
    }

    public override Vector2 getInput()
    {
        if (type == EnemyAI.TRACKING && following)
        {
            // Go towards a player if further than following distance
<<<<<<< HEAD
            direction = following.transform.position - transform.position;
=======
            direction = following.GetComponent<Rigidbody2D>().position - rb.position;
>>>>>>> refs/remotes/origin/tilemap-branch
            if (direction.magnitude > distance)
            {
                return direction.normalized * speed;
            }
        }

        return Vector2.zero;

    }

    public override void logic()
    {
        if (currHealth <= 0) Destroy(gameObject);
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
