using UnityEngine;
using System.Collections;
using System;


[Serializable]
public class SpawnEnemyEvent : TimelineEvent {

    public float startTime;
    public float endTime;
    public Vector3 spawnPos;
    public GameObject enemy;

    private GameObject clonedEnemy;




    public SpawnEnemyEvent(float ti, float tf, Transform parentTransform, Vector3 spawnPos, GameObject enemy) 
        : base(ti, tf, parentTransform)
    {
        this.spawnPos = spawnPos;
        this.enemy = enemy;
    }

    public override void onStart()
    {
        clonedEnemy = Instantiate(enemy, parentTransform) as GameObject;
        clonedEnemy.transform.Translate(spawnPos);

    }

    public override void onFinished()
    {
        // leaveScreen(); or // despawn(); etc...

    }

   
}
