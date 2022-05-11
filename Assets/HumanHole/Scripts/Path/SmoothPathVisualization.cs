using System.Collections.Generic;
using UnityEngine;

namespace HumanHole.Scripts.Path
{
    public class SmoothPathVisualization : MonoBehaviour
    {
        private const int MinPointLengt = 2;
        
        [SerializeField] private Transform[] _points;
        [SerializeField] private float _smoothPower = 1;

        private SmoothPath _path;
        private float _step = 0.05f;
        private float _radius = 0.1f;

        private void OnDrawGizmos()
        {
            _path = CreatePath();
            if (_path == null)
                return;

            Gizmos.color = Color.red;
            for (float t = 0; t < 1; t += _step)
                Gizmos.DrawSphere(_path.GetPosition(t), _radius);
        }

        private SmoothPath CreatePath()
        {
            if (_points.Length < MinPointLengt)
                return null;

            Transform firstPoint = _points[0];
            Transform lastPoint = _points[_points.Length - 1];
            List<Vector3> pointsBetween = new List<Vector3>();
            for (int i = 1; i < _points.Length - 1; i++)
                pointsBetween.Add(_points[i].position);

            return new SmoothPath(firstPoint.position, lastPoint.position, pointsBetween.ToArray(), firstPoint.transform.forward, lastPoint.transform.forward, _smoothPower);
        }
    }
}

