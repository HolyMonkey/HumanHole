using System;
using Agava.YandexGames;
using UnityEngine;

public class SaveLoadService : ISaveLoadService
{
    private const string ProgressKey = "Progress";

    private readonly IPersistentProgressService _progressService;

    public event Action Saved;
    public event Action<Progress> Loaded;

    public SaveLoadService(IPersistentProgressService progressService)
    {
        _progressService = progressService;
    }

    public void SaveProgress()
    {
/*#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
            PlayerAccount.SetPlayerData( _progressService.Progress.ToJson(), OnSetPlayerDataSuccess, OnSetPlayerDataError);
            if(PlayerPrefs.HasKey(ProgressKey))
                PlayerPrefs.DeleteAll();
            
            return;
        }
#endif*/
        
        PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        PlayerPrefs.Save();
        OnSetPlayerDataSuccess();
    }

    public void LoadProgress()
    {
/*#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
            PlayerAccount.GetPlayerData(OnGetPlayerDataSuccess, OnGetPlayerDataError);
            return;
        }
#endif*/
        
        OnGetPlayerDataSuccess(PlayerPrefs.GetString(ProgressKey));
    }

    private void OnGetPlayerDataSuccess(string message)
    {
        Progress progress = message?.FromJson<Progress>();
        Loaded?.Invoke(progress);
    }

    private void OnGetPlayerDataError(string message)
    {
        Debug.LogError(message);
    }
    
    private void OnSetPlayerDataSuccess()
    {
        Saved?.Invoke();
    }

    private void OnSetPlayerDataError(string message)
    {
        Debug.LogError(message);
    }
}