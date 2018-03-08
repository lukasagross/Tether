using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    private SpriteRenderer sr;
    private Color c;
    private bool hittable;
    private Animator animator;

    //Should be set in unity editor
    public float iFrameDuration;

    void Awake()
    {
        sr = transform.GetChild(3).GetComponent<SpriteRenderer>();
        c = sr.color;
        hittable = true;
        animator = GetComponentInChildren<Animator>();

        if(iFrameDuration == 0)
        {
            iFrameDuration = 0.07f;
        }
    }

    public void takeDamage()
    {
        StartCoroutine(flash());
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
}
