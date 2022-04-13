public interface IRewardService : IService
{
    int RewardedPoints { get; }
    void AddReward();
}
