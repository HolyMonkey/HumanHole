using System;
using YandexGames;

namespace CodeBase.Infrastructure.Services.LeaderBoard
{
    public class LeaderBoardService: ILeaderBoardService
    {
        private const string LeaderboardName = "MainLeaderboard";

        public event Action<LeaderboardEntryResponse> GetPlayerEntrySuccess;
        public event Action<string> GetPlayerEntryError;

        public event Action<LeaderboardGetEntriesResponse> GetLeaderboardEntriesSuccess;
        public event Action<string> GetLeaderboardEntriesError;

        public void SetLeaderBoardScore(int score)
        {
            #if UNITY_WEBGL && !UNITY_EDITOR
            Leaderboard.SetScore(LeaderboardName, score);
#endif
        }

        public void GetLeaderBoardEntries()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            Leaderboard.GetEntries(LeaderboardName,GetLeaderboardEntriesSuccess, GetLeaderboardEntriesError);
#endif
        }

        public void GetLeaderBoardPlayer()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            Leaderboard.GetPlayerEntry(LeaderboardName, GetPlayerEntrySuccess, GetPlayerEntryError);
#endif
        }
    }
}