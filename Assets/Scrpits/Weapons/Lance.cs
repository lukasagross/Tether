using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lance : MonoBehaviour
{
    private float timeElapsed;
    private SpriteRenderer sr;
    private float rotation;
    private Transform pTransform;
    private Collider2D col;
    private Vector2 reboundDir;
    private Vector2 dashDir;
    private Score score;
    private int playerNumber;

    public float hitboxDuration = 0.4f;
    public float coolDown = 1f;
    public float dashMultiplier = 4;

    void Awake()
    {
        timeElapsed = 35;
        sr = GetComponent<SpriteRenderer>();
        pTransform = transform.parent;
        col = GetComponent<Collider2D>();
        score = FindObjectOfType<Score>();
        playerNumber = pTransform.parent.GetComponent<Player>().playerNum;
    }

    void Update()
    {
        if (timeElapsed > hitboxDuration)
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
            sr.color = new Color(0, 1, 1, 1); //cyan!!
            reboundDir = aimDirection.normalized * -1.5f*(dashMultiplier); //Rebounding only occurs on a block

            dashDir = aimDirection.normalized * dashMultiplier; //Dashing occurs every attack
            GetComponentInParent<PlayerMovement>().AddVelocity(dashDir);

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

        if (collidedObject.GetComponent<Lance>() != null)
        {
            GetComponentInParent<PlayerMovement>().AddVelocity(reboundDir);
        }
        else if (collidedObject.GetComponent<PlayerMovement>() != null)
        {
            //Add code for victory condition
            GetComponentInParent<PlayerMovement>().AddVelocity(-0.8f*dashDir);
            collidedObject.GetComponent<PlayerMovement>().SetVelocity(dashDir);
            Debug.Log("Player " + collidedObject.GetComponent<Player>().playerNum + " was hit!");
            score.AddScore(playerNumber, 1);
        }
    }

}
