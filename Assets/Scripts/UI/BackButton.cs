// 17042023

using System;
using Managers;
using UI.CanvasGroups;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BackButton : MonoBehaviour
    {
        private Button button;
        public Button Button => button ??= GetComponent<Button>();

        private CanvasGroupSceneTransition canvasGroupSceneTransition;

        public CanvasGroupSceneTransition CanvasGroupSceneTransition => canvasGroupSceneTransition ??=
            GetComponentInParent<CanvasGroupSceneTransition>(true);

        private void OnEnable()
        {
            Button.onClick.AddListener(GoToMenu);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(GoToMenu);
        }

        private void GoToMenu()
        {
            CanvasGroupSceneTransition.ChangeSceneToMenu();
        }
    }
}