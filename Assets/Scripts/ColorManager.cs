using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour {

    private PlayerControls player;
    private GameObject[] hexes;
    private Color[] colors;
    private List<int> taken;
    private AudioSource source;
    private Text text;
    private float spinspeed = 1f;
    private float fadetime = 0.5f;


    public int currentHex = 0;
    public bool isSelected = false;

    public ColorManager Manager1;
    public ColorManager Manager2;
    public ColorManager Manager3;


    void Start () {
        source = GetComponent<AudioSource>();
        player = transform.GetComponent<PlayerControls>();
        text = transform.GetChild(7).GetComponent<Text>();
        hexes = new GameObject[7];
        hexes[0] = transform.GetChild(0).gameObject;
        hexes[1] = transform.GetChild(1).gameObject;
        hexes[2] = transform.GetChild(2).gameObject;
        hexes[3] = transform.GetChild(3).gameObject;
        hexes[4] = transform.GetChild(4).gameObject;
        hexes[5] = transform.GetChild(5).gameObject;
        hexes[6] = transform.GetChild(6).gameObject;

        colors = new Color[7];
        colors[0] = Color.white;
        colors[1] = new Color(0.859f, 0.839f, 0.114f);
        colors[2] = new Color(0.008f, 0.503f, 0.012f);
        colors[3] = new Color(0.027f, 0.576f, 0.906f);
        colors[4] = new Color(0.667f, 0.129f, 0.765f);
        colors[5] = new Color(0.809f, 0.101f, 0.101f);
        colors[6] = new Color(0.923f, 0.509f, 0.011f);

        taken = new List<int>();
    }

    void Update()
    {
        RectTransform curr = hexes[currentHex].GetComponent<RectTransform>();
        curr.localScale = new Vector3(3.6f, 3.6f, 1f);
        if (!isSelected)
            currentHex = 0;

        HandleInput();
        if (isSelected)
            SpinHex(0);
    }

    private void HandleInput()
    {
        float x = player.getHorizontalAxis();
        float y = player.getVerticalAxis();

        if (!isSelected && (x != 0.0f || y != 0.0f))
        {
            float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            if (angle > 45f && angle < 135)
                currentHex = 1;
            if (angle <= 45f && angle >= 0f)
                currentHex = 2;
            if (angle < 0 && angle >= -45f)
                currentHex = 3;
            if (angle < -45 && angle > -135f)
                currentHex = 4;
            if (angle <= -135f)
                currentHex = 5;
            if (angle <= 180 && angle >= 135f)
                currentHex = 6;
            if (!taken.Contains(currentHex))
            {
                RectTransform next = hexes[currentHex].GetComponent<RectTransform>();
                next.localScale = new Vector3(3f, 3f, 1f);
            }
            else
            {
                currentHex = 0;
            }
        }

        if(player.getJumpAxis() && currentHex != 0)
        {
            isSelected = true;
            HandleSelect();
        }

        if (isSelected && player.getBAxis()){
            isSelected = false;
            for (int i = 0; i < 7; i++)
            {
                hexes[i].GetComponent<Image>().color = colors[i];
            }
            source.Play();
            Image image = hexes[0].GetComponent<Image>();
            hexes[0].GetComponent<Image>().fillAmount = 1;
            image.fillClockwise = false;
            text.color = Color.white;

            Manager1.UnrestrictColor(currentHex);
            Manager2.UnrestrictColor(currentHex);
            Manager3.UnrestrictColor(currentHex);

            foreach (int color in taken)
            {
                hexes[color].GetComponent<Image>().color = Color.white;
            }
        }

    }

    private void SpinHex(int index)
    {
        Image image = hexes[index].GetComponent<Image>();
        if (isSelected)
        {
            if (!image.fillClockwise)
            {
                image.fillAmount -= (Time.deltaTime / spinspeed);
                if (image.fillAmount <= 0)
                    image.fillClockwise = true;
            }
            else
            {
                image.fillAmount += (Time.deltaTime / spinspeed);
                if (image.fillAmount >= 1)
                    image.fillClockwise = false;
            }
        }
    }

    private void HandleSelect()
    {
        Manager1.RestrictColor(currentHex);
        Manager2.RestrictColor(currentHex);
        Manager3.RestrictColor(currentHex);

        source.Play();
        Color selected = hexes[currentHex].GetComponent<Image>().color;
        for (int i = 1; i <= hexes.Length; i++)
        {
            int index = (i + currentHex) % hexes.Length;
            Image curr = hexes[index].GetComponent<Image>();
            float delay = 0.3f + (i * fadetime);
            IEnumerator faderoutine = Fade(curr, colors[index], selected, Time.time, delay, false);
            StartCoroutine(faderoutine);
        }
        Image center = hexes[0].GetComponent<Image>();
        IEnumerator centerroutine = Fade(center, colors[0], selected, Time.time, 3f, true);
        StartCoroutine(centerroutine);
    }

    IEnumerator Fade(Image image, Color oldcolor, Color newcolor, float starttime, float delta, bool colorText)
    {
        float frac = 1f;
        while (frac != 0f)
        {
            if (!isSelected)
                break;
            float curr = Time.time;
            frac = Mathf.Max((starttime + delta - Time.time) / delta, 0);
            Color c = Color.Lerp(newcolor, oldcolor, frac);
            image.color = c;
            if (colorText)
                text.color = c;

            yield return null;
        }
    }

    public void RestrictColor(int color)
    {
        taken.Add(color);
        if (!isSelected)
            hexes[color].GetComponent<Image>().color = Color.white;
    }

    public void UnrestrictColor(int color)
    {
        taken.Remove(color);
        if (!isSelected)
            hexes[color].GetComponent<Image>().color = colors[color];
    }
}
