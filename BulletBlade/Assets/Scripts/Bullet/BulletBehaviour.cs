using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Bullet))]
public abstract class BulletBehaviour : MonoBehaviour {

    protected float timeElapsed;
    protected Bullet b;
	// Use this for initialization
	void Start () {
        b = GetComponent<Bullet>();
	}
	
	void FixedUpdate () {
        timeElapsed += Time.deltaTime;
        bulletLogicTick();
	}

    public abstract void startUp();

    protected abstract void bulletLogicTick();
}
