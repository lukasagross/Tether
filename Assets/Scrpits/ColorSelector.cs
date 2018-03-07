using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelector : MonoBehaviour
{
    private Outline border;
    private GameObject[] boxes;
    private Color[] colors;
    private int index;
    private float timeout;
    private AudioClip sound;
    private AudioSource source;
    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        sound = source.clip;
        border = transform.GetChild(0).gameObject.GetComponent<Outline>();
        boxes = new GameObject[5];
        boxes[0] = transform.GetChild(1).gameObject;
        boxes[1] = transform.GetChild(2).gameObject;
        boxes[2] = transform.GetChild(3).gameObject;
        boxes[3] = transform.GetChild(4).gameObject;
        boxes[4] = transform.GetChild(5).gameObject;

        colors = new Color[] { Color.blue, Color.cyan, Color.green, Color.magenta, Color.red, Color.white, Color.yellow, Color.gray };

        for (int i = 0; i < 5; i++)
        {
            boxes[i].GetComponent<Image>().color = colors[i];
        }
        index = 1;

    }

    // Update is called once per frame
    void Update()
    {
        timeout -= Time.deltaTime;
        if (timeout < 0 && Input.GetKey(KeyCode.RightArrow))
        {
            index = (index + 1) % colors.Length;
            HandleScroll();
        }

        if (timeout < 0 && Input.GetKey(KeyCode.LeftArrow))
        {
            index = (index == 0) ? colors.Length - 1 : index - 1;
            HandleScroll();
        }

        if (Input.GetKey(KeyCode.Return))
        {
            border.effectColor = colors[(index + 2) % colors.Length];
        }
    }
    void HandleScroll()
    {
        int j = index;
        for (int i = 0; i < 5; i++)
        {
            Image image = boxes[i].GetComponent<Image>();
            image.color = colors[j];
            j = (j + 1) % colors.Length;
        }
        source.Play();
        timeout = 0.2f;
        border.effectColor = Color.white;
    }




    IEnumerator Fade(Image image, Color oldcolor, Color newcolor, float starttime, float delta)
    {
        float frac = 1f;
        while (frac != 0f)
        {
            float curr = Time.time;
            frac = Mathf.Max((starttime + delta - Time.time) / delta, 0);
            image.color = Color.Lerp(newcolor, oldcolor, frac);

            yield return null;
        }
    }
}
