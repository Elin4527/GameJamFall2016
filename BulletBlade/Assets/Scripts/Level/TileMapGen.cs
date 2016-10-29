using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class TileMapGen : MonoBehaviour
{

    public TextAsset textFile;
    int [][] map;
    int columns;
    int rows;

    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    private Transform boardHolder;


    void Awake()
    {
        map = parseTextMap(textFile.text);

        boardSetup();

    }

    int [] [] parseTextMap(String textMap)
    {

        string[] lines = textMap.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

        string[][] stringMap = new string[lines.Length][];

       

        for (int y=0; y< lines.Length; y++)
        {
            stringMap[y] = lines[y].Split(null);
        }


        rows = stringMap.Length;
        columns = stringMap[0].Length;


        int[][] numMap = new int[rows][];

        for (int y=0; y<numMap.Length; y++)
        {
            numMap[y] = new int[columns];
        }
        


        for (int y=0; y < stringMap.Length; y++)
        {
            for (int x=0; x<stringMap[0].Length; x++)
            {
                numMap[y][x] = int.Parse(stringMap[y][x]);
            }
        }

        return numMap;
    }

    void boardSetup()
    {


        boardHolder = new GameObject("Board").transform;

        GameObject toInstantiate;

        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {

                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    toInstantiate = wallTiles[Random.Range(0, wallTiles.Length)];

                }

                else if (map[y][x]== 1)
                {
                    toInstantiate = wallTiles[Random.Range(0, floorTiles.Length)];
                }

                else
                {
                    toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, -y, 0.0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);

            }

        }
    }

}
