using UnityEngine;

namespace HumanHole.Scripts.Path
{
    public class BezierCurve
    {
        private readonly Vector3 _point0;
        private readonly Vector3 _point1;
        private readonly Vector3 _point2;
        private readonly Vector3 _point3;
        private float _offset;

        public float Length { get;}

        public BezierCurve(Vector3 point0, Vector3 point1, Vector3 point2, Vector3 point3)
        {
            _point0 = point0;
            _point1 = point1;
            _point2 = point2;
            _point3 = point3;

            Length = GetLength();
        }

        public Vector3 GetPoint(float t) => 
            Mathf.Pow(1 - t, 3) * _point0 + 3 * Mathf.Pow(1 - t, 2) * t * _point1 + 3 * (1 - t) * Mathf.Pow(t, 2) * _point2 + Mathf.Pow(t, 3) * _point3;


        private float GetLength()
        {
            float length = 0;
            Vector3 lastPoint = _point0;
            _offset = 0.02f;
            for (float t = 0; t < 1; t += _offset)
            {
                Vector3 point = GetPoint(t);
                length += Vector3.Distance(point, lastPoint);
                lastPoint = point;
            }
            return length;
        }
    }
}
