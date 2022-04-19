using System;
using System.Linq;
using UnityEngine;

namespace HumanHole.Scripts.Player
{
    [CreateAssetMenu(fileName = "Characters")]
    public class Characters : ScriptableObject
    {
        [SerializeField] private Сharacter[] _charactersTemplate;

        public Сharacter GetCharacterById(int id)
        {
            Сharacter characterTemplate = _charactersTemplate.FirstOrDefault(x => x.Id == id);
            if (characterTemplate == null)
                throw new NullReferenceException($"Character with {id} is null");
            return characterTemplate;
        }
    }
} 
