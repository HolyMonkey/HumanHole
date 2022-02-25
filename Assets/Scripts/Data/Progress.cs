using System;
using UnityEngine.SceneManagement;

[Serializable]
public class Progress
{
    [NonSerialized] public bool IsGameStarted;
    
    public int LevelNumber;
    public int Points;

    public Progress()
    {
        LevelNumber = 1;
    }

    public void TryUpdateLevel()
    {
        if (CanUpdateLevel())
        {
            LevelNumber++;
        }
    }

    public void SetLevelNumber(int levelNumber)
    {
        LevelNumber = levelNumber;
    }

    public void UpdatePoints(int points)
    {
        Points += points;
    }

    public string LevelName()
    {
        return $"Level {LevelNumber}";
    }

    private bool CanUpdateLevel()
    {
        var levelsCount = SceneManager.sceneCount - 1;
        var nextLevelNumber = LevelNumber + 1;
        if (levelsCount > nextLevelNumber)
        {
            return true;
        }

        return false;
    }
}
