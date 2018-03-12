using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour {

    private Score sc;
    private int ignore;

    void Start()
    {
        sc = FindObjectOfType<Score>();
    }
	
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.GetComponent<PlayerControls>() != null && col.gameObject.GetComponent<PlayerControls>().playerNum != ignore)
        {
            sc.AddScore(col.gameObject.GetComponent<PlayerControls>().playerNum, 1);
            Destroy(gameObject);
        }
    }

    public void SetVelocity(Vector2 v)
    {
        GetComponent<Rigidbody2D>().velocity = v;
    }

    public IEnumerator IgnoreCollisions(int playerIgnore)
    {
        ignore = playerIgnore;   
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Collider2D>().enabled = true;
        yield return new WaitForSeconds(1f);
        ignore = 0;
    }
}
