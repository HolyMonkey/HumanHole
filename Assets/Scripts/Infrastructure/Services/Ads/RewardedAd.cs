using System;
using CodeBase.Infrastructure.Services.Ads;
using UnityEngine;
using YandexGames;

public class RewardedAd: IAd
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
            VideoAd.Show(OnOpened, OnShowed, OnClosed,OnDidError);
#endif
    }

    protected override void OnClosed()
    {
        Closed?.Invoke();
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
        throw new NotImplementedException();
    }

    protected override void OnOffline()
    {
        Offline?.Invoke();
    }

    protected override void OnShowed()
    {
        Showed?.Invoke();
    }
}
