using UnityEngine;
using System.Collections;

public class PlayerHitbox : MonoBehaviour {

    Player p;
    // Use this for initialization
    void Start()
    {
        p = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (p.isTangible() && collision.gameObject.GetComponent<Bullet>())
        {
            Destroy(collision.gameObject);
            if (gameObject.activeSelf && p.state != Player.ActionState.DYING)
            {
                p.kill();
            }
        }
    }
}
