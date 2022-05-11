using System;
using System.Linq;
using UnityEngine;

namespace HumanHole.Scripts.Player
{
    [CreateAssetMenu(fileName = "CharactersStaticData", menuName = "StaticData/CharactersStaticData")]
    public class Characters : ScriptableObject
    {
        [SerializeField] private Сharacter[] _characters;

        public Сharacter GetCharacterById(int id)
        {
            Сharacter characterTemplate = _characters.FirstOrDefault(x => x.Id == id);
            if (characterTemplate == null)
                throw new NullReferenceException($"Character with {id} is null");
            return characterTemplate;
        }
        
        public Сharacter[] GetCharacters() => 
            _characters;
    }
} 
