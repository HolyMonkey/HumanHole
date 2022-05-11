using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
    [InitializeOnLoad]
    public static class MainSceneAutoLoader
    {
        private static string _mainScenePath = System.IO.Path.Combine(Application.dataPath, "HumanHole","Scenes","Initial.unity");
        
        static MainSceneAutoLoader()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }
        
        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (!EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode)
            {
                if (SceneManager.GetActiveScene().buildIndex != 0)
                    TryToOpenMainScene();
            }
        }

        private static void TryToOpenMainScene()
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                try
                {
                    EditorSceneManager.OpenScene(_mainScenePath);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Cannot load scene: {_mainScenePath}. {e.Message}");
                    EditorApplication.isPlaying = false;
                }
            }
            else
            {
                EditorApplication.isPlaying = false;
            }
        }
    }
}