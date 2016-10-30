using UnityEngine;
using System.Collections;

public class PlayerGraze: MonoBehaviour {

    Player parent;
    public int points;
	// Use this for initialization
	void Start () {
        parent = GetComponentInParent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>())
        {
            parent.grazeCount++;
            parent.graze = true;
            parent.score += points;
        }
    }
}
