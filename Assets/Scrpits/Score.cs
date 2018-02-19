using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

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
        StartCoroutine(changeRes());
    }
	
    public void AddScore(int playernum, int score)
    {
        if (playernum == 1)
        {
            playerOneScore += score;
            var text = playerOneScore.ToString();
            if (playerOneScore < 10) text = "0" + text;
            playerOneText.text = text;
        }

        else if (playernum == 2)
        {
            playerTwoScore += score;
            var text = playerTwoScore.ToString();
            if (playerTwoScore < 10) text = "0" + text;
            playerTwoText.text = text;
        }

        else if (playernum == 3)
        {
            playerThreeScore += score;
            var text = playerThreeScore.ToString();
            if (playerTwoScore < 10) text = "0" + text;
            playerThreeText.text = text;
        }

        else if (playernum == 4)
        {
            playerFourScore += score;
            var text = playerFourScore.ToString();
            if (playerTwoScore < 10) text = "0" + text;
            playerFourText.text = text;
        }
    }

    private IEnumerator changeRes()
    {
        yield return new WaitForSeconds(0.1f);
        rt.sizeDelta = new Vector2(Screen.width, Screen.height);
    }
}
