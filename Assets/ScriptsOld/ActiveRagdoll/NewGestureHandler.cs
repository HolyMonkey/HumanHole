using UnityEngine;
using Zenject;

public class NewGestureHandler : MonoBehaviour
{
    [SerializeField] private LimbControl[] _controls;
    [SerializeField] private Person _person;
    [SerializeField] private СollisionObserver _collisionObserver;
        
    private LimbControl _selectedControl;
    private GameRunner _gameRunner;
    
    [Inject]
    private void Contruct(GameRunner gameRunner)
    {
        _gameRunner = gameRunner;
    }

    private void OnEnable()
    {
        _gameRunner.GameStarted += GameStarted;
        _gameRunner.GameFinished += GameFinished;
        _collisionObserver.WallHitPerson += Disable;
    }

    private void OnDisable()
    {
        _gameRunner.GameStarted -= GameStarted;
        _gameRunner.GameFinished -= GameFinished;

        _collisionObserver.WallHitPerson -= Disable;
    }

    private void GameFinished()
    {
        foreach (var item in _controls)
            item.enabled = false;
    }

    private void GameStarted()
    {
        foreach (var item in _controls)
            item.enabled = true;
    }
    
    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(!_gameRunner.IsGameStarted)
            return;
        
        if (Input.GetMouseButtonDown(0))
            OnCursorDown(Input.mousePosition);
        else if (Input.GetMouseButtonUp(0))
        {
            DeselectAll();
            if (Mathf.Abs(_person.Balance) > 0.8f)
            {
                foreach (var item in _controls)
                {
                    item.BreakJoint();
                }
            }
        }


        if (Input.GetMouseButton(0))
        {
            if (_selectedControl != null)
            {
                _selectedControl.Move(Input.mousePosition);
            }
        }
    }

    private void OnCursorDown(Vector2 mousePosition)
    {
        Debug.Log("Mouse: " + mousePosition.ToString());
        foreach (var item in _controls)
        {
            Debug.Log("rect: " + item.Rect.ToString());
            if (item.Rect.Contains(mousePosition))
            {
                Select(item);
                break;
            }
        }
    }

    private void Select(LimbControl control)
    {
        _selectedControl = control;
        foreach (var item in _controls)
        {
            if (control == item)
                item.Select();
            else
                item.Deselect();
        }
    }

    private void DeselectAll()
    {
        _selectedControl = null;
        foreach (var item in _controls)
            item.Deselect();
    }
}
