using UnityEngine;
using System.Collections;
using System;

public class Enemy : BaseCharacter {

    public enum EnemyAI
    {
        STATIC, TRACKING
    }
    public EnemyAI type;
    public BaseCharacter following;
    public float distance;
    public int maxHealth = 5;
    public int currHealth;

    public override Vector2 getInput()
    {
        if (type == EnemyAI.TRACKING && following)
        {
            // Go towards a player if further than following distance
            direction = following.GetComponent<Rigidbody2D>().position - rb.position;
            Debug.Log(direction);
            if (direction.magnitude > distance)
            {
                return direction.normalized * speed;
            }
        }

        return Vector2.zero;

    }

    public override void logic()
    {
        
    }
}
