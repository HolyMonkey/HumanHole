using UnityEngine;

public class PlatformColorChanger : MonoBehaviour
{
    [SerializeField] private Color _color;

    private void Awake() => 
        GetComponent<MeshRenderer>().sharedMaterial.color = _color;
}
