namespace HumanHole.Scripts.Infrastructure.Services.Ads
{
    public interface IAdsService : IService
    {
        InterstitialAd InterstitialAd { get; }
        RewardedAd RewardedAd { get; }
    }
}