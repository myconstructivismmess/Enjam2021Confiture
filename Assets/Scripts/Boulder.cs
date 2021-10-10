using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : Projectile
{
    private float killLimit = 5.0f;
    private float killTimer = 0.0f;

    public Collision2D collision;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        killTimer += Time.deltaTime;
        
        if (killTimer >= killLimit)
            Destroy(this);
    }
}
