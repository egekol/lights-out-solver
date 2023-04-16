// 16042023

using System;
using System.Collections.Generic;
using UI.CanvasGroups;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroupSceneTransition canvasGroupSceneTransition;
        [SerializeField] private List<LevelButton> buttonList;

        public CanvasGroupSceneTransition GroupSceneTransition => canvasGroupSceneTransition;

        private void Awake()
        {
            GroupSceneTransition.SetAlphaTo(0);
            GroupSceneTransition.OpenPanel();
        }

        private void Start()
        {
            foreach (var button in buttonList)
            {
                button.OnClick(this);
            }
        }

    }
}