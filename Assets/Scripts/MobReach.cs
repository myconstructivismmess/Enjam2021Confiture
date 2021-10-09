using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobReach : Mob
{
    private float reach = 10f;
    public GameObject projectile;

    private Transform firePoint;

    private void Awake() { InitMovement(); }

    // Start is called before the first frame update
    void Start()
    {
        hp = 2;
        dmg = 3;
        speed = 25f;
        mobRb.AddForce(direction * speed);
        firePoint = transform.GetChild(0);
    }

    private void Update()
    {
        tmpTimer += Time.deltaTime;

        RaycastHit2D hitInfo = Physics2D.Raycast((Vector2)transform.position, direction, reach, LayerMask.NameToLayer("MobLayer"));
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.transform.CompareTag("Player") && !attacking)
            {
                attacking = true;
                mobRb.AddForce(-direction * speed);

                Debug.Log("Pew Pew attack !");
                GameObject tmpPew = Instantiate(projectile, firePoint.position, firePoint.rotation);
                tmpPew.GetComponent<Rigidbody2D>().AddForce(direction * 125f);
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
