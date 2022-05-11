using UnityEngine;
using UnityEngine.Events;

namespace HumanHole.Scripts.HandPointer
{
    public class HandAnimatorEventListener : MonoBehaviour
    {
        public event UnityAction HandPressed;

        public void OnHandPressed()
        {
            HandPressed?.Invoke();
        }
    }
}
