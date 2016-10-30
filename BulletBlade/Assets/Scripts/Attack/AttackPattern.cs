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
        public float offset;
        public bool track;
        public Color c = Color.white;
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
                GameObject g = Instantiate(a.spawner, e.gameObject.transform.position, Quaternion.identity) as GameObject;
                g.transform.SetParent(e.transform);
                BulletSpawner s = g.GetComponent<BulletSpawner>();


                s.prefab = a.bullet;
                s.behaviour = a.behaviour;
                s.clr = a.c;
                if(a.track) s.track = e.p.gameObject;
                s.offset += a.offset;
                a.round++;
                a.time += a.cd;
            }
        }
	}
}
