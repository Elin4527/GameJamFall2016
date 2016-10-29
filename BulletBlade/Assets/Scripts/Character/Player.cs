using UnityEngine;
using System.Collections;

public class Player : BaseCharacter {

    enum ActionState
    {
        STANDARD, DASHING
    }

    ActionState state = ActionState.STANDARD;
    public float dashDuration = 0.3f;
    public float invincibleDuration = 0.4f;
    float invincibleTIme;
    float actionTime;
    Animator anim;

	// Use this for initialization
	protected override void init () {
        anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	public override void logic () {
        switch (state)
        {
            case ActionState.DASHING:
                {
                    actionTime -= Time.deltaTime;
                    if (actionTime < 0.0)
                    {
                        state = ActionState.STANDARD;
                        anim.speed = 1.0f;
                        setVelocity(direction.normalized * speed);
                        setTargetVel(new Vector2(0, 0));
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
        if (walk)
        {
            anim.SetFloat("x", rb.velocity.x);
            anim.SetFloat("y", rb.velocity.y);
        }
    }

    void dash()
    {
        state = ActionState.DASHING;
        actionTime = dashDuration;
        invincibleTIme = invincibleDuration;

        // Dash towards mouse cursor
        Vector2 towards = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        setVelocity(towards.normalized * Mathf.Min((speed * 2.0f * rb.velocity.magnitude), towards.magnitude/dashDuration));
        anim.SetFloat("x", rb.velocity.x);
        anim.SetFloat("y", rb.velocity.y);
        anim.speed = 4.0f;
    }

    public override Vector2 getInput()
    {
        if (state == ActionState.STANDARD)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            setTargetVel(new Vector2(x, y).normalized * speed);

            if (Input.GetMouseButtonDown(0) && isWalking())
            {
                dash();
            }
            
            return (new Vector2(x, y).normalized * speed);
        }
        else
            return rb.velocity;
    }
}
