using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using Zenject;

public class HandPointer : MonoBehaviour
{
    [SerializeField] private float _distance = 10;
    [SerializeField] private Animator _animator;
    [SerializeField] private Camera _camera;
    [SerializeField] private HandAnimatorEventListener _animationEvents;
    [SerializeField] private GameObject _hand;

    private MousePositionConverter _positionConverter;
    private float _lastX;
    private float _lastRight = 0;
    private int _anger = 1;
    private int _downAnger;

    public event UnityAction<Vector2> MouseDown;
    public event UnityAction<Vector2> MouseUp;
    public bool IsPressing { get; private set; } = false;

    private GameRunner _gameRunner;
    
    [Inject]
    private void Contruct(GameRunner gameRunner)
    {
        _gameRunner = gameRunner;
    }

    private void Awake()
    {
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        _animationEvents.HandPressed += HandlePress;
        _gameRunner.GameStarted += GameStarted;
    }

    private void OnDisable()
    {
        _animationEvents.HandPressed -= HandlePress;
        _gameRunner.GameStarted -= GameStarted;
    }

    private void Start()
    {
        Cursor.visible = false;
        if (_camera == null)
            _camera = Camera.main;

        if (_camera.orthographic)
            _positionConverter = new OrthographicCameraMousePositionConverter(_camera);
        else
            _positionConverter = new PerspectiveCameraMousePositionConverter(_camera);
    }

    private void Update()
    {
        if(!_gameRunner.IsGameStarted)
            return;
        
        HandleMousePosition(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.visible = false;
            string animation = HandAnimations.MouseDown;
            switch (_anger)
            {
                case 2:
                    animation = HandAnimations.MouseDownAngry;
                    break;
                case 3:
                    animation = HandAnimations.MouseDownHit;
                    break;
                default:
                    break;
            }
            _downAnger = _anger;
            _animator.SetTrigger(animation);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            string animation = HandAnimations.MouseUp;
            switch (_downAnger)
            {
                case 2:
                    animation = HandAnimations.MouseUpAngry;
                    break;
                case 3:
                    animation = HandAnimations.MouseUpHit;
                    break;
                default:
                    break;
            }
            _animator.SetTrigger(animation);
            MouseUp?.Invoke(Input.mousePosition);
        }

        IsPressing = Input.GetMouseButton(0);

        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //    Play(HandAnimations.Angry);
        //else if (Input.GetKeyDown(KeyCode.Alpha2))
        //    Play(HandAnimations.Ok);
        //else if (Input.GetKeyDown(KeyCode.Alpha3))
        //    Play(HandAnimations.ThumbUp);

        if (Input.GetKeyDown(KeyCode.UpArrow))
            AddAngry();
    }

    public void Play(string animation) => _animator.SetTrigger(animation);

    public void ResetAngry() => _anger = 1;

    public void AddAngry() => _anger = _anger == 3 ? 1 : _anger + 1;

    private void GameStarted()
    {
        _hand.SetActive(true);
    }

    private void HandlePress()
    {
        MouseDown?.Invoke(Input.mousePosition);
    }

    private void HandleMousePosition(Vector2 mousePosition)
    {
        transform.position = _positionConverter.GetCursorPosition(mousePosition, _distance);
        transform.rotation = Quaternion.LookRotation(_camera.transform.forward, Vector3.up);

        float speed = transform.position.x - _lastX;
        float right = 0.5f + speed * 100;

        if (right - _lastRight > 0.02f)
            right = _lastRight + 0.02f;
        else if (_lastRight - right > 0.02f)
            right = _lastRight - 0.02f;

        _animator.SetFloat("right", right);
        _lastX = transform.position.x;
        _lastRight = right;
    }
}
