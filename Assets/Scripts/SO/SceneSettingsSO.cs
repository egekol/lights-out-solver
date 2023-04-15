// 15042023

using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    [CreateAssetMenu(fileName = "SceneSettings", menuName = "ScriptableObjects", order = 0)]
    public class SceneSettingsSO : ScriptableObject
    {
        public List<SceneReference> gameSceneList;

    }
}