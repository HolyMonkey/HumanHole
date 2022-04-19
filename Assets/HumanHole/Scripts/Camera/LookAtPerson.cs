using HumanHole.Scripts.ActiveRagdoll;
using HumanHole.Scripts.Handlers;
using UnityEngine;

namespace HumanHole.Scripts.Camera
{
    public class LookAtPerson : MonoBehaviour
    {
        private LevelHandler _levelHandler;
        private Person _person;
        private Transform _target;
        private Vector3 _velocity;
        private Vector3 _lastLookPoint;

        public void Initial(LevelHandler levelHandler, Person person)
        {
            _levelHandler = levelHandler;
            _person = person;
        }

        public void Enable() => 
            gameObject.SetActive(true);

        private void OnEnable() => 
            _levelHandler.LevelLost += OnLostLevel;

        private void OnDisable() => 
            _levelHandler.LevelLost -= OnLostLevel;

        private void OnLostLevel() => 
            SetTarget(_person.Head.transform);

        private void SetTarget(Transform target)
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
}
