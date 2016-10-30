using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance = null;

    public GameObject [] tileMaps;
    public GameObject [] timelineExecutors;

    private GameObject instantiatedTileMap;
    private GameObject instantiatedTimelineExecutor;


    private int index = -1;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        nextLevel();
    }

    public void nextLevel()
    {
        index++;

        if (index != 0)
        {
            Destroy(instantiatedTileMap);
            Destroy(instantiatedTimelineExecutor);
        }

        if (index != timelineExecutors.Length)
        {
            instantiatedTileMap = Instantiate(tileMaps[index]);
            instantiatedTileMap.transform.parent = transform;
            
            instantiatedTimelineExecutor = Instantiate(timelineExecutors[index]);
            instantiatedTimelineExecutor.transform.parent = transform;
        }
        else
        {
            gameOver();
        }

    }

    

    void gameOver() { }

    public Vector3 convertTileCoords(Vector2 tileCoords)
    {
        Vector3 boardTranslate = instantiatedTileMap.GetComponent<TileMapGen>().getBoardTranslate();
        int rows = instantiatedTileMap.GetComponent<TileMapGen>().rows;

        Vector3 loc = new Vector3(tileCoords.x + boardTranslate.x, boardTranslate.y + tileCoords.y - (rows-1));

        return loc;


    }


}
