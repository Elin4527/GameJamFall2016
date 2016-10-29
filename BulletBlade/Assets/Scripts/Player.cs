using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    enum ActionState
    {
        STANDARD, DASHING
    }

    ActionState state = ActionState.STANDARD;
    float actionTime;
    BaseCharacter scr;
    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        scr = GetComponent<BaseCharacter>();
	}

	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case ActionState.DASHING:
                {
                    actionTime -= Time.deltaTime;
                    if (actionTime < 0.0)
                    {
                        state = ActionState.STANDARD;
                        anim.speed = 1.0f;
                        scr.setVelocity(scr.direction.normalized * scr.speed);
                        scr.setTargetVel(new Vector2(0, 0));
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
        getInput();

        bool isWalking = scr.isWalking();
        anim.SetBool("isWalking", isWalking);
        if (isWalking)
        {
            anim.SetFloat("x", scr.rb.velocity.x);
            anim.SetFloat("y", scr.rb.velocity.y);
        }
    }

    void dash()
    {
        state = ActionState.DASHING;
        actionTime = 0.2f;
        scr.setVelocity(scr.direction.normalized * (scr.speed * 4.0f + scr.rb.velocity.magnitude) / 2.0f);

        anim.speed = 2.0f;
    }

    void getInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Debug.Log("THIS");
        scr.setTargetVel(new Vector2(x, y).normalized * scr.speed);

        if (Input.GetButtonDown("Jump") && scr.isWalking()) dash();
    }
}
