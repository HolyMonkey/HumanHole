using System;
using HumanHole.Scripts.Data;

namespace HumanHole.Scripts.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        void LoadProgress();
        event Action Saved;
        event Action<Progress> Loaded;
        void ClearProgress();
    }
}