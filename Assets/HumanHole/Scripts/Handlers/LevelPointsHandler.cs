using HumanHole.Scripts.Data;
using HumanHole.Scripts.Infrastructure.Services.SaveLoad;
using HumanHole.Scripts.Level;
using HumanHole.Scripts.UI;
using HumanHole.Scripts.UI.Panels;

namespace HumanHole.Scripts.Handlers
{
    public class LevelPointsHandler
    {
        public int LevelPoints { get; set; }
        
        private LevelHandler _levelHandler;
        private Progress _progress;
        private WonLevelPanel _wonLevelPanel;
        private LostLevelPanel _lostLevelPanel;
        private LevelStaticData _levelStaticData;
        private ISaveLoadService _saveLoadService;

        private int _progressPoints;
        private bool _wonLevel;
        
        public void Initial(LevelHandler levelHandler, Progress progress,
            ISaveLoadService saveLoadService, LevelPanelsStateMachine levelPanelsStateMachine, LevelStaticData levelStaticData)
        {
            _levelStaticData = levelStaticData;
            _saveLoadService = saveLoadService;
            _progress = progress;
            _levelHandler = levelHandler;
            _wonLevelPanel = levelPanelsStateMachine.GetPanel<WonLevelPanel>();
            _lostLevelPanel = levelPanelsStateMachine.GetPanel<LostLevelPanel>();
        }

        public void OnEnabled()
        {
            _levelHandler.LevelWon += OnLevelWon;
            _levelHandler.LevelLost += OnLevelLost;
            _progress.PointsProgress.Changed += OnPointsChanged;
        }

        public void OnDisabled()
        {
            _levelHandler.LevelWon -= OnLevelWon;
            _levelHandler.LevelLost -= OnLevelLost;
            _progress.PointsProgress.Changed -= OnPointsChanged;
        }

        private void OnPointsChanged()
        {
            _progressPoints = _progress.PointsProgress.Count;
            if(_wonLevel)
                SetLevelWonPoints(LevelPoints, _progressPoints);
            else
                SetLevelLosePoints(LevelPoints, _progressPoints);
        }

        private void OnLevelWon()
        {
            _wonLevel = true;
            SetLevelPoints(_levelStaticData.LevelWonPoints);
            SavePoints(_levelStaticData.LevelWonPoints);
        }

        private void OnLevelLost()
        {
            SetLevelPoints(_levelStaticData.LevelLosePoints);
            SavePoints(_levelStaticData.LevelLosePoints);
        }

        private void SetLevelWonPoints(int levelPoints, int allPoints)
        {
            _wonLevelPanel.SetLevelPoints(levelPoints);
            _wonLevelPanel.SetAllPoints(allPoints);
        }

        private void SetLevelLosePoints(int levelPoints, int allPoints)
        {
            _lostLevelPanel.SetLevelPoints(levelPoints);
            _lostLevelPanel.SetAllPoints(allPoints);
        }

        private void SetLevelPoints(int points) =>
            LevelPoints = points;
        
        private void SavePoints(int points)
        {
            _progress.PointsProgress.Add(points);
            _saveLoadService.SaveProgress();
        }
    }
}