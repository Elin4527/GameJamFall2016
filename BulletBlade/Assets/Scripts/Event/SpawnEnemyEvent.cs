using UnityEngine;
using System.Collections;
using System;


[Serializable]
public class SpawnEnemyEvent : TimelineEvent {

    public float startTime;
    public float endTime;
    public Vector3 tileCoords;
    public GameObject enemy;
    public GameObject att;

    // public AttackPattern attackPattern;

    public void init(float ti, float de, Transform parentTransform, Vector2 tileCoords, GameObject e, GameObject attack) 
    {
        base.init(ti, parentTransform);

        this.tileCoords = tileCoords;
        enemy = e;
        endTime = de;
        att = attack;
    }

    public override void onStart()
    {
        enemy.transform.position = LevelManager.instance.convertTileCoords(tileCoords);
        Enemy i = Instantiate(enemy).GetComponent<Enemy>();
        i.despawnTime = endTime;
        GameObject a = Instantiate(att) as GameObject;
        a.transform.SetParent(i.transform);
    }
}
