using UnityEngine;
using System.Collections;

public class AngledBullet : BulletBehaviour {

    float turned = 45;
    Vector2 angle;
    public override void startUp()
    {
        float w = turned * Mathf.PI / 180;
        float sin = Mathf.Sin(w), cos = Mathf.Cos(w);

        b.velocity = new Vector2(cos * b.velocity.x + sin * b.velocity.y, -sin * b.velocity.x + cos * b.velocity.y);
    }
    protected override void bulletLogicTick()
    {
        b.setAngle(b.velocity);
    }

}
