// 16042023

using Managers;

namespace UI.CanvasGroups
{
    public class CanvasGroupLevel:CanvasGroupBase
    {
        private GameManager _gameManager;

        //todo: It's ironic that I use injection for modularity but still try to create canvasGroup for every occasion :^V
        // private void Awake()
        // {
        //     _gameManager = DependencyInjector.Instance.Resolve<GameManager>();
        // }
        //
        // public void ChangeSceneToMenu()
        // {
        //     ClosePanel(_gameManager.LoadMainMenu);
        // }

    }
}