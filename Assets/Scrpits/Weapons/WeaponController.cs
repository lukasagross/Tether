using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    private Vector2 aimDirection;
    private float attackInput;
    private float rotation;
    private Transform weapon;
    private Player player;
    
	
	void Awake()
    {
        weapon = transform.GetChild(0);
        player = GetComponentInParent<Player>();
    }
	
	void Update () {
        HandleInput();
	}

    private void HandleInput()
    {
        aimDirection = new Vector2(Input.GetAxis(player.getHorizontalAxis()), Input.GetAxis(player.getVerticalAxis()));
        attackInput = Input.GetAxis(player.getAttackAxis());
        
        if(attackInput == 1f)
        {
            if (weapon.GetComponent<WallBouncer>() != null)
            {
                weapon.GetComponent<WallBouncer>().Attack(aimDirection);
            }
        }
    }
}
