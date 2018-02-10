using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public GameObject grappleAnchor;
    public DistanceJoint2D joint;
    public PlayerMovement playerMovement;
    public LineRenderer lineRenderer;
    public LayerMask layerMask;
    public float climbSpeed = 7;
    public float launchSpeed = 14;

    private bool grappleAttached;
    private float grappleMaxDistance = 10f;
    private Collider2D currentCollision;
    private Vector2 playerPosition;
    private Vector2 prevPosition;
    private Rigidbody2D grappleAnchorRb;
    private SpriteRenderer grappleAnchorSprite;
    private Vector2 anchorPosition;


    void Awake()
    {
        grappleAnchorRb = grappleAnchor.GetComponent<Rigidbody2D>();
        grappleAnchorSprite = grappleAnchor.GetComponent<SpriteRenderer>();
        lineRenderer.positionCount = 2;
        joint.enabled = false;
        playerPosition = transform.position;
    }

    void Update()
    {
        //TODO: switch over to XBox controllers after we decided on keybindings
        //Controllers are actually easier than mouse so it shouldn't be a problem
        var worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        var facingDirection = worldMousePosition - transform.position;
        var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
        if (aimAngle < 0f)
        {
            aimAngle = (Mathf.PI * 2) + aimAngle;
        }

        //Don't worry about the math, it checks out
        var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
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
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (grappleAttached) return;
            lineRenderer.enabled = true;

            var hit = Physics2D.Raycast(playerPosition, aimDirection, grappleMaxDistance, layerMask);

            if (hit.collider != null)
            {
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

        if (grappleAttached && Input.GetKeyDown(KeyCode.Space))
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

    private void ToggleGrapple(bool toggle)
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

        lineRenderer.SetPosition(1, transform.position);

        //I have no idea why this line needs to be here, but when I move it everything breaks
        grappleAnchorRb.transform.position = anchorPosition;

        //Shrink the grapple by the climbing speed
        joint.distance -= Time.deltaTime * climbSpeed;
    }
    
    void OnCollisionStay2D(Collision2D coll)
    {
        //If you reach the achor point of the grapple, cancel the grapple
        currentCollision = coll.collider;
        ContactPoint2D[] contacts = new ContactPoint2D[100];
        var numcontacts = coll.GetContacts(contacts);
        var collPos = contacts[0].point;
        var dist = Vector2.Distance(collPos, anchorPosition);
        //0.351 is a magic number, it needs to be tweaked if the collider changes
        if (grappleAttached && dist < 0.351)
        {
            ToggleGrapple(false);
        }
    }
}
