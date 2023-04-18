// 17042023

using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BackButton : MonoBehaviour
    {


        private Button button;
        public Button Button => button ??= GetComponent<Button>();

    }
}