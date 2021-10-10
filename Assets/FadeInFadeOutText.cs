using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInFadeOutText : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _introTexts;
    private int _currentText = 0;
    [SerializeField] private string _sceneName;
    
    void Start()
    {
        foreach (var textVARIABLE in _introTexts)
        {
            textVARIABLE.DOFade(0, 0f);
        }
        FadeIn();
    }


    private void FadeIn()
    {
        if (_currentText >= 0)
        {
            _introTexts[_currentText].DOFade(1f, 4f).OnComplete(() =>
            {
                Invoke(nameof(FadeOut), 2f);
            });
        }
    }

    private void FadeOut()
    {
       _introTexts[_currentText].DOFade(0f, 4f).OnComplete(() =>
       {
           _currentText++;
           if (_currentText <= 2)
               Invoke(nameof(FadeIn), 1f);
           else
               SceneManager.LoadScene(_sceneName);
       });
    }
}
