using System;
using UnityEngine.EventSystems;

namespace HumanHole.Scripts.UI
{
    public interface ILevelPanel : IPointerClickHandler
    {
        event Action Clicked;
        void Enable();
        void Disable();
    }
}
