using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ButtonManager : SerializedMonoBehaviour
    {
        private TMP_Text _text;

        private void Start()
        {
            _text = this.GetComponent<TMP_Text>();
        }

        public void OnPointerEnter()
        {
            _text.rectTransform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f);
        }

        public void OnPointerExit()
        {
            _text.rectTransform.DOScale(new Vector3(1, 1, 1), 0.5f);
        }
    }
}