using UnityEngine;

public class Limb : MonoBehaviour
{
    private Camera _camera;
    private bool _canBreak;
    private bool _isHolding;
    private Vector3 _mouseOffset;
    private float _mouseZCoordinate;
    private bool _allowed;
    
    [SerializeField] private SpriteRenderer _iconSpriteRenderer;
    [SerializeField] private ConfigurableJoint _joint;
    [SerializeField] private Transform _limbAnchor;
    [SerializeField] private LayerMask _rayMask;
    [SerializeField] private Person _person;
    [SerializeField] private float _maxMasScale = 0.2f;
    [SerializeField] private float _cirlceZOffset = 0;
    
    private void Awake()
    {
        _camera = Camera.main;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        _iconSpriteRenderer.enabled = true;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        _iconSpriteRenderer.enabled = false;
        BreakJoint();
    }

    public void AllowMovement()
    {
        _allowed = true;
    }

    public void ForbidMovement()
    {
        _allowed = false;
    }

    private void Update()
    {
        if (_isHolding || !_allowed)
            return;

        var postion = _limbAnchor.position;
        _joint.transform.position = postion;
        _joint.connectedAnchor = _limbAnchor.localPosition;
        _iconSpriteRenderer.color = Color.white;

        postion.z += _cirlceZOffset;
        transform.position = postion;
    }

    private void FixedUpdate()
    {
        if (!_isHolding || !_canBreak || !_allowed)
            return;

        TryToBreakJoint();
    }

    private void TryToBreakJoint()
    {
        float maxForce = _person.Weight * Mathf.Abs(Physics.gravity.y) * _maxMasScale;
        if (_joint.currentForce.magnitude > maxForce)
            BreakJoint();
    }

    private void BreakJoint()
    {
        _isHolding = false;
        _joint.gameObject.SetActive(false);
        _iconSpriteRenderer.color = Color.white;
    }

    private void StartMove()
    {
        _joint.transform.position = _limbAnchor.position;
        _joint.connectedAnchor = _limbAnchor.localPosition;
        _joint.gameObject.SetActive(true);
        _isHolding = true;
    }

    private void OnMouseDown()
    {
        if (!_allowed)
            return;
        
        _mouseZCoordinate = _camera.WorldToScreenPoint(transform.position).z;
        _mouseOffset = transform.position - GetMouseWorldPos();
        Select();
    }

    private void OnMouseUp()
    {
        if (!_allowed)
            return;
        
        Deselect();
    }

    private void OnMouseDrag()
    {
        if (!_allowed)
            return;
        
        var position = GetMouseWorldPos() + _mouseOffset;
        transform.position = position;
        Move();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = _mouseZCoordinate;
        return _camera.ScreenToWorldPoint(mousePoint);
    }

    private void Select()
    {
        _iconSpriteRenderer.color = Color.blue;
        _canBreak = false;
        StartMove();
    }

    private void Deselect()
    {
        _iconSpriteRenderer.color = _isHolding ? Color.green : Color.white;
        _canBreak = true;
    }

    private void Move()
    {
        Vector3 position = Input.mousePosition;
        Ray ray = _camera.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, _rayMask))
        {
            Vector3 hitPoint = hit.point;
            if (hitPoint.y < 0)
                hitPoint.y = 0;

            position = hitPoint;
            /*position.x = Mathf.Clamp(position.x, _minClamp.x, _maxClamp.x);*/
            position.z = _joint.transform.position.z;
            _joint.transform.position = position;
        }
    }
}