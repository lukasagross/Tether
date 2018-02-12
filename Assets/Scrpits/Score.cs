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
    }
	
    public void AddScore(int playernum, int score)
    {
        if (playernum == 1)
        {
            playerOneScore += score;
            playerOneText.text = playerOneScore.ToString();
        }

        else if (playernum == 2)
        {
            playerTwoScore += score;
            playerTwoText.text = playerTwoScore.ToString();
        }
    }
}
