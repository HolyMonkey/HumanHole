using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpringJoint))]
public class Floating : MonoBehaviour
{
    [SerializeField] private Transform _anchorPoint;

    private void OnEnable()
    {
        GetComponent<SpringJoint>().connectedAnchor = _anchorPoint.localPosition;
    }
}
