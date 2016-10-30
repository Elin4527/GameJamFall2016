using UnityEngine;
using System.Collections;

public class PlayerGraze: MonoBehaviour {

    Player parent;
	// Use this for initialization
	void Start () {
        parent = GetComponentInParent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void graze()
    {
        parent.graze = true;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>())
        {
            parent.grazeCount++;
            parent.graze = true;
        }
    }
}
