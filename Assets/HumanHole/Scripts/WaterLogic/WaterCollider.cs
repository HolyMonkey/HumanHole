using System;
using HumanHole.Scripts.ActiveRagdoll;
using HumanHole.Scripts.Infrastructure.Services.Factory;
using HumanHole.Scripts.Level;
using UnityEngine;

namespace HumanHole.Scripts.WaterLogic
{
    public class WaterCollider : MonoBehaviour
    {
        [SerializeField] private BodyTarget bodyTarget;
        [SerializeField] private float _jointY = 0.5f;
        [SerializeField] private ParticleSystem _splashEffectPrefab;
        [SerializeField] private ParticleSystem _tinySplashEffectPrefab;

        public event Action CollidedPlayer;
        
        private bool _isCollided;
        private IFactoryService _factoryService;

        public void Initial(IFactoryService factoryService) => 
            _factoryService = factoryService;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BodyPart part))
            {
                Vector3 position = new Vector3(part.transform.position.x, 0, part.transform.position.z);
                if (part is Spine)
                {
                    bodyTarget.SetPosition(position + Vector3.up * _jointY);
                    bodyTarget.Enable();
                    _factoryService.Create(_splashEffectPrefab, position, Quaternion.identity);
                }
                else
                {
                    _factoryService.Create(_tinySplashEffectPrefab, position, Quaternion.identity);
                }

                if (!_isCollided)
                {
                    _isCollided = true;
                    CollidedPlayer?.Invoke();
                }
            }

            if (other.TryGetComponent(out Hand hand))
            {
                hand.PrepareForSwim();
                if (!_isCollided)
                {
                    _isCollided = true;
                    CollidedPlayer?.Invoke();
                }
            }
        }
    }
}