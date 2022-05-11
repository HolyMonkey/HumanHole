using System;
using System.Linq;
using HumanHole.Scripts.ActiveRagdoll;
using HumanHole.Scripts.Player;
using HumanHole.Scripts.UI.Panels;
using UnityEngine;

namespace HumanHole.Scripts.Shop
{
    public class ShopCharactersSpawner : MonoBehaviour
    {
        [SerializeField] private Сharacter[] _characters;
        [SerializeField] private DirectionalLight _directionalLight;

        private Сharacter _selectedCharacter;
        private Person _person;
        private ShopPanel _shopPanel;

        public void Initial(Person person, LevelPanelsStateMachine levelPanelsStateMachine)
        {
            _person = person;
            _shopPanel = levelPanelsStateMachine.GetPanel<ShopPanel>();
            foreach (Сharacter character in _characters) 
                character.SetPerson(_person);
        }

        public void OnEnabled()
        {
            _shopPanel.Closed += OnShopClosed;
            _shopPanel.CharacterSelected += OnShopCharacterSelected;
            _shopPanel.Opened += OnShopOpened;
        }
        
        public void OnDisabled()
        {
            _shopPanel.Closed -= OnShopClosed;
            _shopPanel.CharacterSelected -= OnShopCharacterSelected;
            _shopPanel.Opened -= OnShopOpened;
        }
        
        private void OnShopClosed()
        {
            DisableCharacter();
            _directionalLight.Disable();
        }

        private void OnShopOpened() => 
            _directionalLight.Enable();
        
        private void OnShopCharacterSelected(int id)
        {
            DisableCharacter();
            _selectedCharacter = _characters.FirstOrDefault(x => x.Id == id);
            if (_selectedCharacter == null)
                throw new NullReferenceException($"Character with id {id} is null");
            
            _selectedCharacter.Enable();
        }

        private void DisableCharacter() => 
            _selectedCharacter?.Disable();
        
    }
}