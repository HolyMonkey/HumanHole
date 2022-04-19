using HumanHole.Scripts.Infrastructure.Services.PersistentProgress;
using HumanHole.Scripts.Infrastructure.Services.SaveLoad;

namespace HumanHole.Scripts.Infrastructure.Services.Reward
{
    public class RewardService : IRewardService
    {
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        public int RewardedPoints => 3;
    
        public RewardService(IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _progressService = progressService;
        }

        public void AddReward()
        {
            _progressService.Progress.UpdatePoints(RewardedPoints);
            _saveLoadService.SaveProgress();
        }
    }
}
