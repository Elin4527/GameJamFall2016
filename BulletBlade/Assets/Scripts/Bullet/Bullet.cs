using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public Rect bounds;
    public Vector2 velocity;
    public Vector2 acceleration;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = velocity;
        rb.rotation = convertAngle(rb.velocity);
    }

    void FixedUpdate()
    {
        velocity += acceleration * Time.fixedDeltaTime;
        rb.velocity = velocity;
        if (!bounds.Contains(rb.position))
            Destroy(gameObject);
    }

    public void setAngle(Vector2 a)
    {
        rb.rotation = convertAngle(a);
    }

    static public float convertAngle(Vector2 a)
    {
        return ((a.x < 0) ? -1 : 1) * Vector2.Angle(Vector2.down, a);
    }

	// Update is called once per frame
	void Update () {
	}

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.GetComponent<Wall>())
        {
            Destroy(gameObject);
        }
    }
}
