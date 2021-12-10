using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartGameTapPanel : MonoBehaviour, IPointerClickHandler
{
    private int tapAnimationHash = Animator.StringToHash("Tap");
        
    [SerializeField] private Animator _handAnimator;

    public event Action Clicked;

    public void Enable()
    {
        gameObject.SetActive(true);
        PlayHandAnimation();
    }

    private void PlayHandAnimation()
    {
        _handAnimator.SetBool(tapAnimationHash, true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked?.Invoke();
    }
    
}
