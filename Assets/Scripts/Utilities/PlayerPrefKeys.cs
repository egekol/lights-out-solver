// 15042023

using UnityEngine;

namespace Managers
{
    public static class PlayerPrefKeys
    {
        public static int CurrentLevel
        {
            get => PlayerPrefs.GetInt("CurrentLevel", 0);
            set => PlayerPrefs.SetInt("CurrentLevel", value);
        }
        public static int CurrentGridPattern
        {
            get => PlayerPrefs.GetInt("CurrentGridPattern", 0);
            set => PlayerPrefs.SetInt("CurrentGridPattern", value);
        }
    }
}