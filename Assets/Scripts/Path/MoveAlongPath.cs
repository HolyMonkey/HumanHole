using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongPath : MonoBehaviour
{
    [SerializeField] private SmoothPathVisualization _pathSource;

    private SmoothPath _path;
    private Vector3 _velocity;
    private bool _isMoving;
    private float _duration = 1;
    private float _time = 0;
    private CubicBezier _easing = new CubicBezier(BezierCurveType.EaseInOut);

    public void MoveToEnd(float duration)
    {
        _duration = duration;
        _isMoving = true;
    }

    private void Awake()
    {
        _path = _pathSource.CreatePath();
    }

    private void Update()
    {
        if (_isMoving && _time < _duration)
        {
            float value = _easing.GetValue(_time / _duration);
            transform.position = Vector3.SmoothDamp(transform.position, _path.GetPosition(value), ref _velocity, 0.1f);
            _time += Time.deltaTime;
        }
    }
}
