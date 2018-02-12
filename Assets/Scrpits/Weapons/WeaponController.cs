using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    private Vector2 aimDirection;
    private float attackInput;
    private float rotation;
    private Transform wallBouncerTr;
    private Transform lanceTr;
    private Player player;

    //To be set in unity
    public string weapon;
	
	void Awake()
    {
        wallBouncerTr = transform.GetChild(0);
        lanceTr = transform.GetChild(1);
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
            if (weapon == "WallBouncer")
            {
                wallBouncerTr.GetComponent<WallBouncer>().Attack(aimDirection);
            }
            if(weapon == "Lance")
            {
                lanceTr.GetComponent<Lance>().Attack(aimDirection);
            }
        }
    }
}
