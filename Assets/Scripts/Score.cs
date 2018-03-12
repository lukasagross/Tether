using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using XInputDotNetPure;

public class Score : MonoBehaviour {
    public int playerOneScore = 0;
    private int playerTwoScore = 0;
    private int playerThreeScore = 0;
    private int playerFourScore = 0;
    private Text playerOneText;
    private Text playerTwoText;
    private Text playerThreeText;
    private Text playerFourText;
    private RectTransform rt;
    private int scoreToWin;

	void Start () {
        playerOneText = transform.GetChild(0).GetComponent<Text>();
        playerTwoText = transform.GetChild(1).GetComponent<Text>();
        playerThreeText = transform.GetChild(2).GetComponent<Text>();
        playerFourText = transform.GetChild(3).GetComponent<Text>();
        rt = GetComponent<RectTransform>();
        playerOneText.text = "00";
        playerTwoText.text = "00";
    }

    void Awake()
    {
        scoreToWin = FindObjectOfType<GameMode>().scoreToWin;
        StartCoroutine(changeRes());
    }

    public void Win(string playernum)
    {
        PlayerPrefs.SetString("Winner", playernum);
        PlayerPrefs.Save();
        Time.timeScale = 0;
        System.Threading.Thread.Sleep(1000);
        GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
        GamePad.SetVibration(PlayerIndex.Two, 0f, 0f);
        GamePad.SetVibration(PlayerIndex.Three, 0f, 0f);
        GamePad.SetVibration(PlayerIndex.Four, 0f, 0f);
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
                    if (playerOneScore < 10) text = "0" + text;
                    playerOneText.text = text;
                    if (playerOneScore >= scoreToWin) Win("Player One");
                    break;
                }

            case 2:
                {
                    playerTwoScore += score;
                    playerTwoScore = (playerTwoScore < 0 ? 0 : playerTwoScore);
                    var text = playerTwoScore.ToString();
                    if (playerTwoScore < 10) text = "0" + text;
                    playerTwoText.text = text;
                    if (playerTwoScore >= scoreToWin) Win("Player Two");
                    break;
                }

            case 3:
                {
                    playerThreeScore += score;
                    playerThreeScore = (playerThreeScore < 0 ? 0 : playerThreeScore);
                    var text = playerThreeScore.ToString();
                    if (playerThreeScore < 10) text = "0" + text;
                    playerThreeText.text = text;
                    if (playerThreeScore >= scoreToWin) Win("Player Three");
                    break;
                }

            case 4:
                {
                    playerFourScore += score;
                    playerFourScore = (playerFourScore < 0 ? 0 : playerFourScore);
                    var text = playerFourScore.ToString();
                    if (playerFourScore < 10) text = "0" + text;
                    playerFourText.text = text;
                    if (playerFourScore >= scoreToWin) Win("Player Four");
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
}
