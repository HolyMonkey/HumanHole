using System;
using HumanHole.Scripts.ActiveRagdoll;
using UnityEngine;

namespace HumanHole.Scripts
{
    public class Wall : MonoBehaviour
    {
        [SerializeField] private Sprite _contour;
        [SerializeField] private GameObject _middleCollider;
    
        private float _speed = 2;
        private WallCollider[] _colliders;
        private Person _person;
        private int _index;
        private bool _touchedPerson;
        private bool _allowed;
        private MeshRenderer _meshRenderer;
    
        public Sprite Contour => _contour;
        public event Action TouchedPlayer;
        public event Action LeftPlayerZone;
        public event Action Destroyed;

        public void Initialize(float speed, Color color)
        {
            _speed = speed;
            AllowMovement();
            _meshRenderer.sharedMaterial.color = color;
        }

        private void Awake()
        {
            _colliders = GetComponentsInChildren<WallCollider>();
            _meshRenderer = GetComponentInChildren<MeshRenderer>();
        }

        private void OnEnable()
        {
            foreach (var item in _colliders)
                item.TouchedPerson += OnTouchedPerson;
        }

        private void OnDisable()
        {
            foreach (var item in _colliders)
                item.TouchedPerson -= OnTouchedPerson;
        }

        public void AllowMovement()
        {
            _allowed = true;
        }

        public void StopMovement()
        {
            _allowed = false;
        }

        private void Update()
        {
            if (_allowed)
                transform.position += Vector3.back * _speed * Time.deltaTime;
        }

        private void OnTouchedPerson()
        {
            if (!_touchedPerson)
            {
                _middleCollider.SetActive(true);
                _touchedPerson = true;
                TouchedPlayer?.Invoke();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out LeftPlayerZone leftPlayerZone))
            {
                LeftPlayerZone?.Invoke();
            }
            else if (other.TryGetComponent(out DestroyWallZone destroyWallZone))
            {
                Destroyed?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}