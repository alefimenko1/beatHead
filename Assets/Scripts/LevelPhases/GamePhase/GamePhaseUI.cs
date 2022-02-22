using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePhaseUI : LevelPhaseUI
{
    [SerializeField]
    private Image _progressImage;

    public override void Reset()
    {
        _progressImage.fillAmount = 0f;
    }

    public void SetProgress(float progress)
    {
        _progressImage.fillAmount = progress;
    }
}
