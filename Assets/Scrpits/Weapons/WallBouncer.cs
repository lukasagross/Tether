using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBouncer : MonoBehaviour {
    private float timeElapsed;
    private SpriteRenderer sr;
    private float rotation;
    private Transform pTransform;
    private Collider2D col;
    private Vector2 reboundDir;
    private int playerNumber;

    public Score score;
    public float hitboxDuration = 0.2f;
    public float coolDown = 0.6f;
    public float bounceMultiplier = 7;

    void Awake()
    {
        timeElapsed = 35;
        sr = GetComponent<SpriteRenderer>();
        pTransform = transform.parent;
        col = GetComponent<Collider2D>();
        reboundDir = Vector2.zero;
        score = FindObjectOfType<Score>();
        playerNumber = pTransform.parent.GetComponent<Player>().playerNum;
    }
	
	void Update () {
        if(timeElapsed > hitboxDuration) 
        {
            sr.color = new Color(0, 0, 0, 0);
            col.enabled = false;
        }
        timeElapsed += Time.deltaTime;
	}

    public void Attack(Vector2 aimDirection)
    {
        if (timeElapsed > coolDown) 
        {
            timeElapsed = 0;
            col.enabled = true;
            sr.color = new Color(1, 0.92f, 0.016f, 1); //yellow!!
            reboundDir = aimDirection.normalized*-(bounceMultiplier); //The player rebounds opposite the direction they are aiming
            
            rotation = Vector2.Angle(aimDirection, Vector2.up);
            Vector3 tempCross = Vector3.Cross(aimDirection, Vector2.up);
            if (tempCross.z > 0)
            {
                rotation = 360 - rotation;
            }
            pTransform.rotation = Quaternion.Euler(0, 0, rotation);
        }
    }

    //Handle collisions
   void OnTriggerEnter2D(Collider2D Other)
    {
        GameObject collidedObject = Other.gameObject;

        if(collidedObject.GetComponent<WallBouncer>() != null)
        {
            GetComponentInParent<PlayerMovement>().SetVelocity(reboundDir);
        }
        else if(collidedObject.GetComponent<PlayerMovement>() != null)
        {
            //Add code for victory condition

            collidedObject.GetComponent<PlayerMovement>().SetVelocity(-reboundDir);

            Debug.Log("Player " + collidedObject.GetComponent<Player>().playerNum + " was hit!");
            score.AddScore(playerNumber, 1);
        }else if (collidedObject.GetComponent<Obstacle>())
        {
            GetComponentInParent<PlayerMovement>().SetVelocity(reboundDir);
        }

    }

}
