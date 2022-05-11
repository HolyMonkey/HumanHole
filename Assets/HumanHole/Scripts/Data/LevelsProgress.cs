using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace HumanHole.Scripts.Data
{
    [Serializable]
    public class LevelsProgress
    {
        public int LevelNumber;
        public int Attempts;

        public LevelsProgress() => 
            LevelNumber = 1;

        public void TryIncreaseLevelNumber()
        {
            if (CanIncreaseLevelNumber())
            {
                LevelNumber++;
                ResetAttempts();
            }
        }

        public void TryDecreaseLevelNumber()
        {
            if (CanDecreaseLevelNumber())
                LevelNumber--;
        }

        public void SetLevelNumber(int levelNumber) => 
            LevelNumber = levelNumber;

        public string LevelName() => 
            $"Level {LevelNumber}";
    
        public void AddAttempt() => 
            Attempts++;

        private bool CanIncreaseLevelNumber()
        {
            int levelsCount = SceneManager.sceneCountInBuildSettings - 1;
            int nextLevelNumber = LevelNumber + 1;
            return levelsCount > nextLevelNumber;
        }

        private bool CanDecreaseLevelNumber() => 
            LevelNumber > 0;

        private void ResetAttempts() => 
            Attempts = 0;
    }
}