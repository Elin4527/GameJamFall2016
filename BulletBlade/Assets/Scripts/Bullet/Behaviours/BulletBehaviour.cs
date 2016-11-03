using UnityEngine;
using System.Collections;

public abstract class BulletBehaviour : MonoBehaviour {

    protected float timeElapsed;
    protected Bullet b;

	// Use this for initialization
	void Start () {
        b = GetComponent<Bullet>();
        startUp();
	}
	
	void FixedUpdate () {
        timeElapsed += Time.deltaTime;
        bulletLogicTick();
	}

    public abstract void startUp();

    protected abstract void bulletLogicTick();
}
