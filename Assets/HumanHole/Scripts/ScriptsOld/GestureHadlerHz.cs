using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureHadlerHz : MonoBehaviour
{
    [SerializeField] private Control[] _controls;
    [SerializeField] private HandPointerHandler _pointerHandler;

    private Control _selectedControl;

    private void OnEnable()
    {
        _pointerHandler.MouseDown += OnCursorDown;
        _pointerHandler.MouseUp += OnCursorUp;

        foreach (var item in _controls)
            item.gameObject.SetActive(true);

        foreach (var item in _controls)
            item.ResetPosition();
    }

    private void OnDisable()
    {
        _pointerHandler.MouseDown -= OnCursorDown;
        _pointerHandler.MouseUp -= OnCursorUp;

        foreach (var item in _controls)
            item.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnCursorDown(Input.mousePosition);
        else if (Input.GetMouseButtonUp(0))
            DeselectAll();


        if (Input.GetMouseButton(0))
        {
            if (_selectedControl != null)
            {
                _selectedControl.Move(Input.mousePosition);
            }
        }

        //if (_pointer.IsPressing)
        //    if (_selectedControl != null)
        //        _selectedControl.Move(Input.mousePosition);
    }

    private void OnCursorDown(Vector2 mousePosition)
    {
        foreach (var item in _controls)
        {
            if (item.Rect.Contains(mousePosition))
            {
                Select(item);
                break;
            }
        }
    }

    private void OnCursorUp(Vector2 mousePosition)
    {
        DeselectAll();
    }

    private void Start()
    {
        foreach (var item in _controls)
        {
            Debug.Log(item.Rect);
        }
    }

    private void Select(Control control)
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
