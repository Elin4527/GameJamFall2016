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
    public Player p;
    public float distance;
    public int maxHealth = 1;
    public int points;
    int currHealth;
    Animator anim;
    float spawnTime;
    bool dying=false;


    protected override void init()
    {
        currHealth = maxHealth;
        anim = GetComponent<Animator>();
        anim.SetBool("spawn", true);
        spawnTime = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
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
        spawnTime -= Time.deltaTime;
        if (currHealth <= 0 && !dying)
        {
            StartCoroutine("die");
        }
    }

    public IEnumerator die()
    {
        dying = true;
        anim.SetBool("spawn", false);
        yield return new WaitForEndOfFrame();

        Destroy(gameObject, anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        
    }

    public void hit(int damage)
    {
        currHealth -= damage;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Attack a = collision.gameObject.GetComponent<Attack>();
        if (a && spawnTime < 0 )
        {
            hit(a.damage);
        }
    }
}
