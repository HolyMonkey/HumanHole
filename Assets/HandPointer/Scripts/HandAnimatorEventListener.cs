using UnityEngine;
using UnityEngine.Events;

public class HandAnimatorEventListener : MonoBehaviour
{
    public event UnityAction HandPressed;

    public void OnHandPressed()
    {
        HandPressed?.Invoke();
    }
}
