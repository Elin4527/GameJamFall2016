using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ManaBar : MonoBehaviour {

    public Player player;
    public Color low;
    public Color medium;
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
        if (player.mana < player.manaCost)
        {
            i.color = low;
        }
        else if (player.mana < player.maxMana)
        {
            i.color = medium;
        }
        else
        {
            i.color = high;
        }
	}
}
