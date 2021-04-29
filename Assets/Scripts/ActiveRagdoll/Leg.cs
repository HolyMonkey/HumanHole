using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
public class Leg : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private ConfigurableJoint _joint;
    private Quaternion _startRotation;

    private void Awake()
    {
        _joint = GetComponent<ConfigurableJoint>();
        _startRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        _joint.targetRotation = Quaternion.Inverse(Quaternion.LookRotation(_startRotation * Quaternion.Euler(0, 180, 0) * (_target.position - transform.position)));
    }
}
