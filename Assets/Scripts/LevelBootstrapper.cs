using System;
using UnityEngine;

public class LevelBootstrapper : MonoBehaviour
{
    [SerializeField] private LevelHandler _levelHandler;
    [SerializeField] private GestureHandler _gestureHandler;
    [SerializeField] private HandPointerHandler _handPointerHandler;
    [SerializeField] private CollisionObserver _collisionObserver;
    [SerializeField] private Contours _contours;
    [SerializeField] private LevelUI _levelUI;
    [SerializeField] private LevelPanelsStateMachine _levelPanelsStateMachine;
    [SerializeField] private AdHandler _adHandler;
    [SerializeField] private LevelPauseHandler _levelPauseHandler;
    [SerializeField] private AuthorizationHandler _authorizationHandler;

    public void Awake()
    {
        Initial();
    }

    private void Initial()
    {
        _levelPanelsStateMachine.Initial();
        _levelUI.Initial();
        _contours.Initial();
       // _handPointerHandler.Initial();
        _adHandler.Initial();
        _levelPauseHandler.Initial();
        _gestureHandler.Initial();
        _levelHandler.Initial();
        _authorizationHandler.Initial();
    }

    private void OnEnable()
    {
        _adHandler.Enable();
        _contours.Enable();
        _levelHandler.Enable();
        _authorizationHandler.Enable();
    }
}