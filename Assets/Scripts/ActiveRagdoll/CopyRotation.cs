using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRotation : MonoBehaviour
{
    [SerializeField] private Transform _original;

    private void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(_original.forward, Vector3.up);
    }
}
