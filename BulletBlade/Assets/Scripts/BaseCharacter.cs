using UnityEngine;
using System.Collections;

public class BaseCharacter : MonoBehaviour {

    public float speed = 3;
    public float camOffset = 5;
    public Vector3 cameraPos;
    Vector2 targetVel = Vector2.zero;
    Vector2 delta;
    public Rigidbody2D rb;
    public Vector2 direction;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void FixedUpdate()
    {
        rb.velocity += delta;
        if (Mathf.Abs(rb.velocity.x - targetVel.x) + Mathf.Abs(rb.velocity.y - targetVel.y) < 0.1f)
        {
            setVelocity(targetVel);
        }
        direction = rb.velocity;
    }

    void Update()
    {
        Vector2 pos = rb.position + direction.normalized * camOffset;
        cameraPos = new Vector3(pos.x, pos.y, transform.position.z - 1);
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
        rb.velocity = v;
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
}
