using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HumanHole.Scripts.UI.Panels
{
    public class WonLevelPanel : MonoBehaviour, ILevelPanel, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI _levelGoldText;
        [SerializeField] private TextMeshProUGUI _levelAttemptsText;
        [SerializeField] private TextMeshProUGUI _levelPointsText;
        [SerializeField] private TextMeshProUGUI _allPointsText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _rewardButton;
        [SerializeField] private Button _nextButton;

        public event Action RewardedAdButtonClick;
        public event Action RestartButtonClick;
        
        public event Action Opened;
        public event Action Closed;
        public event Action Clicked;

        public void OnPointerClick(PointerEventData eventData) => 
            OnClicked();
        

        public void OnEnabled()
        {
            gameObject.SetActive(true);
            _nextButton.onClick.AddListener(OnClicked);
            _restartButton.onClick.AddListener(OnRestartButtonClick);
            _rewardButton.onClick.AddListener(OnRewardedAdButtonClick);
            Opened?.Invoke();
        }

        public void OnDisabled()
        {
            gameObject.SetActive(false);
            _nextButton.onClick.RemoveListener(OnClicked);
            _restartButton.onClick.RemoveListener(OnRestartButtonClick);
            _rewardButton.onClick.RemoveListener(OnRewardedAdButtonClick);
            Closed?.Invoke();
        }

        public void OnClicked()
        {
            OnDisabled();
            Clicked?.Invoke();
        }

        public void SetGold(int gold) =>
            _levelGoldText.text = $"+{gold}";

        public void SetAttempts(int attempts) => 
            _levelAttemptsText.text = attempts.ToString();
        
        public void DisableRewardButton() => 
            _rewardButton.interactable = false;

        public void SetLevelPoints(int points) => 
            _levelPointsText.text = points.ToString();

        public void SetAllPoints(int points) => 
            _allPointsText.text = points.ToString();

        private void OnRestartButtonClick() => 
            RestartButtonClick?.Invoke();

        private void OnRewardedAdButtonClick() => 
            RewardedAdButtonClick?.Invoke();
    }
}
