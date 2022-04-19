using System.Collections.Generic;
using HumanHole.Scripts.Gesture;
using UnityEngine;

public class Limbs : MonoBehaviour
{
    [SerializeField] private Limb[] _limbs;
    
    public IReadOnlyCollection<Limb> LimbsCollection => _limbs;

}
