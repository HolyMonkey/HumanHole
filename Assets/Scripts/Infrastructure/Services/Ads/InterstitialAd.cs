using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Ads
{
    public class InterstitialAd: IAd
    {
        public override event Action Opened;
        public override event Action Closed;
        public override event Action DidError;
        public override event Action Offline;
        public override event Action Showed;

        public bool IsClosed { get; private set; }

        public override void Show()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            InterestialAd.Show(OnOpened, OnClosed, OnDidError, OnOffline);
#endif
        }

        protected override void OnOpened()
        {
            Opened?.Invoke();
        }

        protected override void OnDidError(string message)
        {
            Debug.LogError(message);
            DidError?.Invoke();
        }

        protected override void OnClosed(bool isClosed)
        {
            IsClosed = isClosed;
        }

        protected override void OnOffline()
        {
            Offline?.Invoke();
        }

        protected override void OnClosed()
        {
            throw new NotImplementedException();
        }

        protected override void OnShowed()
        {
            throw new NotImplementedException();
        }
    }
}