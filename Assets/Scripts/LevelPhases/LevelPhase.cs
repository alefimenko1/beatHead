using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public abstract class LevelPhase : MonoBehaviour
{
    protected LevelController LevelController;

    public LevelPhaseUI LevelPhaseUI;

    public virtual void Awake()
    {
    }

    public virtual void Reset()
    {
        if (LevelPhaseUI != null)
        {
            LevelPhaseUI.DisableUIWithOutAnimation();
        }
    }

    protected virtual void OnLevelControllerSet()
    {
    }

    public virtual IEnumerator Enter()
    {
        if (LevelPhaseUI != null)
        {
            yield return LevelPhaseUI.EnableUI();
        }
        else
        {
            yield return null;
        }
    }

    public virtual IEnumerator Exit()
    {
        if (LevelPhaseUI != null)
        {
            yield return LevelPhaseUI.DisableUI();
        }
        else
        {
            yield return null;
        }
    }

    public void SetLevelController(LevelController levelController)
    {
        LevelController = levelController;
        OnLevelControllerSet();
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
