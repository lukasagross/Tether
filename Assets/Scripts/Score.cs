using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using XInputDotNetPure;

public class Score : MonoBehaviour {
    private int playerOneScore = 0;
    private int playerTwoScore = 0;
    private int playerThreeScore = 0;
    private int playerFourScore = 0;
    private Text playerOneText;
    private Text playerTwoText;
    private Text playerThreeText;
    private Text playerFourText;
    private RectTransform rt;
    private int scoreToWin;
    private GameMode gm;

    public bool IsTutorial;

    void Start()
    {
        playerOneText = transform.GetChild(0).GetComponent<Text>();
        playerTwoText = transform.GetChild(1).GetComponent<Text>();
        playerThreeText = transform.GetChild(2).GetComponent<Text>();
        playerFourText = transform.GetChild(3).GetComponent<Text>();
        rt = GetComponent<RectTransform>();

        GameObject handle;

        gm = FindObjectOfType<GameMode>();
        if (gm.currentMode == GameMode.Mode.carrots)
        {
            transform.GetChild(4).gameObject.SetActive(true);
            transform.GetChild(5).gameObject.SetActive(false);
            transform.GetChild(6).gameObject.SetActive(false);
            handle = transform.GetChild(4).gameObject;
        }
        else if (gm.currentMode == GameMode.Mode.hits)
        {
            transform.GetChild(4).gameObject.SetActive(false);
            transform.GetChild(5).gameObject.SetActive(true);
            transform.GetChild(6).gameObject.SetActive(false);
            handle = transform.GetChild(5).gameObject;
        }
        else if (gm.currentMode == GameMode.Mode.health)
        {
            transform.GetChild(4).gameObject.SetActive(false);
            transform.GetChild(5).gameObject.SetActive(false);
            transform.GetChild(6).gameObject.SetActive(true);
            handle = transform.GetChild(6).gameObject;
        }
        else
        {
            handle = null;
            Debug.Log("Gamemode Not Set");
            Debug.Log(gm.currentMode);
        }
        if (!IsTutorial)
        {
            HandlePlayer(handle, 1);
            HandlePlayer(handle, 2);
            HandlePlayer(handle, 3);
            HandlePlayer(handle, 4);
        }
    }

    void Awake()
    {
        scoreToWin = FindObjectOfType<GameMode>().scoreToWin;
        StartCoroutine(changeRes());
    }

    public void Win(int playernum)
    {
        int currentmap = PlayerPrefs.GetInt("CurrentMap");
        int nummaps = PlayerPrefs.GetInt("NumMaps");
        PlayerPrefs.SetInt("winner" + currentmap, playernum);
        PlayerPrefs.Save();
        float temp = Time.timeScale;
        Time.timeScale = 0;
        System.Threading.Thread.Sleep(1000);
        GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
        GamePad.SetVibration(PlayerIndex.Two, 0f, 0f);
        GamePad.SetVibration(PlayerIndex.Three, 0f, 0f);
        GamePad.SetVibration(PlayerIndex.Four, 0f, 0f);
        Time.timeScale = temp;

        if (nummaps > currentmap)
        {
            string[] mapnames = new string[4];
            mapnames[0] = "UnderwaterMap";
            mapnames[1] = "FleshCaves";
            mapnames[2] = "WarpedMap";
            mapnames[3] = "ForestCave";

            PlayerPrefs.SetInt("CurrentMap", currentmap + 1);
            SceneManager.LoadScene(mapnames[PlayerPrefs.GetInt("map" + (currentmap + 1))]);
            return;
        }
        SceneManager.LoadScene("GameOver");
    }
	
    public void AddScore(int playernum, int score)
    {
        switch (playernum)
        {
            case 1:
                {
                    playerOneScore += score;
                    playerOneScore = (playerOneScore < 0 ? 0 : playerOneScore);
                    var text = playerOneScore.ToString();
                    if (playerOneScore < 10)
                    {
                        text = ":0" + text;
                    }
                    else
                    {
                        text = ":" + text;
                    }

                    playerOneText.text = text;
                    if (playerOneScore >= scoreToWin) Win(1);
                    break;
                }

            case 2:
                {
                    playerTwoScore += score;
                    playerTwoScore = (playerTwoScore < 0 ? 0 : playerTwoScore);
                    var text = playerTwoScore.ToString();
                    if (playerOneScore < 10)
                    {
                        text = ":0" + text;
                    }
                    else
                    {
                        text = ":" + text;
                    }
                    playerTwoText.text = text;
                    if (playerTwoScore >= scoreToWin) Win(2);
                    break;
                }

            case 3:
                {
                    playerThreeScore += score;
                    playerThreeScore = (playerThreeScore < 0 ? 0 : playerThreeScore);
                    var text = playerThreeScore.ToString();
                    if (playerOneScore < 10)
                    {
                        text = ":0" + text;
                    }
                    else
                    {
                        text = ":" + text;
                    }
                    playerThreeText.text = text;
                    if (playerThreeScore >= scoreToWin) Win(3);
                    break;
                }

            case 4:
                {
                    playerFourScore += score;
                    playerFourScore = (playerFourScore < 0 ? 0 : playerFourScore);
                    var text = playerFourScore.ToString();
                    if (playerOneScore < 10)
                    {
                        text = ":0" + text;
                    }
                    else
                    {
                        text = ":" + text;
                    }
                    playerFourText.text = text;
                    if (playerFourScore >= scoreToWin) Win(4);
                    break;
                }
        }
    }

    public int GetScore(int playerNum)
    {
        switch (playerNum)
        {
            case 1:
                return playerOneScore;
            case 2:
                return playerTwoScore;
            case 3:
                return playerThreeScore;
            case 4:
                return playerFourScore;
        }
        return -1;
    }

    private IEnumerator changeRes()
    {
        yield return new WaitForSeconds(0.1f);
        rt.sizeDelta = new Vector2(Screen.width, Screen.height);
    }

    private void HandlePlayer(GameObject handle, int playernum)
    {
        Color[] colors = new Color[7];
        colors[0] = Color.white;
        colors[1] = new Color(0.859f, 0.839f, 0.114f);
        colors[2] = new Color(0.008f, 0.503f, 0.012f);
        colors[3] = new Color(0.027f, 0.576f, 0.906f);
        colors[4] = new Color(0.667f, 0.129f, 0.765f);
        colors[5] = new Color(0.809f, 0.101f, 0.101f);
        colors[6] = new Color(0.923f, 0.509f, 0.011f);

        int colorindex = PlayerPrefs.GetInt("color" + playernum);
        if (colorindex <= 0)
        {
            handle.transform.GetChild(playernum - 1).gameObject.SetActive(false);
            transform.GetChild(playernum - 1).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(playernum - 1).gameObject.GetComponent<Text>().color = colors[colorindex];
        }
    }
}
