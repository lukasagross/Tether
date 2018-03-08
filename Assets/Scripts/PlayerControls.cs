﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    private string horizontalAxis;
    private string verticalAxis;
    private string attackAxis;
    private string grappleAxis;
    private string jumpAxis;
    private string bAxis;
    public int playerNum;

    void Start()
    {
        horizontalAxis = "Controller-Horizontal" + playerNum;
        verticalAxis = "Controller-Vertical" + playerNum;
        attackAxis = "Controller-Attack" + playerNum;
        grappleAxis = "Controller-Grapple" + playerNum;
        jumpAxis = "Controller-Jump" + playerNum;
        bAxis = "Controller-B" + playerNum;
    }

    public string getHorizontalAxis()
    {
        return horizontalAxis;
    }

    public string getVerticalAxis()
    {
        return verticalAxis;
    }

    public string getAttackAxis()
    {
        return attackAxis;
    }

    public string getGrappleAxis()
    {
        return grappleAxis;
    }

    public string getJumpAxis()
    {
        return jumpAxis;
    }
    public string getBAxis()
    {
        return bAxis;
    }
}
