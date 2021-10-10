using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PrideBoss : Boss
{
    ParticleSystem.Particle[] particles = new ParticleSystem.Particle[6];
    int particlesAlive = 0;

    public GameObject sword;
    public GameObject[] tmpSwords;

    private PolygonCollider2D cleaveCollider;
    private CircleCollider2D fireCollider;

    private ParticleSystem[] bossParticles;

    public Vector3 registeredPlayerPos;

    bool launched = false;
    bool launched2 = false;
    bool launched3 = false;
    bool cleave = false;
    // Start is called before the first frame update
    void Start()
    {
        hp = 1000;
        speed = 100f;
        cleaveCollider = transform.GetChild(0).GetComponent<PolygonCollider2D>();
        fireCollider = transform.GetChild(1).GetComponent<CircleCollider2D>();
        bossParticles = new ParticleSystem[5];
        for(int i=2; i<=6; i++) bossParticles[i-2] = transform.GetChild(i).GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();

        patternTimer += Time.deltaTime;
        
        switch (attackIndex)
        {
            // Lancement des épées à tête chercheuses
            case 1: {
                    if(!launched)
                    {
                        bossParticles[0].Play();
                        patternTimer = 0;
                        launched = true;
                    }
                    if (!launched2 && patternTimer >= 2f)
                    {
                        particlesAlive = bossParticles[0].GetParticles(particles);
                        tmpSwords = new GameObject[particlesAlive];
                        for (int i = 0; i < particlesAlive; i++) tmpSwords[i] = Instantiate(sword, particles[i].position, transform.rotation);
                        
                        patternTimer = 0;
                        launched2 = true;
                    }
                    break;
            }
            // Lancement de l'attaque Cleave
            case 2: {
                    if (Vector2.Distance(transform.position, player.transform.position) > 3  && !cleave)
                    {
                        bossRb.velocity = direction * (speed * 10) * Time.deltaTime;
                        
                        patternTimer = 0;
                    }
                    else if(!cleave)
                    {
                        bossParticles[1].Play();
                        bossRb.velocity = Vector2.zero;
                        cleave = true;
                    }
                    break;
            }
            case 3: {
                    if(Vector2.Distance(transform.position, player.transform.position ) <= 5)
                    {
                        bossRb.velocity = -direction * (speed * 10) * Time.deltaTime;
                    }
                    else if (!launched3)
                    {
                        bossRb.velocity = Vector2.zero;
                        for (int i = 2; i < 5; i++) bossParticles[i].Play();
                        fireCollider.enabled = true;
                        patternTimer = 0;
                        launched3 = true;
                    }
                    else if (patternTimer >= 5)
                    {
                        launched3 = false;
                        fireCollider.enabled = false;
                        attackIndex = 1;
                    }
                    break;
            }
        }

        if (launched)
        {
            if (patternTimer >= 2f)
            {
                foreach (GameObject s in tmpSwords)
                {
                    if (s != null) s.GetComponent<Rigidbody2D>().AddForce((registeredPlayerPos - s.transform.position).normalized * 4);
                }
            }
            else
            {
                foreach (GameObject s in tmpSwords)
                {
                    if (s != null) s.transform.rotation = LookAt(s.transform.position);
                }
                registeredPlayerPos = player.transform.position;
            }
            if (patternTimer >= 4f)
            {
                launched = false;
                launched2 = false;
                patternTimer = 0;
                attackIndex = 2;
            }
        }

        if(cleave)
        {
            //Todo animation
            if (patternTimer >= 0.5f) cleaveCollider.enabled = true;
            if (patternTimer >= 1f) cleaveCollider.enabled = false;
            if (patternTimer >= 3f)
            {
                cleave = false;
                attackIndex = 3;
            }
        }
    }

    Quaternion LookAt(Vector3 pos)
    {
        var dir = player.transform.position - pos;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Player")) collider.transform.GetComponent<Player>().Hit(2,true);
    }
}
