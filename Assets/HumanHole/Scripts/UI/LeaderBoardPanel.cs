using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agava.YandexGames;
using HumanHole.Scripts.Infrastructure;
using HumanHole.Scripts.Infrastructure.Services.Authorization;
using HumanHole.Scripts.Infrastructure.Services.Download;
using HumanHole.Scripts.Infrastructure.Services.LeaderBoard;
using UnityEngine;
using UnityEngine.UI;

namespace HumanHole.Scripts.UI
{
    public class LeaderBoardPanel : MonoBehaviour
    {
        [SerializeField] private LeaderBoardUserTemplate _leaderBoardUserTemplate;
        [SerializeField] private Transform _content;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _continueButton;
    
        private ILeaderBoardService _leaderBoardService;
        private IDownloadService _downloadService;
        private IAuthorizationService _authorizationService;
        private LeaderboardEntryResponse _playerEntry;
        private readonly List<LeaderBoardUserTemplate> _leaderBoardUserTemplates = new List<LeaderBoardUserTemplate>();
        private readonly Vector2 _avatarSize = new Vector2(80, 80);
        private readonly string _emptyName = "Anonymous";

        public event Action Opened;
        public event Action Closed;
    
        public void Initial()
        {
            var services = Game.Instance.AllServices;
            _leaderBoardService = services.Single<ILeaderBoardService>();
            _downloadService = services.Single<IDownloadService>();
            _authorizationService = services.Single<IAuthorizationService>();
        }

        public void Start()
        {
            if (_authorizationService.IsAuthorized)
            {
                GetLeaderBoard();
            }
            else
            {
                _authorizationService.Authorized += OnAuthorized;
            }
        }

        private void GetLeaderBoard()
        {
            _leaderBoardService.GetPlayerEntryError += OnGetPlayerEntryError;
            _leaderBoardService.GetPlayerEntrySuccess += OnGetPlayerEntrySuccess;
            _leaderBoardService.GetLeaderBoardPlayer();
        }

        private void OnAuthorized()
        {
            _authorizationService.Authorized -= OnAuthorized;
            GetLeaderBoard();
        }

        public void Enable()
        {
            gameObject.SetActive(true);
            Opened?.Invoke();
        }

        public void Disable()
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
                Closed?.Invoke();
            }
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(Disable);
            _continueButton.onClick.AddListener(Disable);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(Disable);
            _continueButton.onClick.RemoveListener(Disable);
        }

        private void OnGetLeaderboardEntriesError(string errorMessage) => 
            _leaderBoardService.GetLeaderboardEntriesError -= OnGetLeaderboardEntriesError;

        private void OnGetLeaderboardEntriesSuccess(LeaderboardGetEntriesResponse result)
        {
            _leaderBoardService.GetLeaderboardEntriesSuccess -= OnGetLeaderboardEntriesSuccess;
            _ = CreateLeaderboard(result);
        }

        private async Task CreateLeaderboard(LeaderboardGetEntriesResponse result)
        {
            foreach (var entry in result.entries)
            {
                string name = entry.player.publicName;
                if (string.IsNullOrEmpty(name)) 
                    name = _emptyName;

                LeaderBoardUserTemplate template = Instantiate(_leaderBoardUserTemplate, _content);
                bool ownPlayer = _playerEntry.player.uniqueID == entry.player.uniqueID;

                string avatarUrl = entry.player.scopePermissions.avatar;
                Sprite avatarSprite = null;
                if (!string.IsNullOrEmpty(entry.player.scopePermissions.avatar))
                {
                    Texture2D texture2D = await _downloadService.DownloadPreview(avatarUrl);
                    avatarSprite = CreateSprite(texture2D, _avatarSize.x, _avatarSize.y);
                }

                template.Initial(name, entry.formattedScore, entry.rank, avatarSprite, ownPlayer);
                _leaderBoardUserTemplates.Add(template);
            }
        }

        private void OnGetPlayerEntrySuccess(LeaderboardEntryResponse result)
        {
            _leaderBoardService.GetPlayerEntrySuccess -= OnGetPlayerEntrySuccess;
            if (result != null)
            {
                _playerEntry = result;
                _leaderBoardService.GetLeaderboardEntriesSuccess += OnGetLeaderboardEntriesSuccess;
                _leaderBoardService.GetLeaderboardEntriesError += OnGetLeaderboardEntriesError;
                _leaderBoardService.GetLeaderBoardEntries();
            }
        }

        private void OnGetPlayerEntryError(string errorMessage) => 
            _leaderBoardService.GetPlayerEntryError -= OnGetPlayerEntryError;

        private Sprite CreateSprite(Texture2D texture, float width, float height)
        {
            if (width == 0) 
                width = texture.width;

            if (height == 0)
                height = texture.height;
        
            return Sprite.Create(texture, new Rect(0.0f, 0.0f, width, height), new Vector2(0.5f, 0.5f), 100.0f);
        }
    }
}