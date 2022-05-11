using System;

namespace HumanHole.Scripts.Data
{
    [Serializable]
    public class PointsProgress
    {
        public int Count;
        public event Action Changed;
        
        public void Add(int value)
        {
            if (value < 0)
                throw new ArgumentException("Gold value can not be less then 0");

            Count += value;
            Changed?.Invoke();
        }
    }
}