using UnityEngine;
using System;
using System.Collections;

public class AttackPattern : MonoBehaviour {

    [Serializable]
    public class AttackEvent
    {
        public GameObject bullet, behaviour;

        public GameObject spawner;
        public float time;
        public int repeat;
        public int round;
        public float cd;
    }
    Enemy e;

    public AttackEvent[] attackEventList;

	// Use this for initialization
	void Start () {
        e = GetComponent<Enemy>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float elapsedTime = Time.fixedDeltaTime;
        for (int i = 0; i < attackEventList.Length; i++)
        {
            AttackEvent a = attackEventList[i];

            a.time -= elapsedTime;
            while (a.time < 0 && (a.repeat == 0 || a.round < a.repeat))
            {
                BulletSpawner s = Instantiate(a.spawner, e.gameObject.transform.position, Quaternion.identity) as BulletSpawner;
                s.gameObject.transform.SetParent(e.transform);
                s.prefab = a.bullet;
                s.behaviour = a.behaviour;
                
                s.track = e.p.gameObject;
                a.round++;
                a.time += a.cd;
            }
        }
	}
}
