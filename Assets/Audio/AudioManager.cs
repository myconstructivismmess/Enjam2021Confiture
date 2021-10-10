using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _src;
    [SerializeField] private AudioClip _intro;
    [SerializeField] private AudioClip _loop;
    private bool _shouldCheckIntroEnd = true;

    void Start()
    {
        DontDestroyOnLoad(this);
        _src.clip = _intro;
        _src.Play();
        _shouldCheckIntroEnd = true;
        _src.loop = false;
    }

    private void Update()
    {
        if (_shouldCheckIntroEnd && _src.time >= _intro.length)
        {
            _src.clip = _loop;
            _src.Play();
            _src.loop = true;
            _shouldCheckIntroEnd = false;
        }
    }
}
