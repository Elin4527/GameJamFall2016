using UnityEngine;
using System.Collections;
using System;

public class NextLevelEvent : TimelineEvent
{

    public override void onStart()
    {
        LevelManager.instance.nextLevel();
    }
}
