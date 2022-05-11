using System;
using UnityEngine;
using UnityEngine.UI;

namespace HumanHole.Scripts.UI
{
    public class CharacterIconTemplate : MonoBehaviour
    {
        public int Id { get; private set; }

        [SerializeField] private Image _borderImage;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Image _closedImage;
        [SerializeField] private Toggle _toggle;
        
        private event Action<CharacterIconTemplate> _selected;

        private void OnEnable() => 
            _toggle.onValueChanged.AddListener(OnToggleValueChanged);

        private void OnDisable() => 
            _toggle.onValueChanged.RemoveListener(OnToggleValueChanged);

        public void Initial(int id, Sprite sprite, Action<CharacterIconTemplate> onSelected)
        {
            Id = id;
            _iconImage.sprite = sprite;
            _selected = onSelected;
        }

        public void Select(Color color) => 
            _borderImage.color = color;

        public void Deselect(Color color)
        {
            _borderImage.color = color;
            _toggle.isOn = false;
        }

        public void Open()
        {
            _closedImage.gameObject.SetActive(false);
            _iconImage.gameObject.SetActive(true);
            _toggle.interactable = true;
        }

        private void OnToggleValueChanged(bool value)
        {
            if(value)
                _selected?.Invoke(this);
        }
    }
}
