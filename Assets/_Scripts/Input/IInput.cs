using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EInputDevices
{
    KeyboardMouse,
    DirectInput,
    XInput
}

// Input Controller/Device Information
interface IInput
{
    // PLAYER/AI ~~
    int ownerControllerID { get; set; }
    int GetOwnerControllerID();

    // DEVICES ~~
    EInputDevices inputDevice { get; set; }
    bool IsKeyboardMouse();
    bool IsGamepad();
    bool IsXInput();
    bool IsDirectInput();

    // IN-GAME CHARACTER DATA
    CharacterInputData characterInput { get; set; }
}
