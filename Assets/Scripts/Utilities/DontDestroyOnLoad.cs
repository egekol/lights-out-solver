// 15042023

using UnityEngine;

namespace Utilities
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}