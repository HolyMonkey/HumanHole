using UnityEngine;

public class SaveLoadService : ISaveLoadService
{
    private const string ProgressKey = "Progress";

    private readonly IPersistentProgressService _progressService;

    public SaveLoadService(IPersistentProgressService progressService)
    {
        _progressService = progressService;
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        PlayerPrefs.Save();
    }

    public Progress LoadProgress() => 
        PlayerPrefs.GetString(ProgressKey)?.FromJson<Progress>();
}