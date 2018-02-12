using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour {
    private int playerOneScore = 0;
    private int playerTwoScore = 0;
    private Text playerOneText;
    private Text playerTwoText;

	void Start () {
        playerOneText = transform.GetChild(0).GetComponent<Text>();
        playerTwoText = transform.GetChild(1).GetComponent<Text>();
        playerOneText.text = "00";
        playerTwoText.text = "00";
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
    }
}
