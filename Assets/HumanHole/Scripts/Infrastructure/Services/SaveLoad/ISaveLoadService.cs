using System;

public interface ISaveLoadService : IService
{
    void SaveProgress();
    void LoadProgress();
    event Action Saved;
    event Action<Progress> Loaded;
}