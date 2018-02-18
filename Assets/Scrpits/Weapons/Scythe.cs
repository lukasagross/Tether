﻿using System.Collections;
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

    public Score score;
    public float hitboxDuration = .25f;
    public float coolDown = .75f;
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
        aim = GetComponentInParent<Player>().GetComponentInChildren<Aim>();
    }

    void Update()
    {
        if (timeElapsed > hitboxDuration)
        {
            sr.color = new Color(0, 0, 0, 0);
            col.enabled = false;
            pTransform.rotation = Quaternion.Euler(0, 0, 0);
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
            reboundDir = aimDirection.normalized * -(bounceMultiplier); //The player rebounds opposite the direction they are aiming

            //Controls rotating the weapon
            rotation = Vector2.Angle(aimDirection, Vector2.up);
            Vector3 tempCross = Vector3.Cross(aimDirection, Vector2.up);
            if (tempCross.z > 0)
            {
                rotation = 360 - rotation;
            }

            if(rotation > 180)
            {
                StartCoroutine(cutRight());
            }
            else
            {
                StartCoroutine(cutLeft());
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
        }
        else if (collidedObject.GetComponent<PlayerMovement>() != null)
        {
            //Add code for victory condition

            collidedObject.GetComponent<PlayerMovement>().SetVelocity(-reboundDir);

            Debug.Log("Player " + collidedObject.GetComponent<Player>().playerNum + " was hit!");
            score.AddScore(playerNumber, 1);
        }
        
    }

    private IEnumerator cdTimer()
    {
        yield return new WaitForSeconds(coolDown - 0.05f);
        aim.ChangeColor(1f, 1f, 1f, 1f);
    }

   //The values in these coroutines are magic, found through experimentation.
    private IEnumerator cutRight()
    {
        pTransform.localScale = new Vector3(-1, 1, 1);
        pTransform.rotation = Quaternion.Euler(0, 0, 0);
        for (int i = 0; i < 200; i++)
        {
            pTransform.rotation = Quaternion.Slerp(pTransform.rotation, Quaternion.Euler(0, 0, 180), .075f);
            yield return null;
        }
    }

    private IEnumerator cutLeft()
    {
        pTransform.localScale = new Vector3(1, 1, 1);
        pTransform.rotation = Quaternion.Euler(0, 0, 0);
        for (int i = 0; i < 200; i++)
        {
            pTransform.rotation = Quaternion.Slerp(pTransform.rotation, Quaternion.Euler(0, 0, -180), .075f);
            yield return null;
        }
    }

}
