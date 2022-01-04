using UnityEngine;
using UnityEngine.UI;

public class LimbControl : MonoBehaviour
{
    [SerializeField] private RectTransform _icon;
    [SerializeField] private Image _iconImage;
    [SerializeField] private ConfigurableJoint _joint;
    [SerializeField] private Transform _limbAnchor;
    [SerializeField] private LayerMask _rayMask;
    [SerializeField] private Person _person;
    [SerializeField] private float _maxMasScale = 0.2f;
    [SerializeField] private float _offset = 0;

    private Camera _camera;
    private bool _canBreak = true;
    private bool _isHolding = false;
    private Vector3 _mouseOffset;
    private float _mouseZCoordinate;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
      //  _icon.gameObject.SetActive(true);
      transform.position = new Vector3(_limbAnchor.position.x, _limbAnchor.position.y, transform.position.z);
    }

    private void OnDisable()
    {
      //  _icon.gameObject.SetActive(false);
        BreakJoint();
    }

    private void Update()
    {
        if (_isHolding)
            return;

        _joint.transform.position = _limbAnchor.position;
        _joint.connectedAnchor = _limbAnchor.localPosition;
        _iconImage.color = Color.white;

        /*Vector3 middlePosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        var worldToScreenPoint = Camera.main.WorldToScreenPoint(_joint.transform.position);
        _icon.localPosition = worldToScreenPoint - middlePosition;*/
    }

    private void FixedUpdate()
    {
        if (!_isHolding || !_canBreak)
            return;

        float maxForce = _person.Weight * Mathf.Abs(Physics.gravity.y) * _maxMasScale;
        if (_joint.currentForce.magnitude > maxForce)
            BreakJoint();
    }

    public Rect Rect =>
        new Rect(
            (Vector2) _icon.transform.localPosition + new Vector2(Screen.width / 2, Screen.height / 2) -
            (_icon.rect.size * 0.5f), _icon.rect.size);

    public void Select()
    {
        _iconImage.color = Color.blue;
        _canBreak = false;
        StartMove();
    }

    public void Deselect()
    {
        _iconImage.color = _isHolding ? Color.green : Color.white;
        _canBreak = true;
    }

    public void Move()
    {
        Vector2 position = Input.mousePosition;
        Ray ray = _camera.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, _rayMask))
        {
            Vector3 hitPoint = hit.point;
            if (hitPoint.y < 0)
                hitPoint.y = 0;

            _joint.transform.position = hitPoint - _offset * ray.direction;
        }
    }
    
    public void Move(Vector2 position)
    {
        return;
        _icon.transform.localPosition = position - new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = _camera.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, _rayMask))
        {
            Vector3 hitPoint = hit.point;
            if (hitPoint.y < 0)
                hitPoint.y = 0;

            _joint.transform.position = hitPoint - _offset * ray.direction;
        }
    }

    private void StartMove()
    {
        _joint.transform.position = _limbAnchor.position;
        _joint.connectedAnchor = _limbAnchor.localPosition;
        _joint.gameObject.SetActive(true);
        _isHolding = true;
    }

    public void BreakJoint()
    {
        _isHolding = false;
        _joint.gameObject.SetActive(false);
        _iconImage.color = Color.white;
    }
    
    private void OnMouseDown()
    {
        _mouseZCoordinate = _camera.WorldToScreenPoint(transform.position).z;
        _mouseOffset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + _mouseOffset;
        Move();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = _mouseZCoordinate;
        return _camera.ScreenToWorldPoint(mousePoint);
    }
}
