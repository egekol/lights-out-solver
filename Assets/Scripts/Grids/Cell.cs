using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Grids
{
    public class Cell : MonoBehaviour
    {
        private bool _isOn = false;
        private Action<int> _onToggleButtonClickAction;
        [SerializeField] private Color onColor;
        [SerializeField] private Color offColor;

        public int Index => _index;

        private Image cellImage;
        public Image CellImage => cellImage ??= GetComponent<Image>();
        private Button cellButton;
        [SerializeField] private int _index;
        public Button CellButton => cellButton ??= GetComponentInChildren<Button>(true);

        public bool IsOn
        {
            get => _isOn;
            set => _isOn = value;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            var p = transform.GetComponentInParent<GridPanel>();
            if (!p.IsUnityNull())
            {
                var index = p.CellList.IndexOf(this);
                _index = index;
            }
        }
#endif

        private void Start()
        {
            SetLight(IsOn);
        }

        public void AddListener(Action<int> func)
        {
            _onToggleButtonClickAction = func;
            CellButton.onClick.AddListener(ToggleClick);
        }

        private void ToggleClick()
        {
            IsOn = !IsOn;
            SetLight(IsOn);
            _onToggleButtonClickAction?.Invoke(Index);
        }

        public void Switch(bool isOn)
        {
            if (IsOn == isOn)
            {
                return;
            }

            IsOn = isOn;
            
            SetLight(isOn);
        }

        public void SetLight(bool isOn)
        {
            // int on = Convert.ToInt32(isOn);
            // CellImage.color = new Color(on, on, on);
            transform.DOKill();
            transform.DOScale(1.1f, .1f).OnComplete(() =>
                {
                    transform.DOScale(1f, .1f);
                }).SetEase(Ease.InOutSine)
                .SetLink(gameObject);
            CellImage.color = isOn ? onColor : offColor;
        }

        [ContextMenu("SetLightOff")]
        public void Set()
        {
            SetLight(false);
        }
    }
}