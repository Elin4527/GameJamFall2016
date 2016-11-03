using UnityEngine;
using System.Collections;

public class DelayedBullet : BulletBehaviour {

    Vector2 saved;
    public override void startUp()
    {
        b.setAngle(b.velocity);
        saved = b.velocity;
        b.velocity = Vector2.zero;
    }
    protected override void bulletLogicTick()
    {
        if(b.velocity != Vector2.zero)
            b.setAngle(b.velocity);
        if (timeElapsed > 1.5f)
        {
            b.velocity = saved;
        }
    }
}
