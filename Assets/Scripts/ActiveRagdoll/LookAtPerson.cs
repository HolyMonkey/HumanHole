using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPerson : MonoBehaviour
{
    private Transform _target;
    private Vector3 _velocity;
    private Vector3 _lastLookPoint;

    public void SetTarget(Transform target)
    {
        if (_target == null)
            _lastLookPoint = target.transform.position;

        _target = target;
    }

    private void Update()
    {
        if (_target == null)
            return;

        _lastLookPoint = Vector3.SmoothDamp(_lastLookPoint, _target.position, ref _velocity, 0.2f);

        transform.rotation = Quaternion.LookRotation(_lastLookPoint - transform.position, Vector3.up);
    }
}
