using UnityEngine;
using System.Collections;

public abstract class BaseCharacter : MonoBehaviour {

    public float speed = 3;
    public float camOffset = 5;
    public Vector3 cameraPos;
    public Vector2 targetVel = Vector2.zero;
    protected Vector2 vel;
    Vector2 delta;
    public Vector2 direction;
    
    void Start()
    {
        init();
    }

    protected virtual void init()
    { }

    void FixedUpdate()
    {
        vel += delta;
        if (Mathf.Abs(vel.x - targetVel.x) + Mathf.Abs(vel.y - targetVel.y) < 0.1f)
        {
            setVelocity(targetVel);
        }
        transform.position += (Vector3)vel * Time.fixedDeltaTime;
    }

    void Update()
    {
        Vector2 i = getInput();
        setTargetVel(i);
        logic();
        cameraPos = (Vector2)transform.position + direction.normalized * camOffset;
    }

    public void reset()
    {
        setVelocity(Vector2.zero);
        direction = Vector2.zero;
        cameraPos = (Vector2)transform.position;
    }

    public void setTargetVel(Vector2 v)
    {
        if (v == targetVel)
            return;
        targetVel = v;
        delta = (v - vel) * (Time.fixedDeltaTime / 0.1f);
    }

    public void setVelocity(Vector2 v)
    {
        vel = v;
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
