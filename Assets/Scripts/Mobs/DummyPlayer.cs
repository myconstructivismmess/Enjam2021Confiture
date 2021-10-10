using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayer : MonoBehaviour
{
    public float health = 10;

    public void Hit(int dmg)
    {
        health -= dmg;
        Debug.Log($"Ouch ! J'ai plus que {health} pv");
    }

    private void OnParticleTrigger()
    {
        health -= 1;
        Debug.Log($"Ouch ça brûle ! J'ai plus que {health} pv");
    }
}
