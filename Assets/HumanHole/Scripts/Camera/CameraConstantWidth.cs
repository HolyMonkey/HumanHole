using UnityEngine;

namespace HumanHole.Scripts.Camera
{
    public class CameraConstantWidth : MonoBehaviour
    {
        [SerializeField] private Vector2 _defaultResolution = new Vector2(1920 , 1080);
        [SerializeField, Range(0f, 1f)] private float _widthOrHeight = 0.985f;
    
        private UnityEngine.Camera _camera;
        private float _initialSize;
        private float _targetAspect;
        private float _initialFieldOfView;
        private float _horizontalFov = 120f;
    
        private void Start()
        {
            _camera = GetComponent<UnityEngine.Camera>();
            _initialSize = _camera.orthographicSize;

            _targetAspect = _defaultResolution.x / _defaultResolution.y;

            _initialFieldOfView = _camera.fieldOfView;
            _horizontalFov = CalculateVerticalFieldOfView(_initialFieldOfView, 1 / _targetAspect);
        }

        private void Update()
        {
            if (_camera.orthographic)
            {
                float constantWidthSize = _initialSize * (_targetAspect / _camera.aspect);
                _camera.orthographicSize = Mathf.Lerp(constantWidthSize, _initialSize, _widthOrHeight);
            }
            else
            {
                float constantWidthFov = CalculateVerticalFieldOfView(_horizontalFov, _camera.aspect);
                _camera.fieldOfView = Mathf.Lerp(constantWidthFov, _initialFieldOfView, _widthOrHeight);
            }
        }

        private float CalculateVerticalFieldOfView(float hFovInDeg, float aspectRatio)
        {
            float horizontalFieldOfViewInRads = hFovInDeg * Mathf.Deg2Rad;
            float verticalFieldOfViewInRads = 2 * Mathf.Atan(Mathf.Tan(horizontalFieldOfViewInRads / 2) / aspectRatio);
            return verticalFieldOfViewInRads * Mathf.Rad2Deg;
        }
    }
}