using HumanHole.Scripts.ActiveRagdoll;
using HumanHole.Scripts.Data;
using HumanHole.Scripts.Gesture;
using HumanHole.Scripts.Shop;
using HumanHole.Scripts.UI.Panels;
using UnityEngine;

namespace HumanHole.Scripts.Player
{
    public class CharactersSpawner : MonoBehaviour
    {
        [SerializeField] private CharacterSpawner _characterSpawner;
        [SerializeField] private ShopCharactersSpawner _shopCharactersSpawner;
        [SerializeField] private Person _person;
        [SerializeField] private Limbs _limbs;
        [SerializeField] private Vector3 _originPosition;

        public CharacterSpawner CharacterSpawner => _characterSpawner;
        public ShopCharactersSpawner ShopCharactersSpawner => _shopCharactersSpawner;
        public Person Person => _person;
        public Limbs Limbs => _limbs;

        public void Initial(CharactersProgress charactersProgress, CollisionObserver collisionObserver, LevelPanelsStateMachine levelPanelsStateMachine)
        {
            _characterSpawner.Initial(charactersProgress, _person);
            _person.Initial(collisionObserver);
            _shopCharactersSpawner.Initial(_person, levelPanelsStateMachine);
        }

        public void OnEnabled()
        {
            _person.OnEnabled();
            _shopCharactersSpawner.OnEnabled();
        }

        public void OnDisabled()
        {
            _person.OnDisabled();
            _shopCharactersSpawner.OnDisabled();
        }

        public void OnStarted()
        {
            transform.position = _originPosition;
            _person.OnStarted();
        }
    }
}
