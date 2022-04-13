using System;
using UnityEngine.EventSystems;

public interface ILevelPanel : IPointerClickHandler
{
    event Action Clicked;
    void Enable();
    void Disable();
}
