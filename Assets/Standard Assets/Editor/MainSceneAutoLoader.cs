using System;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
    [InitializeOnLoad] //Позволяет скрипту инициализироваться при запуске Unity 
    public static class MainSceneAutoLoader
    {
        private static string _mainScenePath = Path.Combine(Application.dataPath, "HumanHole","Scenes", "Initial.unity");//Получаем путь к начальной сцене
        
        static MainSceneAutoLoader()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        /// <summary>
        /// Вызывается после отрбаотки события, когда меняется состояние PlayMode
        /// </summary>
        /// <param name="state">enum состояния в которое перешли</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            //Если еще не в PlayMode, но собирается. Сцену нельзя менять в PlayMode
            if (!EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode)
            {
                //Проверяем не является ли текущая сцена начальной сценой
               // if (SceneManager.GetActiveScene().buildIndex != 0)
                    //TryToOpenMainScene();
            }
        }

        private static void TryToOpenMainScene()
        {
            //Cпрашивем у игрока хочет ли он сохранить сцену перед сменой(на слуйчай если он делал изменения в сцене)
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) //Данный метод возврает true если пользователь ответил да на сохранение
            {
                try
                {
                    //Переключаемcя на начальную сцену
                    EditorSceneManager.OpenScene(_mainScenePath);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Cannot load scene: {_mainScenePath}. {e.Message}");
                    EditorApplication.isPlaying = false; //Остановим игру
                }
            }
            else
            {
                EditorApplication.isPlaying = false; //Остановим игру
            }
        }
    }
}