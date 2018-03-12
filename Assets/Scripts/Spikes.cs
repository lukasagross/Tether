using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    private Score sc;
    private GameMode gm;
    public enum Direction{up, down, left, right}
    public Direction d;
	
	void Awake()
    {
        sc = FindObjectOfType<Score>();
        gm = FindObjectOfType<GameMode>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerHealth plh = col.GetComponent<PlayerHealth>();
        PlayerMovement plm = col.GetComponent<PlayerMovement>();
        PlayerControls plc = col.GetComponent<PlayerControls>();
        if (plh != null)
        {
            if (plh.canDamage())
            {
                plh.takeDamage();
            }

            if (gm.currentMode == GameMode.Mode.health && plh.canDamage())
            {
                sc.AddScore(plc.playerNum, -1);
            }
            if (gm.currentMode == GameMode.Mode.carrots || gm.currentMode == GameMode.Mode.hits)
            {
                sc.AddScore(plc.playerNum, -1);
            }
            
            if (d == Direction.up)
            {
                plm.SetVelocity(new Vector2(col.gameObject.GetComponent<Rigidbody2D>().velocity.x, 8));
            }
            else if (d == Direction.down)
            {
                plm.SetVelocity(new Vector2(col.gameObject.GetComponent<Rigidbody2D>().velocity.x, -8));
            }
            else if (d == Direction.left)
            {
                plm.SetVelocity(new Vector2(8, col.gameObject.GetComponent<Rigidbody2D>().velocity.y));
            }
            else
            {
                plm.SetVelocity(new Vector2(-8, col.gameObject.GetComponent<Rigidbody2D>().velocity.y));
            }
        }
        else {

            Carrot c = col.GetComponent<Carrot>();
        if (c != null)
            {
                if (d == Direction.up)
                {
                    c.SetVelocity(new Vector2(col.gameObject.GetComponent<Rigidbody2D>().velocity.x, 5));
                }
                else if (d == Direction.down)
                {
                    c.SetVelocity(new Vector2(col.gameObject.GetComponent<Rigidbody2D>().velocity.x, -5));
                }
                else if (d == Direction.left)
                {
                    c.SetVelocity(new Vector2(5, col.gameObject.GetComponent<Rigidbody2D>().velocity.y));
                }
                else
                {
                    c.SetVelocity(new Vector2(-5, col.gameObject.GetComponent<Rigidbody2D>().velocity.y));
                }
            }
        }
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

            if (gm.currentMode == GameMode.Mode.health && plh.canDamage())
            {
                sc.AddScore(plc.playerNum, -1);
            }
            if (gm.currentMode == GameMode.Mode.carrots || gm.currentMode == GameMode.Mode.hits)
            {
                sc.AddScore(plc.playerNum, -1);
            }

            if (d == Direction.up)
            {
                plm.SetVelocity(new Vector2(col.gameObject.GetComponent<Rigidbody2D>().velocity.x, 8));
            }
            else if (d == Direction.down)
            {
                plm.SetVelocity(new Vector2(col.gameObject.GetComponent<Rigidbody2D>().velocity.x, -8));
            }
            else if (d == Direction.left)
            {
                plm.SetVelocity(new Vector2(8, col.gameObject.GetComponent<Rigidbody2D>().velocity.y));
            }
            else
            {
                plm.SetVelocity(new Vector2(-8, col.gameObject.GetComponent<Rigidbody2D>().velocity.y));
            }
        }

        else
        {
            Carrot c = col.gameObject.GetComponent<Carrot>();
            if (c != null)
            {
                if (d == Direction.up)
                {
                    c.SetVelocity(new Vector2(col.gameObject.GetComponent<Rigidbody2D>().velocity.x, 5));
                }
                else if (d == Direction.down)
                {
                    c.SetVelocity(new Vector2(col.gameObject.GetComponent<Rigidbody2D>().velocity.x, -5));
                }
                else if (d == Direction.left)
                {
                    c.SetVelocity(new Vector2(5, col.gameObject.GetComponent<Rigidbody2D>().velocity.y));
                }
                else
                {
                    c.SetVelocity(new Vector2(-5, col.gameObject.GetComponent<Rigidbody2D>().velocity.y));
                }
            }
        }
    }
}
