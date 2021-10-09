using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob : MonoBehaviour
{
    protected int hp;
    protected int dmg;
    protected float speed;

    protected bool attacking;

    public Vector2 direction;
    protected Rigidbody2D mobRb;

    protected float tmpTimer = 0f;

    protected void InitMovement()
    {
        mobRb = GetComponent<Rigidbody2D>();
        direction = Random.Range(0,2) == 0 ? Vector2.right : Vector2.left;
    }

    public void ChangeDirection()
    {
        direction.x *= -1;
        mobRb.AddForce(direction * speed * 2);
        transform.eulerAngles = new Vector3(0, 180, 0);
    }
}
