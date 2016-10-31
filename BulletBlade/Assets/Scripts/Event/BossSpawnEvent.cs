using UnityEngine;
using System.Collections;

public class BossSpawnEvent : SpawnEnemyEvent {

    public GameObject getBoss()
    {
        return enemy;
    }

}