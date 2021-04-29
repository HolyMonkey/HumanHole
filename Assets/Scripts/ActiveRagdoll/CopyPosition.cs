using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    [SerializeField] private Transform _original;
    [SerializeField] private Vector3 _offset;

    private void LateUpdate()
    {
        transform.position = _original.transform.position + _offset;
    }
}
