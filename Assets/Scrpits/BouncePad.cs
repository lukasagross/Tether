using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour {

    private PlayerMovement plm;

	// Use this for initialization
	void Start () {
		
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
        }
    }
}
