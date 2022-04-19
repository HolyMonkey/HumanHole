using HumanHole.Scripts.ActiveRagdoll;
using UnityEngine;
using UnityEngine.UI;

namespace HumanHole.Scripts.UI
{
    public class BalanceSlider : MonoBehaviour
    {
        [SerializeField] private Image _handle;
        [SerializeField] private Color _dangerColor;
        [SerializeField] private Color _warningColor;
        [SerializeField] private Color _okColor;
    
        private bool _isAllowed;
        private Person _person;
        private Slider _slider;
        private int _lerpTime;
        private float _balanceValue;

        public void Initial(Person person)
        {
            _slider = GetComponent<Slider>();
            _person = person;
        }

        public void Enable() => 
            gameObject.SetActive(true);

        private void Update()
        {
            float currentBalanceValue = Mathf.Abs(_person.Balance);
            Color newColor;
            _lerpTime = 2;
            _balanceValue = 0.5f;
            if (currentBalanceValue < _balanceValue)
            {
                newColor = Color.Lerp(_okColor, _warningColor, currentBalanceValue * _lerpTime);
            }
            else
            {
                newColor = Color.Lerp(_warningColor, _dangerColor, (currentBalanceValue - _balanceValue) * _lerpTime);
            }

            _handle.color = newColor;
            _slider.value = _person.Balance;
        }
    }
}