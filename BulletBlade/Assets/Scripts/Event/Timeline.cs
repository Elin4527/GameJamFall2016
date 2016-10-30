using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class Timeline : MonoBehaviour {
   
    [Serializable]
    public class SpawnEnemyEventField
    {
        public string name;
        public float startTime;
        public Vector2 tileCoords;
        public GameObject enemy;
        public GameObject attack;
        public float despawn;
        // public AttackPattern;

        public SpawnEnemyEventField(float startTime, Vector2 tileCoords, GameObject enemy, GameObject attack, float despawn)
        {
            this.startTime = startTime;
            this.tileCoords = tileCoords;
            this.enemy = enemy;
            this.attack = attack;
            this.despawn = despawn;
        }
    }

    public SpawnEnemyEventField [] spawnEnemyEvents;

    public SpawnEnemyEventField bossSpawnEvent;
    private BossSpawnEvent bossEvent;

    public float nextLevelDelay;
    private NextLevelEvent nextLevelEvent;

    private List<TimelineEvent> queuedEvents = new List<TimelineEvent>();

    private bool imminentExit;



    private float clock = 0f;

    // Awake is called on init;

    public void addEventToQueue(TimelineEvent ev){
        if (ev.getStartTime() > clock)
        {
            queuedEvents.Add(ev);
        }
    }
    
    void Awake()
    {

        foreach (SpawnEnemyEventField spawnEnemyEvent in spawnEnemyEvents)
        {
            addEventToQueue(constructSpawnEnemyEvent(spawnEnemyEvent));
        }

        bossEvent = constructBossSpawnEvent(bossSpawnEvent);

        addEventToQueue(bossEvent);

        nextLevelEvent = ScriptableObject.CreateInstance("NextLevelEvent") as NextLevelEvent;

    }

    SpawnEnemyEvent constructSpawnEnemyEvent(SpawnEnemyEventField f)
    {
        SpawnEnemyEvent cons = ScriptableObject.CreateInstance("SpawnEnemyEvent") as SpawnEnemyEvent;
        cons.init(f.startTime, f.despawn, this.transform, f.tileCoords, f.enemy, f.attack);

        return cons;
    }

    BossSpawnEvent constructBossSpawnEvent(SpawnEnemyEventField f)
    {
        BossSpawnEvent cons = ScriptableObject.CreateInstance("BossSpawnEvent") as BossSpawnEvent;
        cons.init(f.startTime, f.despawn, this.transform, f.tileCoords, f.enemy, f.attack);

        return cons;
    }



    // Update is called once per frame
    void FixedUpdate () {


       this.clock += Time.deltaTime;


        foreach (TimelineEvent queuedEvent in queuedEvents) {

            if (!queuedEvent.hasExecuted && clock > queuedEvent.getStartTime())
            {
                queuedEvent.hasExecuted = true;
                queuedEvent.onStart();
            }
        }

        if (bossEvent.hasExecuted && bossEvent.getBoss() == null && !imminentExit)
        {
            imminentExit = true;
            nextLevelEvent.init(clock + nextLevelDelay, this.transform);
            addEventToQueue(nextLevelEvent);
        }
    }
}
