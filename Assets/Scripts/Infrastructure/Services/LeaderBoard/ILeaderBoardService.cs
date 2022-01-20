using System;
using YandexGames;

namespace CodeBase.Infrastructure.Services.LeaderBoard
{
    public interface ILeaderBoardService: IService
    {
        event Action<LeaderboardEntryResponse> GetPlayerEntrySuccess;
        event Action<string> GetPlayerEntryError;
        event Action<LeaderboardGetEntriesResponse> GetLeaderboardEntriesSuccess;
        event Action<string> GetLeaderboardEntriesError;
        void SetLeaderBoardScore(int score);
        void GetLeaderBoardEntries();
        void GetLeaderBoardPlayer();
    }
}