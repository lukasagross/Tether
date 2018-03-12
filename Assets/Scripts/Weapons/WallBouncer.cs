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
    private Aim aim;
    private Score score;
    private GameMode gm;
    private AudioSource clash;

    public float hitboxDuration = 0.2f;
    public float coolDown = 0.6f;
    public float bounceMultiplier = 8;

    void Awake()
    {
        timeElapsed = 35;
        sr = GetComponent<SpriteRenderer>();
        pTransform = transform.parent;
        col = GetComponent<Collider2D>();
        reboundDir = Vector2.zero;
        score = FindObjectOfType<Score>();
        playerNumber = GetComponentInParent<PlayerControls>().playerNum;
        aim = GetComponentInParent<PlayerControls>().GetComponentInChildren<Aim>();
        gm = FindObjectOfType<GameMode>();
        clash = GetComponentInParent<AudioSource>();
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
            //Controls cd display
            aim.ChangeColor(1f, 0f, 0f, 1f);
            StartCoroutine(cdTimer());

            //Resets variables
            timeElapsed = 0;
            col.enabled = true;
            sr.color = Color.white;
            reboundDir = aimDirection.normalized*-(bounceMultiplier); //The player rebounds opposite the direction they are aiming
            
            //Controls rotating the weapon
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
            col.enabled = false;
            clash.Play();
        }
        else if(collidedObject.GetComponent<PlayerMovement>() != null)
        {
            //Add code for victory condition

            collidedObject.GetComponent<PlayerMovement>().SetVelocity(-reboundDir);

            if (collidedObject.GetComponent<PlayerHealth>().canDamage())
            {
                collidedObject.GetComponent<PlayerHealth>().takeDamage();
                if (gm.currentMode == GameMode.Mode.hits)
                {
                    score.AddScore(playerNumber, 1);
                }
                col.enabled = false;
            }
        }else if (collidedObject.GetComponent<Obstacle>())
        {
            GetComponentInParent<PlayerMovement>().SetVelocity(reboundDir);
        }

    }

    private IEnumerator cdTimer()
    {
        yield return new WaitForSeconds(coolDown - 0.05f);
        aim.ChangeColor(1f, 1f, 1f, 1f);
    }

}
