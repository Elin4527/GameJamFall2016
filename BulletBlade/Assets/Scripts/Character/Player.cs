using UnityEngine;
using System.Collections;

public class Player : BaseCharacter {

    enum ActionState
    {
        STANDARD, DASHING
    }

    ActionState state = ActionState.STANDARD;
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
        actionTime = 0.2f;
        setVelocity(direction.normalized * (speed * 4.0f + rb.velocity.magnitude) / 2.0f);

        anim.speed = 2.0f;
    }

    public override Vector2 getInput()
    {
        if (state == ActionState.STANDARD)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            setTargetVel(new Vector2(x, y).normalized * speed);

            if (Input.GetButtonDown("Jump") && isWalking()) dash();

            return (new Vector2(x, y).normalized * speed);
        }
        else
            return rb.velocity;
    }
}
