// 16042023

using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace UI.CanvasGroups
{
    public abstract class CanvasGroupBase:MonoBehaviour, ICanvasGroup
    {
        public bool IsLocked
        {
            get => _isLocked;
            set
            {
                _isLocked = value;
                CanvasGroup.interactable = !value;
            }
        }
        public List<AlphaGroup> AlphaGroups { get; set; }

        private CanvasGroup canvasGroup;
        private bool _isLocked;
        public CanvasGroup CanvasGroup => canvasGroup ??= GetComponent<CanvasGroup>();

        public virtual void SetAlphaTo(float val)
        {
            CanvasGroup.alpha = val;
            
        }

        public void OnEnable()
        {
            AlphaGroups = GetComponentsInChildren<AlphaGroup>(true).ToList();

        }

        public virtual void ClosePanel(Action onComplete = null)
        {
            IsLocked = true;
            transform.DOComplete();
            float a = CanvasGroup.alpha;
            DOTween.To(() => CanvasGroup.alpha, x =>
            {
                a = x;
                CanvasGroup.alpha = a;
                foreach (var alphaGroup in AlphaGroups)
                {
                    // Debug.Log(a);///
                    alphaGroup.SetAlpha(a);
                }
            }, 0f, .5f).OnComplete(() =>
            {
                IsLocked = false;
                onComplete?.Invoke();
            }).SetLink(gameObject);

        }

        public virtual void OpenPanel(Action onComplete = null)
        {
            IsLocked = true;
            transform.DOComplete();
            DOTween.To(() => CanvasGroup.alpha, x =>
            {
                CanvasGroup.alpha = x;

            }, 1f, .5f).OnComplete(() =>
            {
                IsLocked = false;
                onComplete?.Invoke();
            }).SetLink(gameObject);
        }
    }
}