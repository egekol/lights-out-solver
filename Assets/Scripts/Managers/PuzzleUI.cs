// 15042023

using UI;
using UI.CanvasGroups;
using UnityEngine;

namespace Managers
{
    public class PuzzleUI : MonoBehaviour
    {
        [SerializeField] private BackButton backButton;
        [SerializeField] private CanvasGroupSceneTransition canvasGroupSceneTransition;

        private void OnEnable()
        {
            backButton.Button.onClick.AddListener(GoToMenu);
            
            canvasGroupSceneTransition.SetAlphaTo(0);
            canvasGroupSceneTransition.OpenPanel();
        }

        private void GoToMenu()
        {
            canvasGroupSceneTransition.ChangeSceneToMenu();
        }
    }
}