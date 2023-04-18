// 15042023

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private AsyncOperation _asyncOperation;
        [SerializeField] public SceneSettingsSO sceneSettingsSo;
        // public List<SceneReference> gameSceneList;
        private Coroutine _loadSceneProcessCor;

        public void LoadSceneReference(SceneReference reference)
        {
            // if (_loadSceneProcessCor!=null)
            // {
            //     StopCoroutine(_loadSceneProcessCor);
            // }

            Debug.Log("LOAD SCENE: "+ reference.Name);

            SceneManager.LoadScene(reference);

            // _loadSceneProcessCor= StartCoroutine(StartLoadSceneProcess(reference));
        }
        public void LoadLevelScene(int levelNumber)
        {
            var lvl = levelNumber.ToString("00");
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

        /*private IEnumerator StartLoadSceneProcess(SceneReference reference)
        {
            yield return null;
        
            DOTween.CompleteAll();
            DOTween.KillAll();

            Debug.Log("LOAD SCENE: "+ reference.Name);
            SceneManager.LoadScene(reference.Name);
            
                /*_asyncOperation = SceneManager.LoadSceneAsync(reference);
                _asyncOperation.allowSceneActivation = false;
            
                while (!_asyncOperation.isDone )
                {
                    
                    if (_asyncOperation.progress >= 0.9f)
                    {
                        _asyncOperation.allowSceneActivation = true;
                        Debug.Log(_asyncOperation.allowSceneActivation);
                    }
            
                    yield return null;
                }#1#
        
            yield return null;
        }*/
    }
}