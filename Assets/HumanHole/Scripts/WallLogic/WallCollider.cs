using System;
using HumanHole.Scripts.ActiveRagdoll;
using UnityEngine;

namespace HumanHole.Scripts.Wall
{
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
}
