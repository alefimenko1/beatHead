using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private List<LevelPhase> _phases = new List<LevelPhase>();

    [SerializeField]
    private InputManager _inputManager;

    [SerializeField]
    private SkeetManager _skeetManager;

    private int _currentPhaseIndex = -1;

    private LevelPhase _currentPhase;

    public InputManager InputManager => _inputManager;

    public SkeetManager SkeetManager => _skeetManager;

    public int CountPoints
    {
        get
        {
            return PlayerPrefs.GetInt("points", 0);
        }
        set
        {
            PlayerPrefs.SetInt("points", value);
            PlayerPrefs.Save();
        }
    }

    private void Awake()
    {
        foreach(LevelPhase phase in _phases)
        {
            phase.SetLevelController(this);
            phase.Reset();
            phase.Disable();
        }
    }

    private void Start()
    {
        TriggerNextPhase();
    }

    public void TriggerNextPhase()
    {
        _currentPhaseIndex++;
        if(_currentPhaseIndex >= _phases.Count)
        {
            _currentPhaseIndex = 0;
        }

        StartCoroutine(TriggerPhaseCoroutine());
    }

    private IEnumerator TriggerPhaseCoroutine()
    {
        if(_currentPhase != null)
        {
            yield return _currentPhase.Exit();
            _currentPhase.Disable();
        }

        _currentPhase = _phases[_currentPhaseIndex];

        _currentPhase.Enable();
        yield return _currentPhase.Enter();
    }
}
