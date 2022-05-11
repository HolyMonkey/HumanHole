using System;

namespace HumanHole.Scripts.Infrastructure.Services.Ads
{
    public abstract class IAd
    {
        public abstract event Action Opened;
        public abstract event Action Closed;
        public abstract event Action DidError;
        public abstract event Action Offline;
        public abstract event Action Showed;
        
        public abstract void Show();
        protected abstract void OnOpened();
        protected abstract void OnClosed();
        protected abstract void OnDidError(string message);
        protected abstract void OnOffline();
        protected abstract void OnShowed();
    }
}