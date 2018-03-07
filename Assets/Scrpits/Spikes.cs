using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    private Score sc;
	
	void Awake()
    {
        sc = FindObjectOfType<Score>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        PlayerHealth plh = col.gameObject.GetComponent<PlayerHealth>();
        PlayerMovement plm = col.gameObject.GetComponent<PlayerMovement>();
        PlayerControls plc = col.gameObject.GetComponent<PlayerControls>();
        if (plh != null)
        {
            if (plh.canDamage())
            {
                plh.takeDamage();
            }
            sc.AddScore(plc.playerNum, -1);
            plm.SetVelocity(new Vector2(col.gameObject.GetComponent<Rigidbody2D>().velocity.x, 8));
        }
    }
}
