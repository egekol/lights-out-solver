// 20042023

using System;
using Managers;
using TMPro;
using UnityEngine;
using Utilities;

namespace UI.Puzzle
{
    public class LevelScore : MonoBehaviour
    {
        private TextMeshProUGUI textMeshProUGUI;
        public TextMeshProUGUI LevelTMP => textMeshProUGUI ??= GetComponentInChildren<TextMeshProUGUI>(true);

        private void OnEnable()
        {
            SetLevelText();
            LevelManager.LevelComplete += SetLevelText;
        }

        private void OnDisable()
        {
            LevelManager.LevelComplete -= SetLevelText;
        }

        private void SetLevelText()
        {
            LevelTMP.text = $"LV{PlayerPrefKeys.CurrentPuzzleLevel.ToString()}";
        }
    }
}