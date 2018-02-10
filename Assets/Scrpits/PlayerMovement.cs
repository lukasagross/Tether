using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float groundSpeed = 4f;
    public float swingSpeed = 6f;
    public float jumpSpeed = 5f;
    public float drag = 0.5f;
    public bool isGrounded;
    public bool grappleAttached;
    private Vector2 grappleAnchor;
    private Rigidbody2D rigidBody;
    private bool isJumping;
    private float jumpInput;
    private float horizontalInput;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.drag = drag;
    }

    void Update()
    {
        jumpInput = Input.GetAxis("Jump");
        horizontalInput = Input.GetAxis("Horizontal");

        //0.351 and 0.04 are magic numbers, they need to be tweaked if the collider is changed
        isGrounded = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.351f), Vector2.down, 0.04f);
    }

    void FixedUpdate()
    {
        isJumping = false;

        if (horizontalInput < -0.01f || horizontalInput > 0.01f)
        {
            if (grappleAttached)
            {
                var normDirection = (grappleAnchor - (Vector2) transform.position).normalized;

                //Flip vector if needed
                Vector2 perpDirection = horizontalInput < 0 ?
                    new Vector2(-normDirection.y, normDirection.x) :
                    new Vector2(normDirection.y, -normDirection.x);

                //Swing speed is only limited by gravity and drag
                rigidBody.AddForce(perpDirection * swingSpeed);
            }
            else
            {
                if (isGrounded)
                {
                    //P controller limits the maximum ground speed
                    rigidBody.AddForce(new Vector2((horizontalInput * groundSpeed - rigidBody.velocity.x) * groundSpeed, 0));
                }
            }
        }

        if (!grappleAttached)
        {
            if (!isGrounded) return;

            isJumping = jumpInput > 0.01f;
            if (isJumping)
            {
                //Jumping resets y velocity, it doesn't add a force
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
            }
        }
    }
    //The following functions are the interface for PlayerMovement. Try to use only these functions.
    public void SetAnchorPosition(Vector2 pos)
    {
        grappleAnchor = pos;
    }


    public void ApplyForce(Vector2 direction)
    {
        rigidBody.AddForce(direction, ForceMode2D.Impulse);
    }

    public void AddVelocity(Vector2 velocity)
    {
        rigidBody.velocity = velocity;
    }

    public void ChangePosition(Vector2 direction)
    {
        rigidBody.position += (direction * 0.1f);
    }
}

