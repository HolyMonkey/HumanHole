using UnityEngine;
using UnityEngine.UI;

public class BalanceSlider : MonoBehaviour
{
    private bool _isAllowed;
    private Person _person;
    private Slider _slider;
    
    [SerializeField] private Image _handle;
    [SerializeField] private Color _dangerColor;
    [SerializeField] private Color _warningColor;
    [SerializeField] private Color _okColor;

    public void Initial(Person person)
    {
        _slider = GetComponent<Slider>();
        _person = person;
    }

    public void Enable() => 
        gameObject.SetActive(true);

    private void Update()
    {
        float balanceValue = Mathf.Abs(_person.Balance);
        Color newColor;
        if (balanceValue < 0.5f)
        {
            newColor = Color.Lerp(_okColor, _warningColor, balanceValue * 2);
        }
        else
        {
            newColor = Color.Lerp(_warningColor, _dangerColor, (balanceValue - 0.5f) * 2);
        }

        _handle.color = newColor;
        _slider.value = _person.Balance;
    }
}