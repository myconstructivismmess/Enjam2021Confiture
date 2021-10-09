using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMelee : Mob
{
    private void Awake() { InitMovement(); }

    // Start is called before the first frame update
    void Start()
    {
        hp = 3;
        dmg = 2;
        speed = 50f;
        mobRb.AddForce(direction * speed);
    }

    void Update()
    {
        tmpTimer += Time.deltaTime;
        //Debug.Log(attacking +" " + tmpTimer);

        RaycastHit2D hitInfo = Physics2D.Raycast((Vector2)transform.position, direction, 0.2f, LayerMask.NameToLayer("MobLayer"));
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.transform.tag == "Player" && !attacking)
            {
                attacking = true;
                mobRb.AddForce(-direction * speed);

                Debug.Log("Claw attack !");

                
            }
        }

        if (tmpTimer >= 2f && attacking)
        {
            tmpTimer = 0;
            attacking = false;
            mobRb.AddForce(direction * speed);
        }
    }
}
