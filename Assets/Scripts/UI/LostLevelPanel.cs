using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class LostLevelPanel : MonoBehaviour, ILevelPanel
{
    private bool _clicked;

    [SerializeField] private TapHandHandler _tapHandHandler;
    
    public event Action Clicked;

    public void Enable()
    {
        gameObject.SetActive(true);
        _tapHandHandler.Enable();
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        _tapHandHandler.Disable();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_clicked)
        {
            _clicked = true;
            Clicked?.Invoke();
        }
    }
}
