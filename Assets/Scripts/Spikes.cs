using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    private Score sc;
    public enum Direction{up, down}
    public Direction d;
	
	void Awake()
    {
        sc = FindObjectOfType<Score>();
    }

    void OnTriggerEnter2D(Collider2D col)
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
            if(d == Direction.up)
            {
                plm.SetVelocity(new Vector2(col.gameObject.GetComponent<Rigidbody2D>().velocity.x, 8));
            }
            else
            {
                plm.SetVelocity(new Vector2(col.gameObject.GetComponent<Rigidbody2D>().velocity.x, -8));
            }
            
        }
    }
}
