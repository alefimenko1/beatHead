using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public abstract class LevelPhaseUI : MonoBehaviour
{
    [SerializeField]
    private float _timeAnimate = 0.5f;

    [SerializeField]
    private CanvasGroup _canvasGroup;

    public virtual void Reset()
    {
    }

    public IEnumerator EnableUI()
    {
        yield return _canvasGroup.DOFade(1f, _timeAnimate).OnComplete(()=>
        {
            _canvasGroup.blocksRaycasts = true;
        });
    }

    public IEnumerator DisableUI()
    {
        _canvasGroup.blocksRaycasts = false;
        yield return _canvasGroup.DOFade(0f, _timeAnimate);
    }

    public void DisableUIWithOutAnimation()
    {
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0f;
    }
}
