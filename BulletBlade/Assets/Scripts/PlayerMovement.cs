using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    enum ActionState
    {
        STANDARD, DASHING
    }

    public float speed;
    public float offset;
    public Vector3 cameraPos;
    Vector2 targetVel;
    Vector2 delta;
    Rigidbody2D rigidBody;
    ActionState state = ActionState.STANDARD;
    float actionTime;
    Vector2 direction = new Vector2(0, -1);

    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {
        rigidBody.velocity += delta;
        if (Mathf.Abs(rigidBody.velocity.x - targetVel.x) + Mathf.Abs(rigidBody.velocity.y - targetVel.y) < 0.1f)
        {
            rigidBody.velocity = targetVel;
            delta = new Vector2(0, 0);
        }
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
                        rigidBody.velocity = direction.normalized * speed;
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
        Vector2 pos = rigidBody.position + direction.normalized * offset;
        cameraPos = new Vector3(pos.x, pos.y, transform.position.z - 1);
    }

    void walking()
    {
        getInput();

        bool isWalking = Mathf.Abs(targetVel.x) > 0.0f || Mathf.Abs(targetVel.y) > 0.0f;
        anim.SetBool("isWalking", isWalking);
        if (isWalking)
        {
            anim.SetFloat("x", rigidBody.velocity.x);
            anim.SetFloat("y", rigidBody.velocity.y);
        }
        direction = rigidBody.velocity;
    }

    void dash()
    {
        state = ActionState.DASHING;
        actionTime = 0.2f;
        rigidBody.velocity = direction.normalized * (speed * 4.0f + targetVel.magnitude)/2.0f;
        setTargetVel(rigidBody.velocity);

        anim.speed = 2.0f;
    }

    void setTargetVel(Vector2 v)
    {
        if (v == targetVel)
            return;
        targetVel = v;
        delta = (v - rigidBody.velocity) * (Time.fixedDeltaTime / 0.1f);
    }

    void getInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        setTargetVel(new Vector2(x, y).normalized * speed);

        if (Input.GetButtonDown("Jump") && direction != Vector2.zero) dash();
    }
}
