using HumanHole.Scripts.ActiveRagdoll;
using UnityEngine;

namespace HumanHole.Scripts.Player
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] private Characters _characters;

        private Person _person;

        public void Initial(int id)
        {
            _person = GetComponentInChildren<Person>();
            Spawn(id);
        }

        private void Spawn(int id)
        {
            Сharacter characterTemplate = _characters.GetCharacterById(id);
            Сharacter character = Instantiate(characterTemplate, transform);
            character.SetPerson(_person);
        }
    }
}
