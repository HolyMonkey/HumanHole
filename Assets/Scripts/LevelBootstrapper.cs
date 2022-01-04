using System;
using UnityEngine;

public class LevelBootstrapper : MonoBehaviour
{
    private ICursorService _cursorService;

    [SerializeField] private LevelHandler _levelHandler;
    [SerializeField] private GestureHandler _gestureHandler;
    [SerializeField] private HandPointer _handPointer;
    [SerializeField] private CollisionObserver _collisionObserver;
    [SerializeField] private Contours _contours;
    [SerializeField] private LevelUI _levelUI;
    [SerializeField] private LevelPanelsStateMachine _levelPanelsStateMachine;
    [SerializeField] private GameObject _uiContent;
    

    public void Start()
    {
        Initial();
    }

    private void Initial()
    {
        _levelPanelsStateMachine.Initial();
        _levelUI.Initial();
        _contours.Initial();
        _levelHandler.Initial();
        _handPointer.Initial();
    }

    private void OnEnable()
    {
        Enable();
    }

    private void OnDisable()
    {
      //  Disable();
    }

    private void Enable()
    {
        gameObject.SetActive(true);

        _uiContent.SetActive(true);
        _gestureHandler.Enable();
        _collisionObserver.Enable();
        _contours.Enable();
        _levelHandler.Enable();
    }

    private void Disable()
    {
        _uiContent.SetActive(false);
        _collisionObserver.Disable();
        _levelHandler.Disable();
        _gestureHandler.Disable();
        _contours.Disable();
    }
}