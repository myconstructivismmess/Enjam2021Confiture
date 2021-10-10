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

    public Vector3 registeredPlayerPos;

    bool launched = false;
    bool launched2 = false;
    bool cleave = false;
    // Start is called before the first frame update
    void Start()
    {
        hp = 1000;
        speed = 100f;
        cleaveCollider = transform.GetChild(0).GetComponent<PolygonCollider2D>();
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
                        bossParticles.Play();
                        patternTimer = 0;
                        launched = true;
                    }
                    if (!launched2 && patternTimer >= 2f)
                    {
                        particlesAlive = bossParticles.GetParticles(particles);
                        tmpSwords = new GameObject[particlesAlive];
                        for (int i = 0; i < particlesAlive; i++) tmpSwords[i] = Instantiate(sword, particles[i].position, transform.rotation);
                        
                        patternTimer = 0;
                        launched2 = true;
                    }
                    break;
                }
            // Lancement de l'attaque Cleave
            case 2: {
                    if (Vector2.Distance(transform.position, player.transform.position) > 3 && !cleave)
                    {
                        bossRb.velocity = direction * (speed * 10) * Time.deltaTime;
                        patternTimer = 0;
                    }
                    else
                    {
                        bossRb.velocity = Vector2.zero;
                        cleave = true;
                    }
                    break;
                }
            case 3: attackIndex = 1; break;
        }

        if (launched)
        {
            if (patternTimer >= 2f)
            {
                foreach (GameObject s in tmpSwords)
                {
                    if (s != null) s.GetComponent<Rigidbody2D>().AddForce(((registeredPlayerPos - s.transform.position).normalized) * 2);
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
        Debug.Log("BAM CLEAVE");
        if (collider.transform.CompareTag("Player")) collider.transform.GetComponent<DummyPlayer>().Hit(5);
    }
}
