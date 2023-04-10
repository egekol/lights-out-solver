using System;
using UnityEngine;
using UnityEngine.UI;

namespace Grids
{
    public class Cell : MonoBehaviour
    {
        private bool _isOn = false;
        private Action<bool> _onToggleButtonClickAction;

        private Image cellImage;
        public Image CellImage => cellImage ??= GetComponent<Image>();
        private Button cellButton;
        public Button CellButton => cellButton ??= GetComponentInChildren<Button>(true);

        public void AddListener(Action<bool> func)
        {
            _onToggleButtonClickAction = func;
            CellButton.onClick.AddListener(ToggleClick);
        }

        private void ToggleClick()
        {
            _isOn = !_isOn;
            SetLight(_isOn);
            _onToggleButtonClickAction?.Invoke(_isOn);
        }

        public void Switch(bool isOn)
        {
            if (_isOn == isOn)
            {
                return;
            }

            _isOn = isOn;
            SetLight(isOn);
        }

        public void SetLight(bool isOn)
        {
            // int on = Convert.ToInt32(isOn);
            // CellImage.color = new Color(on, on, on);
            CellImage.color = isOn ? Color.white : Color.black;
        }

        [ContextMenu("SetLightOff")]
        public void Set()
        {
            SetLight(false);
        }
    }
}