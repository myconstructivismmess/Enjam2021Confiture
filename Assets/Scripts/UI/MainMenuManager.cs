using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;

public class MainMenuManager : SerializedMonoBehaviour
{
    public void QuitGame()
   {
#if UNITY_EDITOR
       EditorApplication.ExitPlaymode();
#endif
#if UNITY_STANDALONE_WIN
       Application.Quit();
#endif
       
   }
}
