// 15042023

using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private AsyncOperation _asyncOperation;
        [SerializeField] private SceneSettingsSO sceneSettingsSo;
        
        public void LoadLevelScene(SceneReference reference)
        {
            // SceneManager.LoadScene(reference);
            StartCoroutine(StartLoadSceneProcess(reference));
        }

        public void LoadMainMenu()
        {
            LoadLevelScene(sceneSettingsSo.gameSceneList.First(i=>i.Name=="MainMenu"));
        }

        private void Awake()
        {
            DependencyInjector.Instance.Register(this);
        }

        private IEnumerator StartLoadSceneProcess(SceneReference reference)
        {
            yield return null;
        
            /*DOTween.CompleteAll();
            DOTween.KillAll();*/

            _asyncOperation = SceneManager.LoadSceneAsync(reference);
            _asyncOperation.allowSceneActivation = false;
        
            while (!_asyncOperation.isDone )
            {
                
                if (_asyncOperation.progress >= 0.9f)
                {
                    _asyncOperation.allowSceneActivation = true;
                }
        
                yield return null;
            }
        
            yield return null;
        }
    }
}