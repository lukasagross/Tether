using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour {

    private GameObject[] hexes;
    private float speed = 1f;
    // Use this for initialization
    void Start () {
        hexes = new GameObject[7];
        hexes[0] = transform.GetChild(0).gameObject;
        hexes[1] = transform.GetChild(1).gameObject;
        hexes[2] = transform.GetChild(2).gameObject;
        hexes[3] = transform.GetChild(3).gameObject;
        hexes[4] = transform.GetChild(4).gameObject;
        hexes[5] = transform.GetChild(5).gameObject;
        hexes[6] = transform.GetChild(6).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Image image = hexes[2].GetComponent<Image>();
        if (Input.GetKey(KeyCode.Space))
        {
            if (!image.fillClockwise)
            {
                image.fillAmount -= (Time.deltaTime / speed);
                if (image.fillAmount <= 0)
                    image.fillClockwise = true;
            }
            else
            {
                image.fillAmount += (Time.deltaTime / speed);
                if (image.fillAmount >= 1)
                    image.fillClockwise = false;
            }
        }
        else
        {
            hexes[2].GetComponent<Image>().fillAmount = 1;
            image.fillClockwise = false;
        }
    }
}
