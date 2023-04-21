// 17042023

using TMPro;
using UI.CanvasGroups;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class LevelButton : MonoBehaviour
    {
        private Button button;
        public Button Button => button ??= GetComponent<Button>();
        public int level;
        public SceneReference LevelReference;
        private MainMenuUI _mainMenu;
        private AlphaGroup alphaGroup;
        public AlphaGroup AlphaGroup => alphaGroup ??= GetComponent<AlphaGroup>();
        
        private TextMeshProUGUI tMPLevelText;
        public TextMeshProUGUI TMPLevelText => tMPLevelText ??= GetComponentInChildren<TextMeshProUGUI>(true);

        public void OnClick(MainMenuUI mainMenuUI)
        {
            TMPLevelText.text = "LEVEL " + level;
            _mainMenu = mainMenuUI;
            // Debug.Log("OnClick");
            Button.onClick.AddListener(GoToLevel);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(GoToLevel);
        }

        private void GoToLevel()
        {
            _mainMenu.GroupSceneTransition.ChangeSceneToLevel(level);
        }
    }
}