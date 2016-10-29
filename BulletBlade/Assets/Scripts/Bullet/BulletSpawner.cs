using UnityEngine;
using System.Collections;

public class BulletSpawner : MonoBehaviour {

    public int bulletCount;
    public float delay;
    public float cooldown;
    public float coneAngle;
    public float speed;
    public float distance;
    public Vector2 direction;
    public GameObject track;
    public bool dynamic;
    public bool clockwise;
    public bool total;
    public int times;

    float wait;
    public float timeRemaining = 0;
    Vector2 angle;
    public GameObject prefab;
    public RuntimeAnimatorController anim;
    bool firing = false;
    int shotCount;
    public int round = 0;

	// Use this for initialization
	void Start () {
	}

    void turnAngle()
    {
        if (coneAngle == 0) return;
        float w = (clockwise ? 1: -1) * coneAngle * Mathf.PI / 180 / bulletCount;
        float sin = Mathf.Sin(w), cos = Mathf.Cos(w);
        angle = new Vector2(cos * angle.x + sin * angle.y, -sin * angle.x + cos * angle.y);
    }
	
	// Update is called once per frame
	void Update () {
        if (!firing)
        {
            timeRemaining -= Time.deltaTime;
        }
        if (timeRemaining <= 0)
        {
            // Set initial firing variables based on public variables
            firing = true;
            timeRemaining = cooldown;
            if (track)
            {
                angle = track.transform.position - transform.position;
            }
            else
            {
                angle = direction;
            }
            shotCount = 0;
            wait = total ? (delay / bulletCount) : delay; ;
        }

        if (firing)
        {
            // Instant firing
            if (delay <= 0)
            {
                for (int i = 0; i < bulletCount; i++)
                {
                    fireBullet();
                    turnAngle();
                }
                firing = false;
            }
            // Shoot individual by delay    
            else {
                wait += Time.deltaTime;
                float t = total ? (delay / bulletCount) : delay;
                while (wait > t && firing)
                {
                    wait -= t;
                    fireBullet();
                    firing = ++shotCount < bulletCount;
                    if (track && dynamic)
                    {
                        angle = track.transform.position - transform.position;
                    }
                    else turnAngle();
                }
            }

            if (!firing)
            {
                if (times != 0 && ++round >= times) Destroy(gameObject);
            }
        }
	}

    void fireBullet()
    {
        angle = angle.normalized;
        GameObject g = (GameObject)Instantiate(prefab, transform.position + (Vector3) angle * distance, Quaternion.Euler(0, 0, Bullet.convertAngle(angle)));
        Bullet b = g.GetComponent<Bullet>();
        b.velocity = speed * angle;
        
        b.GetComponent<BulletBehaviour>().startUp();
        b.GetComponent<Animator>().runtimeAnimatorController = anim;
    }
}
