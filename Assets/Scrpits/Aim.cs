using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour {
    private Vector2 aimDirection;
    private PlayerControls player;
    private float rotation;
    private SpriteRenderer sr;

	void Awake () {
        player = transform.parent.parent.GetComponent<PlayerControls>();
        aimDirection = Vector2.up;
        sr = GetComponent<SpriteRenderer>();
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

    public void ChangeColor(float r, float g, float b, float a)
    {
        sr.color = new Color(r, g, b, a);
    }
}
