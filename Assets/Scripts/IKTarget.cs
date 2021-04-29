using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTarget : MonoBehaviour
{
    [SerializeField] private Transform _initialPosition;

    private Vector3 _startPosition;

    public void ResetPosition()
    {
        transform.position = _initialPosition.position;
        _startPosition = _initialPosition.position;
    }
}
