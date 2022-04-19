using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HumanHole.Scripts.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        public SceneLoader(ICoroutineRunner coroutineRunner) => 
            _coroutineRunner = coroutineRunner;

        public void Load(string name, Action onLoaded = null, bool restartAllowed = false)
        {
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded, restartAllowed));
        }

        private static IEnumerator LoadScene(string nextScene, Action onLaded = null, bool restartAllowed = false)
        {
            if (!restartAllowed)
            {
                if (SceneManager.GetActiveScene().name == nextScene)
                {
                    onLaded?.Invoke();
                    yield break;
                }
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
                yield return null;

            onLaded?.Invoke();
        }
        
    }
}