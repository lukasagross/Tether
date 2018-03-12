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
    private GameMode gm;
    private Score sc;
    private float startTime;

    //Should be set in unity editor
    public float iFrameDuration;

    void Awake()
    {
        sc = FindObjectOfType<Score>();
        gm = FindObjectOfType<GameMode>();
        sr = transform.GetChild(3).GetComponent<SpriteRenderer>();
        c = sr.color;
        hittable = true;
        animator = GetComponentInChildren<Animator>();
        playerNum = GetComponent<PlayerControls>().playerNum;

        if(iFrameDuration == 0)
        {
            iFrameDuration = 0.07f;
        }

        startTime = 0;
    }

    void Update()
    {
        if (startTime > .5f && gm.currentMode == GameMode.Mode.health && sc.GetScore(playerNum) == 0)
        {
            StartCoroutine(Die());
        }
        startTime += Time.deltaTime;
        if(startTime > 100)
        {
            startTime = 5;
        }
    }
    public void takeDamage()
    {
        StartCoroutine(flash());
        StartCoroutine(rumble());

        if(gm.currentMode == GameMode.Mode.carrots)
        {
            DropCarrots(sc.GetScore(playerNum));
        }

        if (gm.currentMode == GameMode.Mode.health)
        {
            sc.AddScore(playerNum, -1);
        }
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

    private void DropCarrots(int num)
    {
        if (num >= 3)
        {
            sc.AddScore(GetComponent<PlayerControls>().playerNum, -3);
            spawn3();
        }
        else if(num == 2)
        {
            sc.AddScore(GetComponent<PlayerControls>().playerNum, -2);
            spawn2();
        }else if(num == 1)
        {
            sc.AddScore(GetComponent<PlayerControls>().playerNum, -1);
            spawn1();
        }
        else
        {
            return;
        }

        
    }

    private void spawn3()
    {
        Object c = Resources.Load("Prefabs/Carrot");

        GameObject spawn = (GameObject)Instantiate(c, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        StartCoroutine(spawn.GetComponent<Carrot>().IgnoreCollisions(GetComponent<PlayerControls>().playerNum));
        spawn.GetComponent<Carrot>().SetVelocity(new Vector2(-3, 3));

        spawn = (GameObject)Instantiate(c, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        StartCoroutine(spawn.GetComponent<Carrot>().IgnoreCollisions(GetComponent<PlayerControls>().playerNum));
        spawn.GetComponent<Carrot>().SetVelocity(new Vector2(0, 3));

        spawn = (GameObject)Instantiate(c, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        StartCoroutine(spawn.GetComponent<Carrot>().IgnoreCollisions(GetComponent<PlayerControls>().playerNum));
        spawn.GetComponent<Carrot>().SetVelocity(new Vector2(3, 3));
    }

    private void spawn2()
    {
        Object c = Resources.Load("Prefabs/Carrot");

        GameObject spawn = (GameObject)Instantiate(c, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        StartCoroutine(spawn.GetComponent<Carrot>().IgnoreCollisions(GetComponent<PlayerControls>().playerNum));
        spawn.GetComponent<Carrot>().SetVelocity(new Vector2(-3, 3));

        spawn = (GameObject)Instantiate(c, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        StartCoroutine(spawn.GetComponent<Carrot>().IgnoreCollisions(GetComponent<PlayerControls>().playerNum));
        spawn.GetComponent<Carrot>().SetVelocity(new Vector2(3, 3));
    }

    private void spawn1()
    {
        Object c = Resources.Load("Prefabs/Carrot");

        GameObject spawn = (GameObject)Instantiate(c, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        StartCoroutine(spawn.GetComponent<Carrot>().IgnoreCollisions(GetComponent<PlayerControls>().playerNum));
        spawn.GetComponent<Carrot>().SetVelocity(new Vector2(0, 3));

    }

    private IEnumerator Die()
    {
        gm.killPlayer(playerNum);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Grapple>().enabled = false;
        GetComponentInChildren<WeaponController>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(flash());
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
