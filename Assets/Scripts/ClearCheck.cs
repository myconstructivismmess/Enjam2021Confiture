using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ClearCheck : MonoBehaviour
{
    [SerializeField] private List<Transform> mobList;
    [SerializeField] private string _sceneName;
    [SerializeField] private FadeOutScenes _fadeOutScenes;
    public static ClearCheck Instance;

    private void Start()
    {
        Instance = this;
    }

    public void CheckEnd()
    {
        if (mobList.Count == 0)
        {
            var e = new UnityEvent();
            e.AddListener(() =>
            {
                SceneManager.LoadScene(_sceneName);
            });
            
            _fadeOutScenes.FadeOut(e);
        }
    }
}
