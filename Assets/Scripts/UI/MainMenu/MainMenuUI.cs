// 16042023

using System.Collections.Generic;
using DG.Tweening;
using UI.CanvasGroups;
using UnityEngine;

namespace UI.MainMenu
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroupSceneTransition canvasGroupSceneTransition;
        [SerializeField] private List<LevelButton> buttonList;

        public CanvasGroupSceneTransition GroupSceneTransition => canvasGroupSceneTransition;

        private void Awake()
        {
            GroupSceneTransition.SetAlphaTo(0);
            foreach (var button in buttonList)
            {
                button.AlphaGroup.SetAlpha(0);
            }
            GroupSceneTransition.OpenPanel(() =>
            {
                for (var i = 0; i < buttonList.Count; i++)
                {
                    var button = buttonList[i];
                    var a = 0f;
                    DOTween.To(() => a, x =>
                    {
                        a = x;
                        button.AlphaGroup.SetAlpha(a);
                    }, 1f, .6f).SetDelay(i*.2f).SetLink(button.gameObject);
                }
            });
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