using UnityEngine;
using System.Collections;
using System;


[Serializable]
public class SpawnEnemyEvent : TimelineEvent {

    public float startTime;
    public float endTime;
    public Vector3 tileCoords;
    public GameObject enemy;
    // public AttackPattern attackPattern;

    public void init(float ti, Transform parentTransform, Vector2 tileCoords, GameObject enemy) 
    {
        base.init(ti, parentTransform);

        this.tileCoords = tileCoords;
        this.enemy = enemy;
    }

    public override void onStart()
    {
        enemy.transform.position = LevelManager.instance.convertTileCoords(tileCoords);
        Instantiate(enemy);

    }
}
