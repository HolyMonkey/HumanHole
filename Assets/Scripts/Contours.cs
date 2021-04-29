using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contours : MonoBehaviour
{
    [SerializeField] private HoleContour[] _contours;

    public void ShowContour(int index)
    {
        _contours[index].Appear();
    }

    public void HideContours()
    {
        foreach (var item in _contours)
            item.gameObject.SetActive(false);
    }
}
