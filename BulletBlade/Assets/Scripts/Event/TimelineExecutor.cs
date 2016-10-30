using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class TimelineExecutor : MonoBehaviour {
   
    [Serializable]
    public class SpawnEnemyEventField
    {
        public string name;
        public float startTime;
        public float endTime;
        public Vector3 spawnPos;
        public GameObject enemy;

        public SpawnEnemyEventField(float ti, float tf, Vector3 spawnPos, GameObject enemy)
        {
            this.startTime = ti;
            this.startTime = tf;
            this.spawnPos = spawnPos;
            this.enemy = enemy;
        }
    }

    public SpawnEnemyEventField [] spawnEnemyEvents;

    private List<TimelineEvent> queuedEvents = new List<TimelineEvent>();
    private List<TimelineEvent> eventsInProgress = new List<TimelineEvent>();

    private float clock = 0f;

    // Awake is called on init;
    void Awake()
    {

        foreach (SpawnEnemyEventField spawnEnemyEvent in spawnEnemyEvents)
        {
            queuedEvents.Add(constructSpawnEnemyEvent(spawnEnemyEvent));
        }

//        queuedEvents.Sort((ev1, ev2) => ev1.getStartTime().CompareTo(ev2.getStartTime()));

        

    }

    SpawnEnemyEvent constructSpawnEnemyEvent(SpawnEnemyEventField f)
    {
        SpawnEnemyEvent cons = new SpawnEnemyEvent(f.startTime, f.endTime, this.transform, f.spawnPos, f.enemy);
        return cons;
    }


    // Update is called once per frame
    void FixedUpdate () {

       this.clock += Time.deltaTime;


        for (int i =0; i< queuedEvents.Count; i++) {

            if (!queuedEvents[i].hasStarted && clock > queuedEvents[i].getStartTime() )
            {
                queuedEvents[i].hasStarted = true;
                queuedEvents[i].onStart();
                eventsInProgress.Add(queuedEvents[i]);

            }
        }

        for (int i=0; i< eventsInProgress.Count; i++)
        {

            if (!eventsInProgress[i].hasFinished && clock > eventsInProgress[i].getEndTime())
            {
                eventsInProgress[i].onFinished();
                eventsInProgress[i].hasFinished = true;

            }
        }

    }




}
