using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MobMelee : Mob
{
    /** Collider qui détecte si l'attaque du mob a touché. */
    Collider2D clawCollider;

    // Initialisation des valeurs propres au mob de mêlée.
    void Start()
    {
        hp = 3;
        dmg = 2;
        speed = 50f;
        reach = 0.4f;
        mobRb.AddForce(direction * speed);
        clawCollider = transform.GetChild(0).GetComponent<Collider2D>();
        clawCollider.enabled = false;
        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _onDeath = new UnityEvent();
        
        HeartList = new List<Transform>();
        for (int i = 0; i < hp; i++)
        {
            HeartList.Add(Instantiate(HeartPrefab, HealthBar.transform).transform);

        }
    }

    void Update()
    {

        tmpTimer += Time.deltaTime;

        //Si le joueur est à bonne distance.
        if (DetectPlayer())
        {
            // Lancement de l'attaque.
            if (!attacking)
            {
                attacking = true;
                mobRb.AddForce(-direction * speed);
            }
        }

        if (attacking)
        {
            // On laisse 0.5 secondes au joueur pour esquiver, puis on active le collider d'attaque.
            if (tmpTimer >= 0.5f) clawCollider.enabled = true;
            if (tmpTimer >= 1f) clawCollider.enabled = false;

            // Fin de l'attaque
            if (tmpTimer >= 2f)
            {
                tmpTimer = 0;
                attacking = false;
                if (!DetectPlayer()) mobRb.AddForce(direction * speed);
                // Si le joueur est toujours à portée on relance l'attaque.
                else attacking = true;
            }
        }
        
        if (hp <= 0 && _isAlive)
        {
            _onDeath?.Invoke();
            Blink(_numberOfBlink, _blinkDuration);
            _isAlive = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Player")) collider.transform.GetComponent<DummyPlayer>().Hit(dmg);
    }
}
