using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public Vector2 velocity;
    public Vector2 acceleration;
    public Vector2 maxVelocity;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = velocity;
	}

    void FixedUpdate()
    {
        rb.velocity += acceleration * Time.fixedDeltaTime;
    }

	// Update is called once per frame
	void Update () {
        rb.rotation = Vector2.Angle(new Vector2(0, 0), rb.velocity);
	}
}
