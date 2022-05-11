using System;

namespace HumanHole.Scripts.Data
{
    [Serializable]
    public class ShopProgress
    {
        public int OpenCharacterPrice;

        public event Action Changed;
        
        public ShopProgress() => 
            OpenCharacterPrice = 3;

        public void UpdatePrice()
        {
            OpenCharacterPrice += 3;
            Changed?.Invoke();
        }
    }
}