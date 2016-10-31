using UnityEngine;
using System.Collections;

public class DelayedHoming : BulletBehaviour {

    bool homing = true;
    public override void startUp()
    {
        
    }
    protected override void bulletLogicTick()
    {
        b.setAngle(b.velocity);
        if (timeElapsed > 1.0f && homing)
        {
            b.velocity = (b.p.transform.position - b.transform.position).normalized * b.velocity.magnitude;
            homing = false;
        }
    }
}
