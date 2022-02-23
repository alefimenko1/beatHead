using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public abstract class LevelPhaseUI : MonoBehaviour
{
    [SerializeField]
    private float _timeAnimate = 0.5f;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

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
        if(_canvasGroup == null)
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0f;
    }
}
