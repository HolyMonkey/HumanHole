using System;

[Serializable]
public class Progress
{
    [NonSerialized] public bool IsGameStarted;
    public int LevelNumber;
    public int AllPoints;

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
        AllPoints += points;
    }

    public string LevelName()
    {
        return $"Level {LevelNumber}";
    }
}
