using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class Control : MonoBehaviour
{
    [SerializeField] private IKTarget _ikTarget;
    [SerializeField] private float _offset = 0;
    [SerializeField] private LayerMask _rayMask;

    private RectTransform _rectTransform;
    private Image _image;
    private Color _defaultColor;
    private Camera _camera;

    public Rect Rect => new Rect((Vector2) transform.position - (_rectTransform.rect.size * 0.5f), _rectTransform.rect.size);

    public void Select()
    {
        _image.color = new Color(0, 1, 0, 0.9f);
    }

    public void Deselect()
    {
        _image.color = _defaultColor;
    }

    public void Move(Vector2 position)
    {
        transform.position = position;

        Ray ray = _camera.ScreenPointToRay(position);
        if(Physics.Raycast(ray, out RaycastHit hit, 1000, _rayMask))
        {
            _ikTarget.transform.position = hit.point - ray.direction * _offset;
        }
    }

    public void ResetPosition()
    {
        _ikTarget.ResetPosition();
        transform.position = _camera.WorldToScreenPoint(_ikTarget.transform.position);
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        _defaultColor = _image.color;
        _camera = Camera.main;
    }
}
