// 15042023

using Managers;
using UI.CanvasGroups;
using UnityEngine;

namespace UI.Puzzle
{
    public class PuzzleUI : MonoBehaviour
    {
        [SerializeField] private BackButton backButton;
        [SerializeField] private CanvasGroupSceneTransition canvasGroupSceneTransition;

        private void OnEnable()
        {
            backButton.Button.onClick.AddListener(GoToMenu);
            
            canvasGroupSceneTransition.SetAlphaTo(0);
            canvasGroupSceneTransition.OpenPanel(LevelManager.StartLevel);
        }

        private void GoToMenu()
        {
            canvasGroupSceneTransition.ChangeSceneToMenu();
        }
    }
}