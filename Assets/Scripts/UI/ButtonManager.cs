using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Manage the actions on hover enter/exit on text UI in MainMenu scene
    /// </summary>
    public class ButtonManager : SerializedMonoBehaviour
    {
        private TMP_Text _text;

        private void Start()
        {
            _text = this.GetComponent<TMP_Text>();
        }

        /// <summary>
        /// Scale up 
        /// </summary>
        public void OnPointerEnter()
        {
            _text.rectTransform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f);
        }

        /// <summary>
        /// Scale down
        /// </summary>
        public void OnPointerExit()
        {
            _text.rectTransform.DOScale(new Vector3(1, 1, 1), 0.5f);
        }
    }
}