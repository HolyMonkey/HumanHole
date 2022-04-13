using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class LostLevelPanel : MonoBehaviour, ILevelPanel
{
    private bool _clicked;
    private TapHandHandler _tapHandHandler;
    
    public event Action Clicked;

    public void Initialize(TapHandHandler tapHandHandler) => 
        _tapHandHandler = tapHandHandler;

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
