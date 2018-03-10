using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerHealth : MonoBehaviour {

    private SpriteRenderer sr;
    private Color c;
    private bool hittable;
    private Animator animator;
    private int  playerNum;

    //Should be set in unity editor
    public float iFrameDuration;

    void Awake()
    {
        sr = transform.GetChild(3).GetComponent<SpriteRenderer>();
        c = sr.color;
        hittable = true;
        animator = GetComponentInChildren<Animator>();
        playerNum = GetComponent<PlayerControls>().playerNum;

        if(iFrameDuration == 0)
        {
            iFrameDuration = 0.07f;
        }
    }

    public void takeDamage()
    {
        StartCoroutine(flash());
        StartCoroutine(rumble());
    }

    private IEnumerator flash()
    {
        animator.SetTrigger("PlayerHit");
        hittable = false;
        for (int i = 0; i < 5; i++) {
            sr.color = Color.red;
            yield return new WaitForSeconds(iFrameDuration);
            sr.color = c;
            yield return new WaitForSeconds(iFrameDuration);
        }

        hittable = true;
    }

    public bool canDamage()
    {
        return hittable;
    }

    private IEnumerator rumble()
    {
        switch (playerNum)
        {
            case 1:
                {
                    GamePad.SetVibration(PlayerIndex.One, 0.5f, 0.5f);
                    break;
                }
            case 2:
                {
                    GamePad.SetVibration(PlayerIndex.Two, 0.5f, 0.5f);
                    break;
                }
            case 3:
                {
                    GamePad.SetVibration(PlayerIndex.Three, 0.5f, 0.5f);
                    break;
                }
            case 4:
                {
                    GamePad.SetVibration(PlayerIndex.Four, 0.5f, 0.5f);
                    break;
                }
        }

                yield return new WaitForSeconds(0.5f);

                switch (playerNum)
                {
                    case 1:
                        {
                            GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
                            break;
                        }
                    case 2:
                        {
                            GamePad.SetVibration(PlayerIndex.Two, 0f, 0f);
                            break;
                        }
                    case 3:
                        {
                            GamePad.SetVibration(PlayerIndex.Three, 0f, 0f);
                            break;
                        }
                    case 4:
                        {
                            GamePad.SetVibration(PlayerIndex.Four, 0f, 0f);
                            break;
                        }
                }
        
    }
}
