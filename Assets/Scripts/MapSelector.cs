using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelector : MonoBehaviour {

    private AudioSource click;
    private RectTransform IconsRT;
    private RectTransform RT;
    private GameObject Background;
    private GameObject Icon;

    private GameObject[] Backgrounds;
    private GameObject[] Icons;
    private string[] MapNames;
    private string[] Numbers;
    private Color[] colors;

    private GameObject MapText;
    private RectTransform MapTextRT;

    private Text InfoText;

    private int selected = 0;
    private int numLevels = 3;
    private int currplayer;
    private int currmap;
    private float moveDistance = 4000f;

    public WeaponSelector weaponselector;
    public float scrollTime;

	void Start () {

        click = GetComponent<AudioSource>();
        RT = GetComponent<RectTransform>();
        Background = transform.GetChild(0).gameObject;
        Icon = transform.GetChild(1).gameObject;
        MapText = transform.GetChild(2).gameObject;
        IconsRT = Icon.GetComponent<RectTransform>();
        MapTextRT = MapText.GetComponent<RectTransform>();

        Backgrounds = new GameObject[numLevels];
        Icons = new GameObject[numLevels];
        MapNames = new string[numLevels];
        Numbers = new string[5];
        colors = new Color[7];

        Backgrounds[0] = Background.transform.GetChild(0).gameObject;       
        Backgrounds[1] = Background.transform.GetChild(1).gameObject;
        Backgrounds[2] = Background.transform.GetChild(2).gameObject;

        Icons[0] = Icon.transform.GetChild(0).gameObject;
        Icons[1] = Icon.transform.GetChild(1).gameObject;
        Icons[2] = Icon.transform.GetChild(2).gameObject;

        MapNames[0] = "Sunken Grotto";
        MapNames[1] = "Hell Cave";
        MapNames[2] = "Warped Caverns";

        colors[0] = Color.white;
        colors[1] = new Color(0.859f, 0.839f, 0.114f);
        colors[2] = new Color(0.008f, 0.503f, 0.012f);
        colors[3] = new Color(0.027f, 0.576f, 0.906f);
        colors[4] = new Color(0.667f, 0.129f, 0.765f);
        colors[5] = new Color(0.809f, 0.101f, 0.101f);
        colors[6] = new Color(0.923f, 0.509f, 0.011f);

        Numbers[0] = "Error";
        Numbers[1] = "One";
        Numbers[2] = "Two";
        Numbers[3] = "Three";
        Numbers[4] = "Four";

        InfoText = GameObject.FindGameObjectWithTag("InfoText").GetComponent<Text>();
        currplayer = PlayerPrefs.GetInt("CurrentPlayer");
        currmap = PlayerPrefs.GetInt("CurrentMap");
        InfoText.text = "Player " + Numbers[currplayer];
        InfoText.color = colors[PlayerPrefs.GetInt("color" + currplayer)];

        MapTextRT.position = new Vector2(moveDistance, MapTextRT.position.y);
        IconsRT.position = new Vector2(moveDistance, IconsRT.position.y);

        IEnumerator moveIcons = MoveRectTo(IconsRT, IconsRT.position.x, RT.position.x, scrollTime);
        IEnumerator moveText = MoveRectTo(MapTextRT, MapTextRT.position.x, RT.position.x, scrollTime);
        StartCoroutine(moveIcons);
        StartCoroutine(moveText);
    }

    void Update () {

        if (IconsRT.position.x != RT.position.x)
            return;

        int oldselected = selected;
        HandleInput();

        Icons[oldselected].GetComponent<RectTransform>().localScale = new Vector2(0.2f, 0.2f);
        Backgrounds[oldselected].SetActive(false);

        Icons[selected].GetComponent<RectTransform>().localScale = new Vector2(0.18f, 0.18f);
        Backgrounds[selected].SetActive(true);

        if (oldselected != selected)
        {
            click.Play();
            MapText.GetComponent<SettingsText>().UpdateText(MapNames[selected], selected);
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selected = Mathf.Min(selected + 1, numLevels - 1);
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
        IEnumerator moveIcons = MoveRectTo(IconsRT, IconsRT.position.x, IconsRT.position.x - moveDistance, scrollTime);
        IEnumerator moveText = MoveRectTo(MapTextRT, MapTextRT.position.x , MapTextRT.position.x - moveDistance, scrollTime);
        StartCoroutine(moveIcons);
        StartCoroutine(moveText);

        PlayerPrefs.SetInt("map" + currmap, selected);

        weaponselector.gameObject.SetActive(true);
        weaponselector.Activate();
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
