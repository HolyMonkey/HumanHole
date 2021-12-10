using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private WallSpawner _wallSpawner;
    [SerializeField] private NewGestureHandler _gestureHandler;
    [SerializeField] private LookAtPerson _cameraRotation;
    [SerializeField] private Transform _mainCameraLookPoint;
    [SerializeField] private MoveAlongPath _cameraFailMovement;
    [SerializeField] private GameObject _balanceSlider;
    [SerializeField] private Contours contours;
    [SerializeField] private Slider _progressSlider;

    private Wall _wall;

    private void Start()
    {
        _cameraRotation.SetTarget(_mainCameraLookPoint);
        _progressSlider.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (Input.GetKeyDown(KeyCode.Alpha1))
            SpawnWall(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            SpawnWall(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            SpawnWall(2);

        if (_wall != null)
            _progressSlider.value = 1 - _wall.transform.position.z / _wallSpawner.transform.position.z;
    }

    private void SpawnWall(int index)
    {
      // _contours.ShowContour(index);
       // _wall = _wallSpawner.SpawnWall(index);
        _wall.TouchedPerson += OnWallHitPerson;
        _progressSlider.gameObject.SetActive(true);
    }

    private void OnWallHitPerson()
    {
        /*_contours.HideContours();
        person.SetStabilization(false);
        _gestureHandler.enabled = false;
        _cameraRotation.SetTarget(person.Head.transform);
        _cameraFailMovement.MoveToEnd(2);
        _balanceSlider.gameObject.SetActive(false);
        _progressSlider.gameObject.SetActive(false);*/
    }
}
