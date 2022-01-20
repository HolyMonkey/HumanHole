using System;

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

    public void UpdateLevel()
    {
        LevelNumber++;
    }

    public void UpdatePoints(int points)
    {
        Points += points;
    }

    public string LevelName()
    {
        return $"Level {LevelNumber}";
    }
}
