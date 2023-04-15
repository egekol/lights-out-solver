// 15042023

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private AsyncOperation _asyncOperation;

        public void LoadLevelScene(SceneReference reference)
        {
            // SceneManager.LoadScene(reference);
            StartCoroutine(StartLoadSceneProcess(reference));
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