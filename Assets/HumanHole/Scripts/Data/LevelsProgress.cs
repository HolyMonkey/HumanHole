using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace HumanHole.Scripts.Data
{
    [Serializable]
    public class LevelsProgress
    {
        public string LevelName => "Level";

        private int _maxLevelNumber;
        
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

        public void AddAttempt() => 
            Attempts++;

        public void SetMaxLevelNumber(int levelNumber) => 
            _maxLevelNumber = levelNumber;

        private bool CanIncreaseLevelNumber() => 
            LevelNumber < _maxLevelNumber;

        private bool CanDecreaseLevelNumber() => 
            LevelNumber > 0;

        private void ResetAttempts() => 
            Attempts = 0;
    }
}