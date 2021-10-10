using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeOutScenes : MonoBehaviour
{
    public Image img;
    [SerializeField] private float _fadeOutTime;
    [SerializeField] private float _fadeInTime;


    public void FadeOut(UnityEvent e)
    {
        img.DOFade(1F, _fadeOutTime).OnComplete(() =>
        {
            e?.Invoke();
        });
    }
    
    public void FadeIn()
    {
        img.DOFade(0f, _fadeOutTime);
    }
}
