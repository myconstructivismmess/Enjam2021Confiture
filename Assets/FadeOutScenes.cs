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
    enum FadeOnStart
    {
        In,
        Out
    }

    public Image img;
    [SerializeField] private float _fadeOutTime;
    [SerializeField] private float _fadeInTime;
    [SerializeField] private FadeOnStart _fadeInOnStart;


    private void Start()
    {
        switch (_fadeInOnStart)
        {
            case FadeOnStart.In:
                FadeIn();
                break;
            case FadeOnStart.Out:
                FadeOut();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void FadeOut(UnityEvent e = null)
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
