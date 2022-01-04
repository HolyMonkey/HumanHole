using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartLevelPanel : MonoBehaviour, ILevelPanel
{
    private bool _isClicked;
    
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
        if (!_isClicked)
        {
            _isClicked = true;
            Clicked?.Invoke();
            Disable();
        }
    }
    
}
