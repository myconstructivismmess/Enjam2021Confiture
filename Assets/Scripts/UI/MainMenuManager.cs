using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;

public class MainMenuManager : SerializedMonoBehaviour
{
    [SerializeField] private FadeOutScenes _fadeOutScenes;
    
    private void Start()
    {
        SceneManager.LoadScene("AudioScene", LoadSceneMode.Additive);
    }

    public void QuitGame()
   {
#if UNITY_EDITOR
       EditorApplication.ExitPlaymode();
#endif
#if UNITY_STANDALONE_WIN
       Application.Quit();
#endif
       
   }

    public void StartGame()
    {
        var uEvent = new UnityEvent();
        uEvent.AddListener(() => SceneManager.LoadScene("MobScene"));
        _fadeOutScenes.FadeOut(uEvent);
        
    }
}
