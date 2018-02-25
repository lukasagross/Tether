using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    private SpriteRenderer sr;
    private Color c;
    private bool hittable;

    //Should be set in unity editor
    public float iFrameDuration;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        c = sr.color;
        hittable = true;

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
