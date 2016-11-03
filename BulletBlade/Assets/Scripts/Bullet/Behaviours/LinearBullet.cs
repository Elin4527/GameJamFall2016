using UnityEngine;
using System.Collections;
using System;

public class LinearBullet : BulletBehaviour {
    public override void startUp()
    {
    }
    protected override void bulletLogicTick()
    {
        b.setAngle(b.velocity);
    }
}
