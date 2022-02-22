using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkeetManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _skeetPrefabs = new List<GameObject>();

    [SerializeField]
    private List<SkeetSpawnAndMoveSettings> _settings = new List<SkeetSpawnAndMoveSettings>();

    [SerializeField]
    private List<SkeetTimeProgress> _skeetTimeProgress = new List<SkeetTimeProgress>();

    [SerializeField]
    private float _timeFly;

    [SerializeField]
    private Ease _typeEase;

    private GameObject _skeet;

    public void SpawnSkeet()
    {
        if(_skeet != null)
        {
            Destroy(_skeet);
        }

        var prefab = _skeetPrefabs[Random.Range(0, _skeetPrefabs.Count)];
        var settings = _settings[Random.Range(0, _settings.Count)];

        _skeet = Instantiate(prefab);
        var skeetController = _skeet.GetComponent<SkeetController>();
        skeetController.SetSettings(settings, _skeetTimeProgress, _timeFly, _typeEase);
    }
}
