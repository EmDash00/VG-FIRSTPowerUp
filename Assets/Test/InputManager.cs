using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    public static float getHorizontalInput()
    {
        return Mathf.Clamp(Input.GetAxisRaw("Horizontal") + Input.GetAxisRaw("HorizontalJoystick"), -1, 1);
    }

    public static bool getJumpPressed()
    {
        return Input.GetButtonDown("JumpJoystick");
    }

    public static bool getJumpHeld()
    {
        return Input.GetButton("JumpJoystick");
    }

    public static bool getPausePressed()
    {
        return Input.GetButtonDown("Pause");
    }




}
