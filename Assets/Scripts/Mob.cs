using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob : MonoBehaviour
{
    // Stats du mob.
    protected int hp;
    protected int dmg;
    protected float speed;
    // Stats changeantes.
    protected bool attacking;
    protected float reach;

    // Attributs de mouvements.
    public Vector2 direction;
    protected Rigidbody2D mobRb;
    // Timer WIP.
    protected float tmpTimer = 0f;

    // Initialisation des mouvements aléatoirement.
    void Awake()
    {
        mobRb = GetComponent<Rigidbody2D>();
        direction = Random.Range(0, 2) == 0 ? Vector2.right : Vector2.left;
        ChangeDirection();
    }

    // Méthode appelée pour changer de direction en cas d'obstacle ou de fin de plateforme.
    public void ChangeDirection()
    {
        direction.x *= -1;
        mobRb.AddForce(direction * speed * 2);
        if (direction == Vector2.left) transform.eulerAngles = new Vector3(0, 180, 0);
        else transform.eulerAngles = new Vector3(0, 0, 0);
    }

    // Méthode appelée pour détecter un joueur en face du mob.
    protected bool DetectPlayer()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast((Vector2)transform.position, direction, reach, LayerMask.NameToLayer("MobLayer"));
        if (hitInfo.collider == null) return false;
        else return hitInfo.collider.transform.CompareTag("Player");
    }
}
