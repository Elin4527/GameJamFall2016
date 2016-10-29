using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour {

    Player parent;
	// Use this for initialization
	void Start () {
        parent = GetComponentInParent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (parent.isTangible() && collision.gameObject.GetComponent<Bullet>())
        {
            Destroy(collision.gameObject);
            parent.kill();
        }
    }
}
