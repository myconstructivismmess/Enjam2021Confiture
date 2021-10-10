using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CrueltyBoss : Boss
{
    ParticleSystem.Particle[] particles = new ParticleSystem.Particle[6];
    int particlesAlive = 0;

    public GameObject boulder;
    protected List<GameObject> boulderList;
    protected float boulderSpawnX;

    public GameObject boulderLauncher;
    public GameObject boulderLauncherMin;
    public GameObject boulderLauncherMax;
    private int _boulderNumber;

    private PolygonCollider2D cleaveCollider;

    bool launched = false;
    bool launched2 = false;

    bool cleave = false;

    // Start is called before the first frame update
    void Start()
    {
        hp = 1000;
        speed = 100f;
        boulderList = new List<GameObject>();


    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();

        patternTimer += Time.deltaTime;
        
        if (patternTimer < 5f) return;
        
        patternTimer = 0;

        switch (attackIndex)
        {
            // Apparition des boulders du plafond
            case 1:
            {
                _boulderNumber = 6;
                SpawnBoulders();
                attackIndex++;
                break;
            }
        }
    }

    private void SpawnBoulders()
    {
        if(_boulderNumber > 0)
        {
            _boulderNumber--;
            boulderSpawnX = Random.Range(boulderLauncherMin.transform.position.x, boulderLauncherMax.transform.position.x);
            boulderList.Add(Instantiate(boulder, new Vector2(boulderSpawnX, boulderLauncherMin.transform.position.y), Quaternion.identity, boulderLauncher.transform ));
            Invoke("SpawnBoulders", 0.0f);
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