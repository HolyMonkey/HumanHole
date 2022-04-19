namespace HumanHole.Scripts.Infrastructure.Services.Ads
{
    public class AdsService : IAdsService
    {
        public InterstitialAd InterstitialAd { get; }
        public RewardedAd RewardedAd { get; }

        public AdsService()
        {
            InterstitialAd = new InterstitialAd();
            RewardedAd = new RewardedAd();
        }
    }
}