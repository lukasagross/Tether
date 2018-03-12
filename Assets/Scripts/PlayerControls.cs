using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerControls : MonoBehaviour
{
    public int playerNum;
    public XboxController controller;


    public float getHorizontalAxis()
    {
        return XCI.GetAxis(XboxAxis.LeftStickX, controller);
    }

    public float getVerticalAxis()
    {
        return XCI.GetAxis(XboxAxis.LeftStickY, controller);
    }

    public float getAttackAxis()
    {
        return XCI.GetAxis(XboxAxis.RightTrigger, controller);
    }

    public float getGrappleAxis()
    {
        return XCI.GetAxis(XboxAxis.LeftTrigger, controller);
    }

    public bool getJumpAxis()
    {
        return XCI.GetButton(XboxButton.A, controller);
    }
    public bool getBAxis()
    {
        return XCI.GetButton(XboxButton.B, controller);
    }

    public bool getReset()
    {
        return XCI.GetButton(XboxButton.Start) && XCI.GetButton(XboxButton.Back);
    }
}
