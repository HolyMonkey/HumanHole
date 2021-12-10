using System;
using TMPro;
using UnityEditor;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _pointsText;
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private GameObject _balanceSlider;
    [SerializeField] private GameObject _progressSlider;
    [SerializeField] private СollisionObserver сollisionObserver;

    private void OnEnable()
    {
        сollisionObserver.WallHitPerson += OnWallHitPerson;
    }

    private void OnDisable()
    {
        сollisionObserver.WallHitPerson -= OnWallHitPerson;
    }

    private void OnWallHitPerson()
    {
        _progressSlider.SetActive(false);
        _balanceSlider.SetActive(false);
    }

    private void Start()
    {
        SetLevel(_levelConfig.LevelNumber);
        SetPoints(0);
    }

    private void SetPoints(int points)
    {
        _pointsText.SetText(points.ToString());
    }

    private void SetLevel(int levelNumber)
    {
        string level = $"Level {levelNumber}";
        _levelText.SetText(level);
    }
}
