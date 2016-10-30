using UnityEngine;
using System;
using System.Collections;

public abstract class TimelineEvent : MonoBehaviour {

    protected float timeStart;
    protected float timeEnd;
    protected Transform parentTransform;

    public Boolean hasStarted = false;
    public Boolean hasFinished = false;


    protected TimelineEvent(float ti, float tf, Transform parentTransform)
    {
        this.parentTransform = parentTransform;
        setStartTime(ti);
        setEndTime(tf);
    }

    public void setStartTime(float ti)
    {
        timeStart = ti;
    }

    public void setEndTime(float tf)
    {
        timeEnd = tf;
    }

    public float getStartTime()
    {
        return this.timeStart;
    }

    public float getEndTime()
    {
        return this.timeEnd;
    }

    public abstract void onStart();

    public abstract void onFinished();


	

}
