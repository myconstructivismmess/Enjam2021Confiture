using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob : MonoBehaviour
{
    protected int hp;
    protected int dmg;
    protected float speed;

    public Vector2 direction;
    protected Rigidbody2D mobRb;

    protected void InitMovement()
    {
        mobRb = GetComponent<Rigidbody2D>();
        direction = Random.Range(0,2) == 0 ? Vector2.right : Vector2.left;
    }

    public void ChangeDirection()
    {
        direction.x *= -1;
        mobRb.AddForce(direction * speed * 2);
    }
    protected abstract void DetectPlayer();
    protected abstract void Attack();

}
