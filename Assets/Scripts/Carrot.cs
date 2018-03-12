using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour {

    private Score sc;
    private int ignore;
    private AudioSource collect;

    void Start()
    {
        sc = FindObjectOfType<Score>();
        collect = GetComponent<AudioSource>();
    }
	
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.GetComponent<PlayerControls>() != null && col.gameObject.GetComponent<PlayerControls>().playerNum != ignore)
        {
            sc.AddScore(col.gameObject.GetComponent<PlayerControls>().playerNum, 1);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            StartCoroutine(PlayThenDie());
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

    private IEnumerator PlayThenDie()
    {
        collect.Play();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
