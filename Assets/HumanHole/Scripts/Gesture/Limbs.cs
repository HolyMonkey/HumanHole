using System.Collections.Generic;
using UnityEngine;

namespace HumanHole.Scripts.Gesture
{
    public class Limbs : MonoBehaviour
    {
        [SerializeField] private Limb[] _limbs;
    
        public IReadOnlyCollection<Limb> LimbsCollection => _limbs;
    }
}
