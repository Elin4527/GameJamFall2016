using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

    public GameObject levelManager;

    // Use this for initialization
    void Awake()
    {

        if (LevelManager.instance == null)
        {
            Instantiate(levelManager);
        }
    }
}
