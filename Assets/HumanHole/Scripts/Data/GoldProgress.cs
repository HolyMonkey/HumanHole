using System;

namespace HumanHole.Scripts.Data
{
    [Serializable]
    public class GoldProgress
    {
        public int Count;
        public event Action Changed;
        
        public void Set(int value)
        {
            if (value < 0)
                throw new ArgumentException("Gold value can not be less than 0");

            Count = value;
            Changed?.Invoke();
        }
        
        public void Add(int value)
        {
            if (value < 0)
                throw new ArgumentException("Gold value can not be less than 0");

            Count += value;
            Changed?.Invoke();
        }
        
        public void Remove(int value)
        {
            if (value > Count)
                throw new ArgumentException("Removed gold value can not be more than current gold value");

            Count -= value;
            Changed?.Invoke();
        }
    }
}