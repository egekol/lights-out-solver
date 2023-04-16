using System;
using UnityEngine;

namespace UI.CanvasGroups
{
    interface ICanvasGroup
    {
        public bool IsLocked { get; set; }
        public CanvasGroup CanvasGroup { get; }
        void ClosePanel(Action onComplete=null);
        void OpenPanel(Action onComplete=null);
    }
}