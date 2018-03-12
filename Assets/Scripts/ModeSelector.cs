using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModeSelector : MonoBehaviour
{

    private AudioSource click;
    private GameObject Mode;
    private RectTransform ModeRT;
    private RectTransform RT;
    private GameObject ModeText;
    private RectTransform ModeTextRT;
    private GameObject[] Modes;
    private string[] ModeNames;
    private int currentmap;
    private int selected = 0;
    private int numModes = 3;
    private float moveDistance = 4000f;

    public float scrollTime;

    void Start()
    {
        click = GetComponent<AudioSource>();
        Mode = transform.GetChild(0).gameObject;
        ModeRT = Mode.GetComponent<RectTransform>();
        RT = GetComponent<RectTransform>();
        ModeText = transform.GetChild(1).gameObject;
        ModeTextRT = ModeText.GetComponent<RectTransform>();

        Modes = new GameObject[numModes];
        Modes[0] = Mode.transform.GetChild(0).gameObject;
        Modes[1] = Mode.transform.GetChild(1).gameObject;
        Modes[2] = Mode.transform.GetChild(2).gameObject;

        ModeNames = new string[numModes];
        ModeNames[0] = "Score";
        ModeNames[1] = "Stocks";
        ModeNames[2] = "Carrots";

        currentmap = PlayerPrefs.GetInt("CurrentMap");

        ModeRT.position = new Vector2(moveDistance, ModeRT.position.y);
        ModeTextRT.position = new Vector2(moveDistance, ModeTextRT.position.y);
    }

    void Update()
    {
        if (ModeRT.position.x != RT.position.x)
            return;

        int oldselected = selected;
        HandleInput();

        Modes[oldselected].GetComponent<RectTransform>().localScale = new Vector2(0.18f, 0.18f);
        //Modes[oldselected].GetComponent<Image>().color = new Color(54, 55, 55);

        Modes[selected].GetComponent<RectTransform>().localScale = new Vector2(0.2f, 0.2f);
        //Modes[selected].GetComponent<Image>().color = new Color(70, 70, 70);


        if (oldselected != selected)
        {
            click.Play();
            ModeText.GetComponent<SettingsText>().UpdateText(ModeNames[selected], selected);
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selected = Mathf.Min(selected + 1, numModes - 1);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selected = Mathf.Max(selected - 1, 0);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            HandleSelect();
        }
    }

    private void HandleSelect()
    {
        IEnumerator moveWeapon = MoveRectTo(ModeRT, ModeRT.position.x, ModeRT.position.x - moveDistance, scrollTime);
        IEnumerator moveWeaponText = MoveRectTo(ModeTextRT, ModeTextRT.position.x, ModeTextRT.position.x - moveDistance, scrollTime);
        StartCoroutine(moveWeapon);
        StartCoroutine(moveWeaponText);

        PlayerPrefs.SetInt("mode" + currentmap, selected);
        int currentplayer = PlayerPrefs.GetInt("CurrentPlayer");

        for (int i = currentplayer; i < 4; i++) {
            if (PlayerPrefs.GetInt("color" + (currentplayer + i)) > 0)
            {
                PlayerPrefs.SetInt("CurrentPlayer", currentplayer + i);
                PlayerPrefs.SetInt("CurrentMap", currentmap + 1);
                SceneManager.LoadScene("Settings");
                return;
            }
        }
        string[] mapnames = new string[4];
        mapnames[0] = "UnderwaterMap";
        mapnames[1] = "FleshCaves";
        mapnames[2] = "WarpedMap";
        mapnames[3] = "ForestCave";
        int map = PlayerPrefs.GetInt("CurrentMap");
        SceneManager.LoadScene(mapnames[PlayerPrefs.GetInt("map" + map)]);
    }

    public void Activate()
    {
        IEnumerator moveMode = MoveRectTo(ModeRT, ModeRT.position.x, RT.position.x, scrollTime);
        IEnumerator moveModeText = MoveRectTo(ModeTextRT, ModeTextRT.position.x, RT.position.x, scrollTime);
        StartCoroutine(moveMode);
        StartCoroutine(moveModeText);
    }

    IEnumerator MoveRectTo(RectTransform rect, float oldx, float newx, float time)
    {
        float offset;
        float tempx;
        float starttime = Time.time;
        bool isPositiveDiff = newx < oldx;
        while (Time.time < starttime + time)
        {
            offset = (oldx - newx) * ((Time.time - starttime) / time);
            if (isPositiveDiff)
                tempx = Mathf.Max(newx, oldx - offset);
            else
                tempx = Mathf.Min(newx, oldx + offset);
            rect.position = new Vector2(tempx, rect.position.y);
            yield return null;
        }
        rect.position = new Vector2(newx, rect.position.y);
    }
}
