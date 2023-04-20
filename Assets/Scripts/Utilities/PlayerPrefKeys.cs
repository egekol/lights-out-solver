// 15042023

using System;
using UnityEngine;

namespace Utilities
{
    public static class PlayerPrefKeys
    {
        public static int CurrentPuzzleLevel
        {
            get => PlayerPrefs.GetInt("CurrentLevel", 0);
            set => PlayerPrefs.SetInt("CurrentLevel", value);
        }
        public static int CurrentGridPattern
        {
            get => PlayerPrefs.GetInt("CurrentGridPattern", 0);
            set => PlayerPrefs.SetInt("CurrentGridPattern", value);
        }
        public static int StartingGridPattern
        {
            get => PlayerPrefs.GetInt("StartingGridPattern", 0);
            set => PlayerPrefs.SetInt("StartingGridPattern", value);
        }

       
    }
}