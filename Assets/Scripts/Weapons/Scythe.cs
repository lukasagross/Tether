using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    private float timeElapsed;
    private SpriteRenderer sr;
    private float rotation;
    private Transform pTransform;
    private Collider2D col;
    private Vector2 reboundDir;
    private int playerNumber;
    private Aim aim;
    private Score score;

    public float hitboxDuration = .25f;
    public float coolDown = .75f;
    public float dashMultiplier = 4;

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
            //Controls cd display
            aim.ChangeColor(1f, 0f, 0f, 1f);
            StartCoroutine(cdTimer());

            //Resets variables
            timeElapsed = 0;
            col.enabled = true;
            sr.color = new Color(0.8f, 0.7f, 0.85f, 1); //lavender bois!!
            reboundDir = aimDirection.normalized * -(dashMultiplier); //The player rebounds opposite the direction they are aiming

            //Controls rotating the weapon
            rotation = Vector2.Angle(aimDirection, Vector2.up);
            Vector3 tempCross = Vector3.Cross(aimDirection, Vector2.up);
            if (tempCross.z > 0)
            {
                rotation = 360 - rotation;
            }

            if(rotation > 270)
            {
                StartCoroutine(cutUpRight());
            }
            else if(rotation > 180)
            {
                StartCoroutine(cutDownRight());
            }
            else if(rotation > 90)
            {
                StartCoroutine(cutDownLeft());
            }
            else
            {
                StartCoroutine(cutUpLeft());
            }
        }
    }

    //Handle collisions
    void OnTriggerEnter2D(Collider2D Other)
    {
        GameObject collidedObject = Other.gameObject;

        if (collidedObject.GetComponent<Scythe>() != null)
        {
            GetComponentInParent<PlayerMovement>().SetVelocity(reboundDir);
            col.enabled = false;
        }
        else if (collidedObject.GetComponent<PlayerMovement>() != null)
        {
            collidedObject.GetComponent<PlayerMovement>().SetVelocity(-reboundDir);

            if (collidedObject.GetComponent<PlayerHealth>().canDamage()) { 
            collidedObject.GetComponent<PlayerHealth>().takeDamage();
            score.AddScore(playerNumber, 1);
            col.enabled = false;
            }
        }
        
    }

    private IEnumerator cdTimer()
    {
        yield return new WaitForSeconds(coolDown - 0.05f);
        aim.ChangeColor(1f, 1f, 1f, 1f);
    }

   //The values in these coroutines are magic, found through experimentation.
    private IEnumerator cutUpRight()
    {
        //Gain velocity in slash direction
        GetComponentInParent<PlayerMovement>().AddVelocity(new Vector2(1, 1) * dashMultiplier);

        //Point up and right
        pTransform.localScale = new Vector3(-1, 1, 1);
        pTransform.rotation = Quaternion.Euler(0, 0, 0);

        float temp = 0f;
        float rate = 1 / hitboxDuration;
        while (temp < 1.0)
        {
            temp += Time.deltaTime * rate;
            pTransform.rotation = Quaternion.Slerp(pTransform.rotation, Quaternion.Euler(0, 0, 240), temp);
            yield return null;
        }
    }

    private IEnumerator cutDownRight()
    {
        //Gain velocity in slash direction
        GetComponentInParent<PlayerMovement>().AddVelocity(new Vector2(1, .5f) * dashMultiplier);

        //Point right
        pTransform.localScale = new Vector3(-1, 1, 1);
        pTransform.rotation = Quaternion.Euler(0, 0, 270);

        float temp = 0f;
        float rate = 1 / hitboxDuration;
        while (temp < 1.0)
        {
            temp += Time.deltaTime * rate;
            pTransform.rotation = Quaternion.Slerp(pTransform.rotation, Quaternion.Euler(0, 0, 150), temp);
            yield return null;
        }
    }

    private IEnumerator cutUpLeft()
    {
        //Gain velocity in slash direction
        GetComponentInParent<PlayerMovement>().AddVelocity(new Vector2(-1, 1) * dashMultiplier);

        //Point up and left
        pTransform.localScale = new Vector3(1, 1, 1);
        pTransform.rotation = Quaternion.Euler(0, 0, 0);

        float temp = 0f;
        float rate = 1 / hitboxDuration;
        while (temp < 1.0)
        {
            temp += Time.deltaTime * rate;
            pTransform.rotation = Quaternion.Slerp(pTransform.rotation, Quaternion.Euler(0, 0, -240), temp);
            yield return null;
        }
    }

    private IEnumerator cutDownLeft()
    {
        //Gain velocity in slash direction
        GetComponentInParent<PlayerMovement>().AddVelocity(new Vector2(-1, .5f) * dashMultiplier);

        //Point up and left
        pTransform.localScale = new Vector3(1, 1, 1);
        pTransform.rotation = Quaternion.Euler(0, 0, -270);

        float temp = 0f;
        float rate = 1 / hitboxDuration;
        while (temp < 1.0)
        {
            temp += Time.deltaTime * rate;
            pTransform.rotation = Quaternion.Slerp(pTransform.rotation, Quaternion.Euler(0, 0, -150), temp);
            yield return null;
        }
    }

}
