using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StartPhaseUI : LevelPhaseUI
{
    [SerializeField]
    private Button _playButton;

    public void SetActionOnPlayButton(Action onPlayButtonClickAction)
    {
        _playButton.onClick.RemoveAllListeners();
        _playButton.onClick.AddListener(()=>
        {
            onPlayButtonClickAction?.Invoke();
        });
    }
}
