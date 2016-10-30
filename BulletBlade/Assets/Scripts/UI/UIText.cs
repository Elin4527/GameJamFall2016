using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIText : MonoBehaviour {

    public Player p;
    Text t;

	// Use this for initialization
	void Start () {
        t = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        t.text = "SCORE: \t" + p.score + "\n\nLIVES: \t" + p.lives + "\n\nPOWER: \t" + p.power + "\n\nGRAZE: \t" + p.grazeCount;
	}
}
