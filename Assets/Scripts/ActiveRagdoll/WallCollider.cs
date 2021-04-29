using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class WallCollider : MonoBehaviour
{
    public event UnityAction<Person> TouchedPerson;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out BodyPart bodyPart))
            TouchedPerson?.Invoke(bodyPart.Body);
    }
}
