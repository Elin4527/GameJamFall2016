using UnityEngine;
using System;
using System.Collections;

public abstract class TimelineEvent : ScriptableObject {

    protected float timeStart;
    protected Transform parentTransform;

    public Boolean hasExecuted = false;

    public void init(float ti, Transform parentTransform)
    {
        this.parentTransform = parentTransform;
        setStartTime(ti);
    }

    public void setStartTime(float ti)
    {
        timeStart = ti;
    }

    public float getStartTime()
    {
        return this.timeStart;
    }

    public abstract void onStart();


	

}
