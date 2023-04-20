// 20042023

using System;
using Utilities;

namespace Managers
{
    public static class LevelManager
    {
        public static event Action LevelStart;
        public static event Action LevelComplete;
        public static event Action LevelFail;

        public static bool IsLevelPlaying;

        public static void StartLevel()
        {
            LevelStart?.Invoke();
            IsLevelPlaying = true;
        }

        public static void InitLevelComplete()
        {
            IsLevelPlaying = false;

            LevelComplete?.Invoke();
            PlayerPrefKeys.CurrentPuzzleLevel++;
        }

        public static void InitLevelFail()
        {
            IsLevelPlaying = false;

            LevelFail?.Invoke();
        }
    }
}