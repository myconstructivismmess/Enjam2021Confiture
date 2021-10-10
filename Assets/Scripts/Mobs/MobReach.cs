using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class MobReach : Mob
{
    /** R�f�rence aus pr�fab du projectile. */
    public GameObject projectile;
    /** Position d'o� partent les projectiles. */
    private Transform firePoint;
    // Initialisation des valeurs propres au mob � distance.
    void Start()
    {
        hp = 2;
        dmg = 20;
        speed = 25f;
        reach = 8f;
        mobRb.AddForce(direction * speed);
        firePoint = transform.GetChild(0);
        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _onDeath = new UnityEvent();

        HeartList = new List<Transform>();
        for (int i = 0; i < hp; i++)
        {
            HeartList.Add(Instantiate(HeartPrefab, HealthBar.transform).transform);
        }
    }

    private void Update()
    {
        tmpTimer += Time.deltaTime;

        //Si le joueur est � bonne distance.
        if (DetectPlayer())
        {
            // Lancement de l'attaque.
            if(!attacking)
            {
                attacking = true;
                mobRb.AddForce(-direction * speed);
            }
        }

        // Fin de l'attaque
        if (attacking && tmpTimer >= 2f)
        {
            tmpTimer = 0;
            attacking = false;
            if (!DetectPlayer()) mobRb.AddForce(direction * speed);
            // Si le joueur est toujours � port�e on relance l'attaque.
            else attacking = true;
            // Lancement du projectile.
            
            Debug.Log("Pew Pew attack !");
            GameObject tmpPew = Instantiate(projectile, firePoint.position, firePoint.rotation);
            tmpPew.GetComponent<Rigidbody2D>().AddForce(direction * 125f);
        }

        // Check health an invoke death event
        if (hp <= 0 && _isAlive)
        {
            _onDeath?.Invoke();
            Blink(_numberOfBlink, _blinkDuration, _renderer);
            ClearCheck.Instance.mobList.Remove(transform);
            _isAlive = false;
        }
    }
}
