using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    private Vector2 aimDirection;
    private float attackInput;
    private float rotation;
    private Transform weapon;
    
	
	void Awake()
    {
        weapon = transform.GetChild(0);
    }
	
	void Update () {
        HandleInput();
	}

    private void HandleInput()
    {
        aimDirection = new Vector2(Input.GetAxis("Controller-Horizontal"), Input.GetAxis("Controller-Vertical"));
        attackInput = Input.GetAxis("Controller-Attack");
        
        if(attackInput == 1f)
        {
            if (weapon.GetComponent<WallBouncer>() != null)
            {
                weapon.GetComponent<WallBouncer>().Attack(aimDirection);
            }
        }
    }
}
