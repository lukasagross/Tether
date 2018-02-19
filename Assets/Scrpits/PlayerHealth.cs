using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    private SpriteRenderer sr;
    private Color c;
    private bool hittable;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        c = sr.color;
        hittable = true;
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
            yield return new WaitForSeconds(0.07f);
            sr.color = c;
            yield return new WaitForSeconds(0.07f);
        }
        hittable = true;
    }

    public bool canDamage()
    {
        return hittable;
    }
}
