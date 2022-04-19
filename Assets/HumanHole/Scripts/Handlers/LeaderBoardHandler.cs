using System;
using Agava.YandexGames;
using HumanHole.Scripts.Infrastructure.Services.Authorization;
using HumanHole.Scripts.Infrastructure.Services.LeaderBoard;
using UnityEngine;

namespace HumanHole.Scripts.Handlers
{
    public class LeaderBoardHandler : MonoBehaviour
    {
        private ILeaderBoardService _leaderBoardService;
        private LevelHandler _levelHandler;
        private LevelPointsHandler _levelPointsHandler;
        private IAuthorizationService _authorizationService;

        public void Initial(LevelHandler levelHandler, LevelPointsHandler levelPointsHandler, ILeaderBoardService leaderBoardService, IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            _levelHandler = levelHandler;
            _levelPointsHandler = levelPointsHandler;
            _leaderBoardService = leaderBoardService;
        }

        public void Enable() => 
            gameObject.SetActive(true);

        private void OnEnable() => 
            _levelHandler.LevelWon += OnLevelWon;

        private void OnDisable() => 
            _levelHandler.LevelWon -= OnLevelWon;

        private void OnLevelWon()
        {
            if (_authorizationService.IsAuthorized)
            {
                _leaderBoardService.GetPlayerEntrySuccess += OnGetPlayerEntrySuccess;
                _leaderBoardService.GetLeaderBoardPlayer();
            }
        }

        private void OnGetPlayerEntrySuccess(LeaderboardEntryResponse result)
        {
            _leaderBoardService.GetPlayerEntrySuccess -= OnGetPlayerEntrySuccess;
            int currentScore = 0;
            if (result != null)
            {
                currentScore = result.score;
            }
            else
            {
                throw new NullReferenceException("LeaderboardEntryResponseResult is null");
            }

            currentScore += _levelPointsHandler.LevelPoints;
            _leaderBoardService.SetLeaderBoardScore(currentScore);
        }
    }
}
