using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ManaBar : MonoBehaviour {

    public Player player;
    public Color low;
    public Color medLow;
    public Color med;
    public Color medHigh;
    public Color high;
    float full;
    RectTransform rt;
    Image i;

    void Start () {
        rt = GetComponent<RectTransform>();
        full = rt.localScale.x;
        i = GetComponent<Image>();
	}
	
	void Update () {
        
        rt.localScale = new Vector3(((float)player.mana) / ((float)player.maxMana) * full, rt.localScale.y , rt.localScale.z);
        if (player.mana == player.maxMana)
            i.color = high;
        else if (player.mana > player.manaCost * 3)
            i.color = medHigh;
        else if (player.mana > player.maxMana / 2)
            i.color = med;
        else if (player.mana > player.manaCost)
            i.color = medLow;
        else
            i.color = low;
	}
}
