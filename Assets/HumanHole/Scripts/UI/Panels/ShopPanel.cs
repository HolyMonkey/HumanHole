using System;
using System.Collections.Generic;
using System.Linq;
using HumanHole.Scripts.Data;
using HumanHole.Scripts.Infrastructure.Services.Factory;
using HumanHole.Scripts.Infrastructure.Services.SaveLoad;
using HumanHole.Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HumanHole.Scripts.UI.Panels
{
    public class ShopPanel : MonoBehaviour, ILevelPanel
    {
        [SerializeField] private Characters _characters;
        [SerializeField] private Color _selectedBorderColor;
        [SerializeField] private Color _delectedBorderColor;
        [SerializeField] private TextMeshProUGUI _openPriceText;
        [SerializeField] private CharacterIconTemplate _characterIconTemplate;
        [SerializeField] private Transform _content;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _closeButton;

        public event Action Opened;
        public event Action Closed;
        public event Action Clicked;

        public event Action<int> CharacterSelected;

        private readonly List<CharacterIconTemplate> _characterIconTemplates = new List<CharacterIconTemplate>();
        
        private List<int> _openedCharactersIds;
        private List<int> _closedCharactersIds = new List<int>();
        private CharacterIconTemplate _selectedCharacterTemplate;
        private CharactersProgress _charactersProgress;
        private ShopProgress _shopProgress;
        private GoldProgress _goldProgress;
        private ISaveLoadService _saveLoadService;
        private ClickZone _clickZone;
        private IFactoryService _factoryService;

        public void Initial(Progress progress, ISaveLoadService saveLoadService, ClickZone clickZone, IFactoryService factoryService)
        {
            _factoryService = factoryService;
            _clickZone = clickZone;
            _saveLoadService = saveLoadService;
            _charactersProgress = progress.CharactersProgress;
            _openedCharactersIds = _charactersProgress.OpenedCharactersIds;
            _shopProgress = progress.ShopProgress;
            _goldProgress = progress.GoldProgress;
            
            foreach (Сharacter character in _characters.GetCharacters()) 
                _closedCharactersIds.Add(character.Id);
            
            _closedCharactersIds = _closedCharactersIds.Except(_openedCharactersIds).ToList();
            _openPriceText.text = _shopProgress.OpenCharacterPrice.ToString();
            
            InitialShop();
        }

        public void OnEnabled()
        {
            _buyButton.onClick.AddListener(OnBuyButtonClick);
            _closeButton.onClick.AddListener(OnDisabled);
            _clickZone.Enable();
            _clickZone.Clicked += OnClicked;
            gameObject.SetActive(true);
            CharacterSelected?.Invoke(_selectedCharacterTemplate.Id);
            Opened?.Invoke();
        }

        public void OnDisabled()
        {
            if (gameObject.activeSelf)
            {
                _buyButton.onClick.RemoveListener(OnBuyButtonClick);
                _closeButton.onClick.RemoveListener(OnDisabled);
                _clickZone.Clicked -= OnClicked;
                _clickZone.Disable();
                gameObject.SetActive(false);
                Closed?.Invoke();
            }
        }

        public void OnStarted()
        {
            if(CanBuyCharacter())
                EnableBuyButton();
            else
                DisableBuyButton();
        }

        public void OnClicked()
        {
            OnDisabled();
            Clicked?.Invoke();
        }

        private void EnableBuyButton() => 
            _buyButton.interactable = true;

        private void DisableBuyButton() => 
            _buyButton.interactable = false;
        

        private void InitialShop()
        {
            foreach (Сharacter character in _characters.GetCharacters())
            {
                CharacterIconTemplate characterIconTemplate = _factoryService.Create(_characterIconTemplate, _content);
                characterIconTemplate.Initial(character.Id, character.Icon, OnCharacterSelected);
                _characterIconTemplates.Add(characterIconTemplate);

                if (character.Id == _charactersProgress.SelectedCharacterId)
                {
                    _selectedCharacterTemplate = characterIconTemplate;
                    _selectedCharacterTemplate.Select(_selectedBorderColor);
                }

                foreach (int openedCharacterId in _openedCharactersIds)
                {
                    if (character.Id == openedCharacterId)
                    {
                        characterIconTemplate.Open();
                        break;
                    }
                }
            }
        }

        private void OnBuyButtonClick()
        {
            int randomValue = UnityEngine.Random.Range(0, _closedCharactersIds.Count);
            int id = _closedCharactersIds[randomValue];
            CharacterIconTemplate characterIconTemplate = _characterIconTemplates.FirstOrDefault(x => x.Id == id);
            if (characterIconTemplate == null)
                throw new NullReferenceException($"Character template with id {id} does not exist");
            characterIconTemplate.Open();
            _closedCharactersIds.Remove(id);
            _charactersProgress.Open(id);
            _goldProgress.Remove(_shopProgress.OpenCharacterPrice);
            _shopProgress.UpdatePrice();
            _saveLoadService.SaveProgress();
            
            _openPriceText.text = _shopProgress.OpenCharacterPrice.ToString();
            
            if(CanBuyCharacter())
                EnableBuyButton();
            else
                DisableBuyButton();
        }

        private void OnCharacterSelected(CharacterIconTemplate characterIconTemplate)
        {
            if (_selectedCharacterTemplate.Id != characterIconTemplate.Id)
            {
                _selectedCharacterTemplate.Deselect(_delectedBorderColor);
                _selectedCharacterTemplate = characterIconTemplate;
                _selectedCharacterTemplate.Select(_selectedBorderColor);
                _charactersProgress.Select(_selectedCharacterTemplate.Id);
                CharacterSelected?.Invoke(_selectedCharacterTemplate.Id);
                _saveLoadService.SaveProgress();
            }
        }

        private bool CanBuyCharacter() =>
            _openedCharactersIds.Count < _characters.GetCharacters().Length &&
            _goldProgress.Count >= _shopProgress.OpenCharacterPrice;
    }
}
