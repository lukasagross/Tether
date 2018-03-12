using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour {

    private PlayerMovement plm;
    private Carrot c;
    private AudioSource bounce;

	// Use this for initialization
	void Start () {
        bounce = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        plm = col.gameObject.GetComponent<PlayerMovement>();
        if (plm != null)
        {
            plm.SetVelocity(new Vector2(col.gameObject.GetComponent<Rigidbody2D>().velocity.x, 11));
            bounce.Play();
        }

        c = col.gameObject.GetComponent<Carrot>();
        if (c != null)
        {
            c.SetVelocity(new Vector2(col.gameObject.GetComponent<Rigidbody2D>().velocity.x, 6));
        }
    }
}
