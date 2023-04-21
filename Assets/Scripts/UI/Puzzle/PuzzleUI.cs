// 15042023

using System;
using System.Collections;
using Grids;
using Managers;
using UI.CanvasGroups;
using UnityEngine;

namespace UI.Puzzle
{
    public class PuzzleUI : MonoBehaviour
    {
        [SerializeField] private BackButton backButton;
        [SerializeField] private CanvasGroupSceneTransition canvasGroupSceneTransition;

        public CanvasGroupSceneTransition GroupSceneTransition
        {
            get => canvasGroupSceneTransition;
            set => canvasGroupSceneTransition = value;
        }

        private GridPanel gridPanel;
        public GridPanel GridPanel => gridPanel ??= GetComponentInChildren<GridPanel>(true);

        private void OnEnable()
        {
            backButton.Button.onClick.AddListener(GoToMenu);
        }

        private void Awake()
        {
            Debug.Log("Register: Puzzle");
            // InitGrid();
            DependencyInjector.Instance.Register(this);
        }

        // private void InitGrid()
        // {
        //     throw new NotImplementedException();
        // }

        private void OnDisable()
        {
            backButton.Button.onClick.RemoveListener(GoToMenu);
        }

        private void Start()
        {
            // yield return null;
            GroupSceneTransition.SetAlphaTo(0);
            // Debug.Break();
            GroupSceneTransition.OpenPanel(/*Debug.Break*/);
        }

        private void GoToMenu()
        {
            GroupSceneTransition.ChangeSceneToMenu();
        }
    }
}