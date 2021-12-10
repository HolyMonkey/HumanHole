using UnityEngine.EventSystems;

public class WebEventSystem : EventSystem
{
    protected override void OnApplicationFocus(bool hasFocus) => base.OnApplicationFocus(true);
}