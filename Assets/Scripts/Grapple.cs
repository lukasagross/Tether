﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public GameObject grappleAnchor;
    public DistanceJoint2D joint;
    public PlayerMovement playerMovement;
    public LineRenderer lineRenderer;
    public LayerMask layerMask;
    public float climbSpeed = 6;
    public float launchSpeed = 14;

    private bool grappleAttached;
    private float grappleInput;
    private float horizontalInput;
    private float verticalInput;
    private float grappleMaxDistance = 20f;
    private Vector2 aimDirection;
    private Vector2 playerPosition;
    private Vector2 prevPosition;
    private Vector2 anchorPosition;
    private Rigidbody2D grappleAnchorRb;
    private SpriteRenderer grappleAnchorSprite;
    private PlayerControls player;

    private bool attachedToMoving = false;
    private MovingPlatform movingPlatform;

    void Awake()
    {
        grappleAnchorRb = grappleAnchor.GetComponent<Rigidbody2D>();
        grappleAnchorSprite = grappleAnchor.GetComponent<SpriteRenderer>();
        lineRenderer.positionCount = 2;
        joint.enabled = false;
        playerPosition = transform.position;
        player = GetComponent<PlayerControls>();
    }

    void Update()
    {
        grappleInput = player.getGrappleAxis();
        horizontalInput = player.getHorizontalAxis();
        verticalInput = player.getVerticalAxis();

        aimDirection = new Vector2(horizontalInput, verticalInput);
        playerPosition = transform.position;

        if (grappleAttached)
        {
            playerMovement.grappleAttached = true;
            playerMovement.SetAnchorPosition(anchorPosition);
        }
        else
        {
            playerMovement.grappleAttached = false;
        }
        HandleInput(aimDirection);
        UpdateGrapple();
    }

    private void HandleInput(Vector2 aimDirection)
    {
        if (grappleInput == 1f)
        {
            if (grappleAttached) return;
            lineRenderer.enabled = true;

            var hit = Physics2D.Raycast(playerPosition, aimDirection, grappleMaxDistance, layerMask);
            //
            if (hit.collider != null && Vector2.Distance(hit.point, playerPosition) > .6f)
            {
                attachedToMoving = (hit.collider.transform.gameObject.GetComponent<MovingPlatform>() != null);
                if (attachedToMoving)
                {
                    movingPlatform = hit.collider.transform.gameObject.GetComponent<MovingPlatform>();
                }
                ToggleGrapple(true);

                anchorPosition = hit.point;
                prevPosition = playerPosition;
                joint.distance = Vector2.Distance(playerPosition, hit.point);
                lineRenderer.SetPosition(0, anchorPosition);
            }

            else
            {
                ToggleGrapple(false);
            }
        }

        if (grappleAttached && grappleInput == 0f)
        {
            //Canceling the grapple early will give you a speed boost
            //Canceling later increases the boost
            var normDirection = (anchorPosition - playerPosition).normalized;
            var currDistance = Vector2.Distance(playerPosition, anchorPosition);
            var prevDistance = Vector2.Distance(prevPosition, anchorPosition);
            var multiplier = 1f - (currDistance / prevDistance);
            playerMovement.ApplyForce(normDirection * launchSpeed * multiplier);

            ToggleGrapple(false);
        }
    }

    public void ToggleGrapple(bool toggle)
    {
        joint.enabled = toggle;
        grappleAttached = toggle;
        grappleAnchorSprite.enabled = toggle;
        playerMovement.grappleAttached = toggle;

        if (!toggle)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position);
        }
    }

    private void UpdateGrapple()
    {
        if (!grappleAttached) return;

        //
        if(joint.distance < 0.4f)
        {
            ToggleGrapple(false);
            return;
        }
        //
        lineRenderer.SetPosition(1, transform.position);

        //I have no idea why this line needs to be here, but when I move it everything breaks
        grappleAnchorRb.transform.position = anchorPosition;

        //Shrink the grapple by the climbing speed modulated by player input
        joint.distance -= Time.deltaTime * climbSpeed;

        if (attachedToMoving)
        {
            float temp = joint.distance;
            anchorPosition += movingPlatform.GetMoveDistance();
            lineRenderer.SetPosition(0, anchorPosition);
        }
    }
    
    void OnCollisionStay2D(Collision2D coll)
    {
        //If you reach the achor point of the grapple, cancel the grapple
        ContactPoint2D[] contacts = new ContactPoint2D[100];
        //var numcontacts = coll.GetContacts(contacts);
        var collPos = contacts[0].point;
        var dist = Vector2.Distance(collPos, anchorPosition);

        //0.351 is a magic number, it needs to be tweaked if the collider changes
        if (grappleAttached && dist < 0.351)
        {
            ToggleGrapple(false);
        }
    }
}
