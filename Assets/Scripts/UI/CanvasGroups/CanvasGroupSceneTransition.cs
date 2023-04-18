// 16042023

using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;

namespace UI.CanvasGroups
{
    public class CanvasGroupSceneTransition:CanvasGroupBase
    {
        private GameManager _gameManager;
        private void Awake()
        {
            _gameManager = DependencyInjector.Instance.Resolve<GameManager>();
        }

        public void ChangeSceneToMenu()
        {
            ClosePanel(_gameManager.LoadMainMenu);
        }

        public void ChangeSceneToLevel(int levelNumber)
        {
            ClosePanel(() =>
            {
                Debug.Log("Complete");
                _gameManager.LoadLevelScene(levelNumber);
            });
        }
    }
}