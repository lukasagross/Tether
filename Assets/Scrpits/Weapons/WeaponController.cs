﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    private Vector2 aimDirection;
    private float attackInput;
    private float rotation;
    private Transform wallBouncerTr;
    private Transform lanceTr;
    private Transform scytheTr;
    private PlayerControls player;

    //To be set in unity
    public Weapon.WeaponType type;
	
	void Awake()
    {
        wallBouncerTr = transform.GetChild(0);
        lanceTr = transform.GetChild(1);
        scytheTr = transform.GetChild(2);
        player = GetComponentInParent<PlayerControls>();
    }
	
	void Update () {
        HandleInput();
        //if (Input.GetKeyDown(KeyCode))
	}

    private void HandleInput()
    {
        aimDirection = new Vector2(Input.GetAxis(player.getHorizontalAxis()), Input.GetAxis(player.getVerticalAxis()));
        attackInput = Input.GetAxis(player.getAttackAxis());
        
        if(attackInput == 1f)
        {
            if (type == Weapon.WeaponType.Wallbouncer)
            {
                wallBouncerTr.GetComponent<WallBouncer>().Attack(aimDirection);
            }
            if(type == Weapon.WeaponType.Lance)
            {
                lanceTr.GetComponent<Lance>().Attack(aimDirection);
            }
            if(type == Weapon.WeaponType.Scythe)
            {
                scytheTr.GetComponent<Scythe>().Attack(aimDirection);
            }
        }
    }
}
