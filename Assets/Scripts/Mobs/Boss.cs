using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    protected int hp = 30;
    protected float speed = 2f;

    protected float patternTimer = 0f;
    protected int attackIndex = 1;

    public GameObject player;
    protected Rigidbody2D bossRb;
    protected Vector2 direction = Vector2.left;

    void Awake()  {  bossRb = GetComponent<Rigidbody2D>(); }

    public void Hit(int dmg) { hp -= dmg; Debug.Log("AAAAH"); }

    protected void LookAtPlayer()
    {
        if (transform.position.x > player.transform.position.x)
        {
            direction = Vector2.left;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            direction = Vector2.right;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

}
