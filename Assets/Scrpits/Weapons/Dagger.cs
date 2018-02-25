using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    private float timeElapsed;
    private SpriteRenderer sr;
    private float rotation;
    private Transform pTransform;
    private Collider2D col;
    private Vector2 reboundDir;
    private int playerNumber;
    private Aim aim;
    private PlayerControls player;
    private Vector2 dashDir;
    private Score score;

    public float hitboxDuration = 0.6f;
    public float coolDown = 1.6f;
    public float dashMultiplier = 4.5f;

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
        player = GetComponentInParent<PlayerControls>();
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

    public IEnumerator Attack(Vector2 aimDirection)
    {
        if (timeElapsed > coolDown)
        {
            //Controls cd display
            aim.ChangeColor(1f, 0f, 0f, 1f);
            StartCoroutine(cdTimer());

            //Resets variables
            timeElapsed = 0;
            col.enabled = true;
            sr.color = new Color(0, 0, 0, 1); //black!!
            reboundDir = aimDirection.normalized * -(dashMultiplier); //The player rebounds opposite the direction they are aiming
            dashDir = aimDirection.normalized * dashMultiplier; //Dashing occurs every attack
            GetComponentInParent<PlayerMovement>().AddVelocity(dashDir);

            //Controls rotating the weapon
            rotation = Vector2.Angle(aimDirection, Vector2.up);
            Vector3 tempCross = Vector3.Cross(aimDirection, Vector2.up);
            if (tempCross.z > 0)
            {
                rotation = 360 - rotation;
            }
            pTransform.rotation = Quaternion.Euler(0, 0, rotation);

            

            //******************swing again with new aimDirection*************************
            yield return new WaitForSeconds(0.15f);
            sr.color = new Color(1, 1, 1, 0);
            col.enabled = false;
            yield return new WaitForSeconds(0.05f);
            col.enabled = true;

            aimDirection = new Vector2(Input.GetAxis(player.getHorizontalAxis()), Input.GetAxis(player.getVerticalAxis()));
            sr.color = new Color(0, 0, 0, 1); //blacc!!
            reboundDir = aimDirection.normalized * -(dashMultiplier);
            dashDir = aimDirection.normalized * dashMultiplier; //Dashing occurs every attack
            GetComponentInParent<PlayerMovement>().AddVelocity(dashDir);

            //Rotate again
            rotation = Vector2.Angle(aimDirection, Vector2.up);
            tempCross = Vector3.Cross(aimDirection, Vector2.up);
            if (tempCross.z > 0)
            {
                rotation = 360 - rotation;
            }
            pTransform.rotation = Quaternion.Euler(0, 0, rotation);

            //******************swing a third time with new aimDirection*************************
            yield return new WaitForSeconds(0.15f);
            sr.color = new Color(1, 1, 1, 0);
            col.enabled = false;
            yield return new WaitForSeconds(0.05f);
            col.enabled = true;

            aimDirection = new Vector2(Input.GetAxis(player.getHorizontalAxis()), Input.GetAxis(player.getVerticalAxis()));
            sr.color = new Color(0, 0, 0, 1); //black!!
            reboundDir = aimDirection.normalized * -(dashMultiplier);
            dashDir = aimDirection.normalized * dashMultiplier; //Dashing occurs every attack
            GetComponentInParent<PlayerMovement>().AddVelocity(dashDir);

            //Rotate again
            rotation = Vector2.Angle(aimDirection, Vector2.up);
            tempCross = Vector3.Cross(aimDirection, Vector2.up);
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

        if (collidedObject.GetComponent<Dagger>() != null)
        {
            GetComponentInParent<PlayerMovement>().SetVelocity(reboundDir);
            col.enabled = false;
        }
        else if (collidedObject.GetComponent<PlayerMovement>() != null)
        {
            //Add code for victory condition

            collidedObject.GetComponent<PlayerMovement>().SetVelocity(-reboundDir);

            if (collidedObject.GetComponent<PlayerHealth>().canDamage())
            {
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

}
