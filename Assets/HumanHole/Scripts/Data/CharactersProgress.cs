using System;
using System.Collections.Generic;

namespace HumanHole.Scripts.Data
{
    [Serializable]
    public class CharactersProgress
    {
        public List<int> OpenedCharactersIds;
        public int SelectedCharacterId;

        public CharactersProgress()
        {
            OpenedCharactersIds = new List<int> {0};
        }

        public void Open(int id)
        {
            if (OpenedCharactersIds.Contains(id))
                throw new ArgumentException($"CharactersIds contains character with id {id}");

            OpenedCharactersIds.Add(id);
        
        }

        public void Select(int id)
        {
            if (!OpenedCharactersIds.Contains(id))
                throw new ArgumentException($"CharactersIds not contains character with id {id}");
        
            SelectedCharacterId = id;
        }
    }
}