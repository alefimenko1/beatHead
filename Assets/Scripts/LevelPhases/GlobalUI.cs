using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalUI : MonoBehaviour
{
    [SerializeField]
    private Text _counterPoints;

    private const string _pointsString = "POINTS: ";

    public void SetPoints(int points)
    {
        _counterPoints.text = _pointsString + points.ToString();
    }
}
