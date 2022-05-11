using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HumanHole.Scripts.UI.Panels
{
    public class LostLevelPanel : MonoBehaviour, ILevelPanel, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI _levelGoldText;
        [SerializeField] private TextMeshProUGUI _levelAttemptsText;
        [SerializeField] private TextMeshProUGUI _levelPointsText;
        [SerializeField] private TextMeshProUGUI _allPointsText;
        [SerializeField] private Button _rewardButton;
        [SerializeField] private Button _restartButton;
        
        public event Action Opened;
        public event Action Closed;
        public event Action Clicked;
        
        public event Action RewardedAdButtonClick;
        public event Action RestartButtonClick;

        public void OnPointerClick(PointerEventData eventData) => 
            OnClicked();

        public void OnEnabled()
        {
            gameObject.SetActive(true);
            _restartButton.onClick.AddListener(OnRestartButtonClick);
            _rewardButton.onClick.AddListener(OnRewardedAdButtonClick);
            Opened?.Invoke();
        }

        public void OnDisabled()
        {
            gameObject.SetActive(false);
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

        public void DisableRewardButton() => 
            _rewardButton.interactable = false;
        
        public void SetAttempts(int attempts) => 
            _levelAttemptsText.text = attempts.ToString();
        
        public void SetLevelPoints(int levelPoints) => 
            _levelPointsText.text = levelPoints.ToString();

        public void SetAllPoints(int allPoints) => 
            _allPointsText.text = allPoints.ToString();

        private void OnRestartButtonClick() => 
            RestartButtonClick?.Invoke();

        private void OnRewardedAdButtonClick() => 
            RewardedAdButtonClick?.Invoke();
    }
}
