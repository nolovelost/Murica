using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayState
{
    Play,
    Pause,
    Quit,
    _END
}

public enum EAxisCommand
{
    Movement,
    View,
    Aim,
    _END
}

public enum EActionCommand
{
    Run,
    Vault,
    Cover,
    Shoot,
    _END
}

public class CharacterInputData
{
    // PAUSE/PLAY ~~
    public EPlayState playStateCmd { get; set; }


    // MOVEMENT/VIEW/AIM ~~
    Vector2 movementAxis = Vector2.zero;
    Vector2 viewAxis = Vector2.zero;
    Vector2 aimAxis = Vector2.zero;

    public Vector2 GetRawAxisCmd(EAxisCommand axisCmd)
    {
        switch (axisCmd)
        {
            case EAxisCommand.Movement:
                return movementAxis;
            case EAxisCommand.View:
                return viewAxis;
            case EAxisCommand.Aim:
                return aimAxis;
            default:
                return Vector2.zero;
        }
    }

    public void SetRawAxisCmd(EAxisCommand axisCmd, Vector2 value)
    {
        switch (axisCmd)
        {
            case EAxisCommand.Movement:
                movementAxis = value;
                break;
            case EAxisCommand.View:
                viewAxis = value;
                break;
            case EAxisCommand.Aim:
                aimAxis = value;
                break;
            default:
                Debug.LogWarning("Axis Command is invalid.");
                return;
        }
    }


    // ACTIONS ~~
    bool isRunningCmd = false;
    bool isVaultingCmd = false;
    bool isTakingCoverCmd = false;
    bool isShootingCmd = false;

    public bool GetActionCmd(EActionCommand actionCmd)
    {
        switch (actionCmd)
        {
            case EActionCommand.Run:
                return isRunningCmd;
            case EActionCommand.Vault:
                return isVaultingCmd;
            case EActionCommand.Cover:
                return isTakingCoverCmd;
            case EActionCommand.Shoot:
                return isShootingCmd;
            default:
                return false;
        }
    }
    public void SetActionCmd(EActionCommand actionCmd, bool state)
    {
        switch (actionCmd)
        {
            case EActionCommand.Run:
                isRunningCmd = state;
                break;
            case EActionCommand.Vault:
                isVaultingCmd = state;
                break;
            case EActionCommand.Cover:
                isTakingCoverCmd = state;
                break;
            case EActionCommand.Shoot:
                isShootingCmd = state;
                break;
            default:
                return;
        }
    }
}
