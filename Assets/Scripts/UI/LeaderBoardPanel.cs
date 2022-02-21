using System;
using System.Collections.Generic;
using Agava.YandexGames;
using CodeBase.Infrastructure.Services.LeaderBoard;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardPanel : MonoBehaviour
{
    private ILeaderBoardService _leaderBoardService;
    private LeaderboardEntryResponse _playerEntry;
    private List<LeaderBoardUserTemplate> _leaderBoardUserTemplates = new List<LeaderBoardUserTemplate>();

    [SerializeField] private TextMeshProUGUI _rank;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _formattedScore;
    [SerializeField] private TextMeshProUGUI _errorMessage;
    [SerializeField] private LeaderBoardUserTemplate _leaderBoardUserTemplate;
    [SerializeField] private Transform _content;
    [SerializeField] private Button _closeButton;

    public event Action Opened;
    public event Action Closed;
    
    public void Initial()
    {
        _leaderBoardService = Game.Instance.AllServices.Single<ILeaderBoardService>();
    }

    private void OnEnable()
    {
        _leaderBoardService.GetPlayerEntryError += OnGetPlayerEntryError;
        _leaderBoardService.GetPlayerEntrySuccess += OnGetPlayerEntrySuccess;
        _leaderBoardService.GetLeaderBoardPlayer();

        _leaderBoardService.GetLeaderboardEntriesSuccess += OnGetLeaderboardEntriesSuccess;
        _leaderBoardService.GetLeaderboardEntriesError += OnGetLeaderboardEntriesError;
        _leaderBoardService.GetLeaderBoardEntries();
    }

    private void OnDisable()
    {
        _leaderBoardService.GetPlayerEntryError -= OnGetPlayerEntryError;
        _leaderBoardService.GetPlayerEntrySuccess -= OnGetPlayerEntrySuccess;
    }
    
    private void OnGetLeaderboardEntriesError(string errorMessage)
    {
        _errorMessage.text = errorMessage;
        Debug.Log(errorMessage);
    }

    private void OnGetLeaderboardEntriesSuccess(LeaderboardGetEntriesResponse result)
    {
        if (_leaderBoardUserTemplates.Count > 0)
        {
            for (int i = 0; i < _leaderBoardUserTemplates.Count; i++)
            {
                var template = _leaderBoardUserTemplates[i];
                Destroy(template.gameObject);
            }
            
            _leaderBoardUserTemplates.Clear();
        }
        
        foreach (var entry in result.entries)
        {
            string name = entry.player.publicName;
            if (string.IsNullOrEmpty(name))
                name = "Anonymous";
            
            LeaderBoardUserTemplate template = Instantiate(_leaderBoardUserTemplate, _content);
            template.Initial(name,entry.score.ToString(),entry.rank.ToString());
            _leaderBoardUserTemplates.Add(template);
            
            Debug.Log(name + " " + entry.score);
        }
    }

    private void OnGetPlayerEntrySuccess(LeaderboardEntryResponse result)
    {
        if (result == null)
        {
            _errorMessage.text = "Player is not present in the leaderboard.";
            Debug.Log(_errorMessage.text);
        }
        else
        {
            _playerEntry = result;
            _rank.text = result.rank.ToString();
            _score.text = result.score.ToString();
            _formattedScore.text = result.formattedScore;
        }
    }

    private void OnGetPlayerEntryError(string errorMessage)
    {
        _errorMessage.text = errorMessage;
        Debug.Log(errorMessage);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        _closeButton.onClick.AddListener(Disable);
        Opened?.Invoke();
    }

    public void Disable()
    {
        if (gameObject.activeSelf)
        {
            _closeButton.onClick.RemoveListener(Disable);
            gameObject.SetActive(false);
            Closed?.Invoke();
        }
    }
}