using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour {
    private Vector2 aimDirection;
    private Player player;
    private float rotation;

	void Awake () {
        player = transform.parent.parent.GetComponent<Player>();
        aimDirection = Vector2.up;
	}
	
	void Update () {
        aimDirection = new Vector2(Input.GetAxis(player.getHorizontalAxis()), Input.GetAxis(player.getVerticalAxis()));
        rotation = Vector2.Angle(aimDirection, Vector2.up);
        Vector3 tempCross = Vector3.Cross(aimDirection, Vector2.up);
        if (tempCross.z > 0)
        {
            rotation = 360 - rotation;
        }
        transform.parent.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
