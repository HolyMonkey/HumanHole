using System;
using System.Collections.Generic;
using UnityEngine;

namespace HumanHole.Scripts
{
    public class DirectionalLight : MonoBehaviour
    {
        [SerializeField] private Vector3 _originPosition;
        [SerializeField] private Vector3 _originRotation;

        private Light _light;

        private void Awake() => 
            _light = GetComponent<Light>();

        public void OnStarted()
        {
            transform.position = _originPosition;
            transform.eulerAngles = _originRotation;
        }

        public void Enable() => 
            _light.enabled = true;

        public void Disable() => 
            _light.enabled = false;
    }
}
