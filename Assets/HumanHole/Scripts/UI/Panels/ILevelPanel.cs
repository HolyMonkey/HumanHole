using System;

namespace HumanHole.Scripts.UI.Panels
{
    public interface ILevelPanel
    {
        event Action Opened;
        event Action Closed;
        event Action Clicked;
        void OnEnabled();
        void OnDisabled();
        void OnClicked();
    }
}
