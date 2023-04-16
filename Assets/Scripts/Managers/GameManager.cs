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
        
        public void LoadSceneReference(SceneReference reference)
        {
            // SceneManager.LoadScene(reference);
            Debug.Log("Ref: "+ reference.Name);
            StartCoroutine(StartLoadSceneProcess(reference));
        }
        public void LoadLevelScene(int levelNumber)
        {
            var lvl = levelNumber.ToString("00");
            Debug.Log(lvl);
            Debug.Log("G_LevelScene"+lvl);
            LoadSceneReference(sceneSettingsSo.gameSceneList.First(i=>i.Name=="G_LevelScene"+lvl));

        }

        public void LoadMainMenu()
        {
            LoadSceneReference(sceneSettingsSo.gameSceneList.First(i=>i.Name=="MainMenu"));
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