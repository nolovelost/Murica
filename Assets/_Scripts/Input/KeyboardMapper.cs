using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyboardMapper
{
    public Dictionary<EAxisCommand, EKeyboardMouse> axisKeys = new Dictionary<EAxisCommand, EKeyboardMouse>((int)EAxisCommand._END);
    public Dictionary<EActionCommand, KeyCode> actionKeys = new Dictionary<EActionCommand, KeyCode>((int)EActionCommand._END);
    public Dictionary<EPlayState, KeyCode> playStateKeys = new Dictionary<EPlayState, KeyCode>((int)EPlayState._END);

    public KeyboardMapper()
    {
        for (int i = 0; i < (int)EAxisCommand._END; i++)
        {
            axisKeys.Add(EAxisCommand.Movement, EKeyboardMouse.WASD);
            axisKeys.Add(EAxisCommand.View, EKeyboardMouse.Mouse);
            axisKeys.Add(EAxisCommand.Aim, EKeyboardMouse.IJKL);
        }
        for (int i = 0; i < (int)EActionCommand._END; i++)
        {
            actionKeys.Add(EActionCommand.Run, KeyCode.LeftShift);
            actionKeys.Add(EActionCommand.Cover, KeyCode.E);
            actionKeys.Add(EActionCommand.Shoot, KeyCode.Slash);
            actionKeys.Add(EActionCommand.Vault, KeyCode.Space);
        }
        for (int i = 0; i < (int)EPlayState._END; i++)
        {
            playStateKeys.Add(EPlayState.Play, KeyCode.Escape);
            playStateKeys.Add(EPlayState.Pause, KeyCode.Escape);
            playStateKeys.Add(EPlayState.Quit, KeyCode.Backspace);
        }
    }
}
