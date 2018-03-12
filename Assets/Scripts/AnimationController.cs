using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
    private Animator animator;
    private Vector2 aimDirection;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private PlayerControls player;
    private float rotation;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        player = GetComponentInParent<PlayerControls>();
        string[] animators = new string[7];
        int index = PlayerPrefs.GetInt("color" + player.playerNum);
        if (index == 0)
        {
            transform.parent.gameObject.SetActive(false);
            return;
        }
        animators[1] = "Yellow";
        animators[2] = "Green";
        animators[3] = "Blue";
        animators[4] = "Purple";
        animators[5] = "Red";
        animators[6] = "Orange";

        animator.runtimeAnimatorController = Resources.Load(animators[index]) as RuntimeAnimatorController;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponentInParent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!gameObject.activeSelf)
        {
            return;
        }
       
        aimDirection = new Vector2(player.getHorizontalAxis(), player.getVerticalAxis());
        rotation = Vector2.Angle(aimDirection, Vector2.up);
        Vector3 tempCross = Vector3.Cross(aimDirection, Vector2.up);
        if (tempCross.z > 0)
        {
            rotation = 360 - rotation;
        }
            
        if(rotation < 180)
        {
            transform.localScale = new Vector3(-10, 5, 1);
        }
        else
        {
            transform.localScale = new Vector3(10, 5, 1);
        }

        if(rb.velocity.x > 0 || rb.velocity.x < 0)
        {
            animator.SetBool("MovingLeftRight", true);
        }
        else
        {
            animator.SetBool("MovingLeftRight", false);
        }

        if(rb.velocity.y > 0)
        {
            animator.SetBool("MovingUp", true);
            animator.SetBool("MovingDown", false);
        }

        if(rb.velocity.y < 0)
        {
            animator.SetBool("MovingDown", true);
            animator.SetBool("MovingUp", false);
        }

        if(rb.velocity.y == 0)
        {
            animator.SetBool("MovingDown", false);
            animator.SetBool("MovingUp", false);
        }


    }
}
