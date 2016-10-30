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
    public float despawnTime;
    float lifeTimer;
    int currHealth;
    Animator anim;
    float spawnTime;
    bool dying=false;


    protected override void init()
    {
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
        if (spawnTime >= 0)
        {
            spawnTime -= Time.deltaTime;
        }
        else if (despawnTime != 0)
        {
            lifeTimer += Time.deltaTime;
        }
        if (lifeTimer > despawnTime && !dying)
        {
            StartCoroutine("despawn");
        }
        else if (currHealth <= 0 && !dying)
        {
            die();
        }
    }
    public void die()
    {
        StartCoroutine("despawn");
        p.score += points;
    }
    public IEnumerator despawn()
    {
        dying = true;
        anim.SetBool("spawn", false);

        yield return new WaitForEndOfFrame();
        BulletSpawner[] b = GetComponentsInChildren<BulletSpawner>();
        for (int i = 0; i < b.Length; i++)
        {
            Destroy(b[i]);
        }
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
