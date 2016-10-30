using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

    public int damage;
    Animator a;

	// Use this for initialization
	void Start () {
        a = gameObject.GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	void Update () {
        Destroy(gameObject, a.GetCurrentAnimatorClipInfo(0)[0].clip.length - 0.01f);
    }


}
