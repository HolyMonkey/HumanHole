using HumanHole.Scripts.ActiveRagdoll;
using HumanHole.Scripts.Data;
using UnityEngine;

namespace HumanHole.Scripts.Player
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] private Characters _characters;

        private Person _person;
        private CharactersProgress _charactersProgress;

        public void Initial(CharactersProgress charactersProgress, Person person)
        {
            _charactersProgress = charactersProgress;
            _person = person;
        }

        public void Spawn()
        {
            Сharacter characterTemplate = _characters.GetCharacterById(_charactersProgress.SelectedCharacterId);
            Сharacter character = Instantiate(characterTemplate, transform);
            character.SetPerson(_person);
        }
    }
}
