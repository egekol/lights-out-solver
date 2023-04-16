// 16042023

using System;
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

        private CanvasGroup canvasGroup;
        private bool _isLocked;
        public CanvasGroup CanvasGroup => canvasGroup ??= GetComponent<CanvasGroup>();

        public virtual void SetAlphaTo(float val)
        {
            CanvasGroup.alpha = val;
            
        }
        public virtual void ClosePanel(Action onComplete = null)
        {
            IsLocked = true;
            transform.DOComplete();
            DOTween.To(() => CanvasGroup.alpha, x =>
            {
                CanvasGroup.alpha = x;

            }, 0, 1.5f).OnComplete(() =>
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

            }, 1, 1.5f).OnComplete(() =>
            {
                IsLocked = false;
                onComplete?.Invoke();
            }).SetLink(gameObject);
        }
    }
}