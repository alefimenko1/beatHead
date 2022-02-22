using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPhase : LevelPhase
{
    private StartPhaseUI _phaseUI => (StartPhaseUI)LevelPhaseUI;

    public override void Reset()
    {
        base.Reset();
        _phaseUI.SetActionOnPlayButton(OnPlayButtonClicked);
    }

    public override IEnumerator Enter()
    {
        return base.Enter();
    }

    public override IEnumerator Exit()
    {
        return base.Exit();
    }

    private void OnPlayButtonClicked()
    {
        LevelController.TriggerNextPhase();
    }

}
