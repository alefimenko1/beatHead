using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GamePhase : LevelPhase
{
    [Header("Move settings")]

    [SerializeField]
    private Transform _targetMove;

    [SerializeField]
    private float _multiplyDelta;

    [SerializeField]
    private float _speedMove;

    [SerializeField]
    private float _timeMoveToStartPosition;

    [SerializeField]
    private BordersMove _borders;

    [Header("Raycast Check Settings")]

    [SerializeField]
    private LayerMask _layerCheck;

    [SerializeField]
    private float _radiusRaycast;

    private GamePhaseUI _phaseUI => (GamePhaseUI)LevelPhaseUI;

    private Vector3 _startPosition;

    private bool _isActive;

    private float _progress;

    public override void Awake()
    {
        _startPosition = _targetMove.position;
    }

    public override IEnumerator Enter()
    {
        _phaseUI.Reset();
        _progress = 0f;
        yield return base.Enter();
        _isActive = true;
        LevelController.SkeetManager.SpawnSkeet(LevelController.TriggerNextPhase);
    }

    public override IEnumerator Exit()
    {
        _isActive = false;

        yield return MoveToStartPosition();

        yield return base.Exit();
    }

    private void Update()
    {
        if (LevelController.InputManager.IsSwipe && _isActive)
        {
            Move();
        }
    }

    private void FixedUpdate()
    {
        if(LevelController.InputManager.IsSwipe)
        {
            CheckingRaycastTarget();
        }
    }

    private IEnumerator MoveToStartPosition()
    {
        yield return _targetMove.DOMove(_startPosition, _timeMoveToStartPosition);
    }

    private void Move()
    {
        var delta = LevelController.InputManager.Delta;
        var deltaMultiplyed = new Vector3(delta.x, delta.y, 0f) * _multiplyDelta;
        var newPosition = _targetMove.position + deltaMultiplyed;

        var clampedVertical = Mathf.Clamp(newPosition.y, _borders.Bottom, _borders.Top);
        var clampedHorizontal = Mathf.Clamp(newPosition.x, _borders.Left, _borders.Right);

        var clampedNewPosition = new Vector3(clampedHorizontal, clampedVertical, _targetMove.position.z);

        _targetMove.position = Vector3.Lerp(_targetMove.position, clampedNewPosition, Time.deltaTime * _speedMove);
    }

    private void CheckingRaycastTarget()
    {
        Ray ray = new Ray(_targetMove.position, Vector3.forward);
        RaycastHit hit;
        if(Physics.SphereCast(ray, _radiusRaycast, out hit, 50f, _layerCheck))
        {
            var skeetController = hit.collider.gameObject.GetComponent<SkeetController>();
            if(skeetController != null)
            {
                var multiplyProgress = skeetController.MultiplyProgress;
                _progress += Time.deltaTime * multiplyProgress;
                _progress = Mathf.Clamp(_progress, 0f, 1f);
                _phaseUI.SetProgress(_progress);
                if (_progress >= 1f)
                {
                    skeetController.Destroy();
                    LevelController.CountPoints++;
                    LevelController.TriggerNextPhase();
                }
            }
        }
        else
        {
            if (_progress < 1f)
            {
                _progress = 0f;
                _phaseUI.SetProgress(_progress);
            }
        }
    }
}
