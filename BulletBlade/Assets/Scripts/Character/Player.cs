using UnityEngine;
using System.Collections;
using System;

public class Player : BaseCharacter {

    enum ActionState
    {
        STANDARD, DASHING
    }

    ActionState state = ActionState.STANDARD;
    public GameObject atkObj;
    public float dashDuration = 0.3f;
    public float invincibleDuration = 0.4f;
    public float attackCd;
    float invincibleTIme;
    float actionTime;
    Animator anim;
    public int damage;
    float cdRemain = 0;
    public int grazeCount = 0;
    public bool graze = false;
    public int maxMana;
    public int mana;
    public int manaCost;

    public float cloneTime;
    float cloneCD;

	// Use this for initialization
	protected override void init () {
        anim = GetComponent<Animator>();
        anim.speed = 1.4f;
        manaCost = maxMana / 4;
    }

    public override void fixedLogic()
    {
        if (graze)
        {
            grazeCount++;
            graze = false;
            mana++;
        }
        mana++;
        if (mana > maxMana)
        {
            mana = maxMana;
        }
    }

    // Update is called once per frame
    public override void logic () {
        direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);

        switch (state)
        {

            case ActionState.DASHING:
                {
                    cloneCD -= Time.deltaTime;
                    actionTime -= Time.deltaTime;


                    if (actionTime < 0.0)
                    {
                        state = ActionState.STANDARD;
                        anim.speed = 1.4f;
                        setVelocity(direction.normalized * speed);
                        setTargetVel(new Vector2(0, 0));
                        anim.SetBool("isDashing", false);
                    }
                    else if (cloneCD <= 0)
                    {
                        SpriteRenderer s = GetComponent<SpriteRenderer>();
                        Sprite spr = s.sprite;
                        
                        GameObject o = new GameObject();
                        cloneCD += cloneTime;

                        o.transform.position = transform.position;
                        SpriteRenderer g = o.AddComponent<SpriteRenderer>() as SpriteRenderer;
                        g.color = new Color(1.0f, 0,.3f, (1 - actionTime/dashDuration) * 0.6f + 0.4f);
                        g.sprite = spr;

                        Destroy(o, actionTime);
                    }
                    break;
                }
            case ActionState.STANDARD:
                walking();
                break;
            default:
                walking();
                break;
        }
        invincibleTIme -= Time.deltaTime;

    }

    public bool isTangible()
    {
        return state == ActionState.STANDARD;
    }

    public void kill()
    {
        reset();

        gameObject.SetActive(false);
    }

    void walking()
    {
        bool walk = isWalking();
        anim.SetBool("isWalking", walk);
    }

    void dash()
    {
        state = ActionState.DASHING;
        actionTime = dashDuration;
        invincibleTIme = invincibleDuration;
        cloneCD = 0.0f;

        // Dash towards mouse cursor
        Vector2 towards = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        setVelocity(towards.normalized * Mathf.Min((speed * 2.0f), towards.magnitude/dashDuration));

        anim.SetBool("isDashing", true);
    }

    void attack()
    {
        GameObject g = (GameObject)Instantiate(atkObj, transform.position + (Vector3)direction.normalized * 1, 
            Quaternion.Euler(0, 0, ((direction.y < 0) ? -1 : 1) * Vector2.Angle(Vector2.right, direction)));
        Attack a = g.GetComponent<Attack>();
        a.damage = damage;
    }

    public override Vector2 getInput()
    {
        cdRemain -= Time.deltaTime;
        if (Input.GetMouseButton(1) && cdRemain <= 0)
        {
            attack();
            cdRemain = attackCd;
        }
        if (state == ActionState.STANDARD)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            setTargetVel(new Vector2(x, y).normalized * speed);

            if (Input.GetMouseButtonDown(0) && mana > manaCost)
            {
                mana -= manaCost;
                dash();
            }

            return (new Vector2(x, y).normalized * speed);
        }
        else
            return vel;
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTangible() && collision.gameObject.GetComponent<Bullet>())
        {
            Destroy(collision.gameObject);
            kill();
        }
    }
}
