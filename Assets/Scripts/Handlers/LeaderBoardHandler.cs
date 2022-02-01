using CodeBase.Infrastructure.Services.LeaderBoard;
using UnityEngine;
using YandexGames;

public class LeaderBoardHandler : MonoBehaviour
{
    private ILeaderBoardService _leaderBoardService;
    
    private LevelHandler _levelHandler;
    private LevelPointsHandler _levelPointsHandler;

    public void Initial(LevelHandler levelHandler, LevelPointsHandler levelPointsHandler, ILeaderBoardService leaderBoardService)
    {
        _levelHandler = levelHandler;
        _levelPointsHandler = levelPointsHandler;
        _leaderBoardService = leaderBoardService;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }
    
    private void OnEnable()
    {
        _levelHandler.LevelWon += OnLevelWon;
    }

    private void OnDisable()
    {
        _levelHandler.LevelWon -= OnLevelWon;
    }

    private void OnLevelWon()
    {
        _leaderBoardService.GetPlayerEntrySuccess += OnGetPlayerEntrySuccess;
        _leaderBoardService.GetLeaderBoardPlayer();
    }

    private void OnGetPlayerEntrySuccess(LeaderboardEntryResponse result)
    {
        _leaderBoardService.GetPlayerEntrySuccess -= OnGetPlayerEntrySuccess;
        int currentScore = 0;
        if (result != null)
        {
            currentScore = result.score;
            Debug.Log("Player is not present in the leaderboard.");
        }

        currentScore += _levelPointsHandler.LevelPoints;
        _leaderBoardService.SetLeaderBoardScore(currentScore);
    }
}
