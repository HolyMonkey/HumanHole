using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class WallCollider : MonoBehaviour
{
    public event Action TouchedPerson;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out BodyPart bodyPart))
            TouchedPerson?.Invoke();
    }
}
