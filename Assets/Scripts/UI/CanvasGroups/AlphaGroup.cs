using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI.CanvasGroups
{
    public class AlphaGroup : MonoBehaviour
    {
        private List<Graphic> graphics;

        [SerializeField] private bool includeSelf;

        [SerializeField] private List<Graphic> excludeList;
        private List<float> graphicAlphas;

        [ContextMenu("Alpha1Test")]
        private void Aplha1Test()
        {
            SetAlpha(1);
        }

        [ContextMenu("Alpha0Test")]
        private void Aplha0Test()
        {
            SetAlpha(0);
        }

        private void Setup()
        {
            graphics = GetComponentsInChildren<Graphic>(true).ToList();
            if (!includeSelf)
                graphics.Remove(GetComponent<Graphic>());

            for (int i = 0; i < excludeList.Count; i++)
            {
                if (graphics.Contains(excludeList[i]))
                    graphics.Remove(excludeList[i]);
            }

            SetCurrentAlphaValue();
        }

        // private void OnEnable()
        // {
        //     SetCurrentAlphaValue();
        // }

        private void SetCurrentAlphaValue()
        {
            graphicAlphas = new List<float>();
            foreach (var graphic in graphics)
            {
                graphicAlphas.Add(graphic.color.a);
            }
        }

        public void Remove(Graphic graphic)
        {
            graphics.Remove(graphic);
        }

        public void Add(Graphic graphic)
        {
            graphics.Add(graphic);
        }

        public void SetAlpha(float value)
        {
            if (graphics == null)
                Setup();

            for (int i = 0; i < graphics.Count; i++)
            {
                var c = graphics[i].color;
                c.a = value * graphicAlphas[i];
                graphics[i].color = c;
            }
        }
    }
}