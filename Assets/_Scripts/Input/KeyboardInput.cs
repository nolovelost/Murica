using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EKeyboardMouse
{
    WASD,
    IJKL,
    Mouse
}

public class KeyboardInput : MonoBehaviour, IInput
{
    KeyboardInput()
    {
        inputDevice = EInputDevices.KeyboardMouse;
        characterInput = new CharacterInputData();
        keyMapper = new KeyboardMapper();
    }

    // PLAYER/AI ~~
    public int ownerControllerID { get; set; }
    public int GetOwnerControllerID() { return ownerControllerID; }


    // DEVICE ~~
    public EInputDevices inputDevice { get; set; }

    public bool IsKeyboardMouse() { return true; }
    public bool IsGamepad() { return false; }
    public bool IsXInput() { return false; }
    public bool IsDirectInput() { return false; }


    // CHARACTER INPUT DATA ~~
    public CharacterInputData characterInput { get; set; }


    // Key Setter
    KeyboardMapper keyMapper { get; set; }


    // Vector Translator
    Vector2 GetRawDirection(KeyCode[] keys)
    {
        Vector2 direction = new Vector2(0.0f, 0.0f);
        foreach (KeyCode key in keys)
        {
            if (key == KeyCode.W || key == KeyCode.I)
                direction.y = 1.0f;
            if (key == KeyCode.S || key == KeyCode.K)
                direction.y = -1.0f;
            if (key == KeyCode.A || key == KeyCode.J)
                direction.x = -1.0f;
            if (key == KeyCode.D || key == KeyCode.L)
                direction.x = 1.0f;
        }
        return direction;
    }
    Vector2 GetNormalisedDirection(KeyCode[] keys)
    {
        Vector2 direction = new Vector2(0.0f, 0.0f);
        foreach (KeyCode key in keys)
        {
            if (key == KeyCode.W || key == KeyCode.I)
                direction.y = 1.0f;
            if (key == KeyCode.S || key == KeyCode.K)
                direction.y = -1.0f;
            if (key == KeyCode.A || key == KeyCode.J)
                direction.x = -1.0f;
            if (key == KeyCode.D || key == KeyCode.L)
                direction.x = 1.0f;
        }
        return direction.normalized;
    }

    // Set Character Data
    void Update()
    {
        //characterInput.SetRawAxisCmd(EAxisCommand.Movement, GetNormalisedDirection())
    }
}
