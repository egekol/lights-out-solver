// 20042023

using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SuccessPanel : MonoBehaviour
    {
        [SerializeField] private Transform successUI;
        [SerializeField] private Button nextLevelButton;

        private void OnEnable()
        {
            Debug.Log("Panel: Enable");
            LevelManager.LevelComplete += OpenPanel;
        }

        private void Awake()
        {
            Debug.Log("Panel: Awake");
        }

        private void Start()
        {
            Debug.Log("Panel: Start");

        }

        private void OnDisable()
        {
            LevelManager.LevelComplete -= OpenPanel;
        }

        private void OpenPanel()
        {
            successUI.gameObject.SetActive(true);
            nextLevelButton.onClick.AddListener(NewLevel);
        }

        private void NewLevel()
        {
            successUI.gameObject.SetActive(false);
            LevelManager.StartLevel();
            nextLevelButton.onClick.RemoveListener(NewLevel);
        }
    }
}