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

    protected override void Attack()
    {
       // Todo lancer l'animation + enable le collider d'attaque.
    }

    protected override void DetectPlayer()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast((Vector2)transform.position, direction, 1f);
        if (hitInfo.collider != null) if (hitInfo.collider.transform.CompareTag("Player")) Attack();
    }
}
