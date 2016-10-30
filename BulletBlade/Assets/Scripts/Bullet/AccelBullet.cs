using UnityEngine;
using System.Collections;

public class AccelBullet : BulletBehaviour
{
    public override void startUp()
    {
        b.acceleration = b.velocity;
    }
    protected override void bulletLogicTick()
    {
        b.setAngle(b.velocity);
    }
}
