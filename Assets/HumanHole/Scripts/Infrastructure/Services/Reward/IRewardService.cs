namespace HumanHole.Scripts.Infrastructure.Services.Reward
{
    public interface IRewardService : IService
    {
        int RewardedPoints { get; }
        void AddReward();
    }
}
