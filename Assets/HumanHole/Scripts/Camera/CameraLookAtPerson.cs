using HumanHole.Scripts.ActiveRagdoll;
using HumanHole.Scripts.Handlers;
using UnityEngine;

namespace HumanHole.Scripts.Camera
{
    public class CameraLookAtPerson
    {
        private const float SmoothTime = 0.2f;
        
        private LevelHandler _levelHandler;
        private Person _person;
        private Transform _cameraTransform;
        private Transform _target;
        private Vector3 _velocity;
        private Vector3 _lastLookPoint;

        public void Initial(LevelHandler levelHandler, Person person, UnityEngine.Camera mainCamera)
        {
            _levelHandler = levelHandler;
            _person = person;
            _cameraTransform = mainCamera.transform;
        }

        public void OnEnabled() => 
            _levelHandler.LevelLost += OnLostLevel;

        private void OnLostLevel()
        {
            _levelHandler.LevelLost -= OnLostLevel;
            SetTarget(_person.Head.transform);
        }

        private void SetTarget(Transform target)
        {
            if (_target == null)
                _lastLookPoint = target.transform.position;

            _target = target;
        }

        public void OnUpdated()
        {
            if (_target == null)
                return;

            _lastLookPoint = Vector3.SmoothDamp(_lastLookPoint, _target.position, ref _velocity, SmoothTime);
            _cameraTransform.rotation = Quaternion.LookRotation(_lastLookPoint - _cameraTransform.position, Vector3.up);
        }
    }
}
