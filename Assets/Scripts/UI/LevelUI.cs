using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _pointsText;
    [SerializeField] private GameObject _balanceSlider;
    [SerializeField] private GameObject _progressSlider;
    [SerializeField] private LevelHandler _levelHandler;

    public void Initial()
    {
        string levelName = Game.Instance.AllServices.Single<IPersistentProgressService>().Progress.LevelName();
        SetLevelName(levelName);
        SetPoints(0);
    }

    private void OnEnable()
    {
        _levelHandler.LevelLost += OnLevelСompleted;
        _levelHandler.LevelWon += OnLevelСompleted;
    }

    private void OnDisable()
    {
        _levelHandler.LevelLost -= OnLevelСompleted;
        _levelHandler.LevelWon -= OnLevelСompleted;
    }

    private void OnLevelСompleted()
    {
        _progressSlider.SetActive(false);
        _balanceSlider.SetActive(false);
    }
    
    public void SetPoints(int points) => 
        _pointsText.SetText(points.ToString());

    private void SetLevelName(string levelNumber) => 
        _levelText.SetText(levelNumber);

}
