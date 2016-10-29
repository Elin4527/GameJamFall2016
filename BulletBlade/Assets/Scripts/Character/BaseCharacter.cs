using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseCharacter : MonoBehaviour {

    public float speed = 3;
    public float camOffset = 5;
    public Vector3 cameraPos;
    public Vector2 targetVel = Vector2.zero;
    Vector2 vel;
    Vector2 delta;
    protected Rigidbody2D rb;
    public Vector2 direction;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        init();
    }

    protected virtual void init()
    { }

    void FixedUpdate()
    {
        rb.velocity = vel += delta;
        if (Mathf.Abs(rb.velocity.x - targetVel.x) + Mathf.Abs(rb.velocity.y - targetVel.y) < 0.1f)
        {
            setVelocity(targetVel);
        }
        direction = rb.velocity;
    }

    void Update()
    {
        Vector2 i = getInput();
        setTargetVel(i);
        logic();
        cameraPos = rb.position + direction.normalized * camOffset;
    }

    public void reset()
    {
        setVelocity(Vector2.zero);
        direction = Vector2.zero;
        cameraPos = rb.position + direction.normalized * camOffset;
    }

    public void setTargetVel(Vector2 v)
    {
        if (v == targetVel)
            return;
        targetVel = v;
        delta = (v - rb.velocity) * (Time.fixedDeltaTime / 0.1f);
    }

    public void setVelocity(Vector2 v)
    {
        rb.velocity = vel = v;
        setTargetVel(v);
        delta = Vector2.zero;
    }

    public Vector2 getTargetVel()
    {
        return targetVel;
    }

    public bool isWalking()
    {
        return targetVel != Vector2.zero;
    }

    public abstract Vector2 getInput();
    public abstract void logic();
}
