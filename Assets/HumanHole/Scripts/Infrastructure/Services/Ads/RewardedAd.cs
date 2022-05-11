using System;
using Agava.YandexGames;

namespace HumanHole.Scripts.Infrastructure.Services.Ads
{
    public class RewardedAd: IAd
    {
        public override event Action Opened;
        public override event Action Closed;
        public override event Action DidError;
        public override event Action Offline;
        public override event Action Showed;
        
        public override void Show()
        {
#if UNITY_WEBGL && UNITY_EDITOR
            VideoAd.Show(OnOpened, OnShowed, OnClosed, OnDidError);
#else
            OnShowed();
#endif
        }

        protected override void OnClosed() => 
            Closed?.Invoke();

        protected override void OnOpened() => 
            Opened?.Invoke();

        protected override void OnDidError(string message) => 
            DidError?.Invoke();

        protected override void OnOffline() => 
            Offline?.Invoke();

        protected override void OnShowed() => 
            Showed?.Invoke();
    }
}
