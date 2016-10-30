using UnityEngine;
using System.Collections;

public class BossSpawnEvent : SpawnEnemyEvent {

    private GameObject boss;

	public new void init(float startTime, Transform parentTransform, Vector2 tileCoords, GameObject enemy)
    {
        base.init(startTime, parentTransform, tileCoords, enemy);


    }

    public new void onStart()
    {
        enemy.transform.Translate(LevelManager.instance.convertTileCoords(tileCoords));
        boss = Instantiate(enemy, parentTransform) as GameObject;

    }

    public GameObject getBoss()
    {
        return boss;
    }
	
	
    
}
