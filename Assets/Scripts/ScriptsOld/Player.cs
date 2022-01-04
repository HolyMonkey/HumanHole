using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

[RequireComponent(typeof(FullBodyBipedIK))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    private Collider[] _ragdollColliders;
    private Rigidbody[] _ragdollRigitbodies;
    private FullBodyBipedIK _iK;
    private Animator _animator;

    public void SetIkActive(bool active)
    {
        _iK.enabled = active;
        _animator.SetBool("isReady", active);
    }

    public void Dance()
    {
        _iK.enabled = false;
        _animator.SetBool("isDancing", true);
    }

    private void Awake()
    {
        _ragdollColliders = GetComponentsInChildren<Collider>();
        _ragdollRigitbodies = GetComponentsInChildren<Rigidbody>();
        _iK = GetComponent<FullBodyBipedIK>();
        _animator = GetComponent<Animator>();

        SetRagdollActive(false);
    }

    private void SetRagdollActive(bool active)
    {
        foreach (var item in _ragdollColliders)
            item.enabled = active;

        foreach (var item in _ragdollRigitbodies)
            item.isKinematic = !active;
    }
}
