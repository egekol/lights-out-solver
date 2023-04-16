// 16042023

using System;
using Managers;

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

        public void ChangeSceneToLevel(SceneReference levelNumber)
        {
            ClosePanel(() =>
            {
                _gameManager.LoadSceneReference(levelNumber);
            });
        }
    }
}