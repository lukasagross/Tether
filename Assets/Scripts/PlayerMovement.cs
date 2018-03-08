using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float groundSpeed = 4f;
    public float swingSpeed = 10f;
    public float jumpSpeed = 6f;
    public float drag = 0.5f;
    public bool isGrounded;
    public bool grappleAttached;

    private Vector2 grappleAnchor;
    private Rigidbody2D rigidBody;
    private bool isJumping;
    private bool canJump;
    private bool jumpInput;
    private float horizontalInput;
    private PlayerControls player;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.drag = drag;
        player = GetComponent<PlayerControls>();
    }

    void Update()
    {
        jumpInput = player.getJumpAxis();
        horizontalInput = player.getHorizontalAxis();

        //0.351 and 0.04 are magic numbers, they need to be tweaked if the collider is changed
        isGrounded = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.351f), Vector2.down, 0.04f);
        if (isGrounded) canJump = true;
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
            isJumping = jumpInput;

            if (!isJumping || !canJump) return;

            if (isGrounded)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
            }

            else
            {
                rigidBody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                //rigidBody.velocity = rigidBody.velocity.normalized * jumpSpeed;
                //rigidBody.AddForce(rigidBody.velocity.normalized * jumpSpeed, ForceMode2D.Impulse);
            }
            canJump = false;
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
        rigidBody.velocity += velocity;
    }

    public void SetVelocity(Vector2 velocity)
    {
        rigidBody.velocity = velocity;
    }

    public void ChangePosition(Vector2 direction)
    {
        rigidBody.position += (direction * 0.1f);
    }
}

