using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -11 || transform.position.y > 11
            || transform.position.x < -19 || transform.position.x > 19) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            collision.transform.GetComponent<DummyPlayer>().Hit(10);
    }
}
