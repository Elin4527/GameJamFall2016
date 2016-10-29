using UnityEngine;
using System.Collections;

public class YSort : MonoBehaviour {

    SpriteRenderer spr;
	// Use this for initialization
	void Start () {
        spr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LateUpdate()
    {
        spr.sortingOrder = -(int)(transform.position.y * 1000.0f);
    }
}
