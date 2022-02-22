using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkeetController : MonoBehaviour
{
    private float _timer;

    private List<SkeetTimeProgress> _skeetTimeProgress;

    private bool _isStartTimer;

    private float _multiplyProgress;

    public float MultiplyProgress => _multiplyProgress;

    private Tween _flyAnim;

    private void Update()
    {
        if (!_isStartTimer) return;

        _timer += Time.deltaTime;

        CheckTimeProgress();
    }

    public void SetSettings(SkeetSpawnAndMoveSettings settings, List<SkeetTimeProgress> skeetTimeProgress, float timeFly, Ease typeEase)
    {
        _skeetTimeProgress = skeetTimeProgress;

        var height = Random.Range(settings.MinHeight, settings.MaxHeight);

        transform.position = settings.StartPoint.position;
        _flyAnim = transform.DOJump(settings.EndPoint.position, height, 1, timeFly).SetEase(typeEase).OnComplete(EndMoveSkeet);

        _isStartTimer = true;
    }

    private void CheckTimeProgress()
    {
        var multiplyProgress = 1f / _skeetTimeProgress[0].TimeForFullProgress;
        for(int i = 0; i < _skeetTimeProgress.Count; i++)
        {
            if(_timer > _skeetTimeProgress[i].Time)
            {
                multiplyProgress = 1f / _skeetTimeProgress[i].TimeForFullProgress;
            }
        }
        _multiplyProgress = multiplyProgress;
    }

    private void EndMoveSkeet()
    {
        _isStartTimer = false;
    }

    public void Destroy()
    {
        _flyAnim.Kill();
        Destroy(gameObject);
    }
}
