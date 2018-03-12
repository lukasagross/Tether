using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public GameObject Round1;
    public GameObject Round2;
    public GameObject Round3;
    public GameObject Round4;

    private Color[] colors;
    private string[] winners;
    private int nummaps;
    private int winner1;
    private int winner2;
    private int winner3;
    private int winner4;
    private int color1;
    private int color2;
    private int color3;
    private int color4;

    void Start () {
        colors = new Color[7];
        colors[0] = Color.white;
        colors[1] = new Color(0.859f, 0.839f, 0.114f);
        colors[2] = new Color(0.008f, 0.503f, 0.012f);
        colors[3] = new Color(0.027f, 0.576f, 0.906f);
        colors[4] = new Color(0.667f, 0.129f, 0.765f);
        colors[5] = new Color(0.809f, 0.101f, 0.101f);
        colors[6] = new Color(0.923f, 0.509f, 0.011f);

        winners = new string[5];
        winners[0] = "Error";
        winners[1] = "Player One";
        winners[2] = "Player Two";
        winners[3] = "Player Three";
        winners[4] = "Player Four";


        color1 = PlayerPrefs.GetInt("color1");
        color2 = PlayerPrefs.GetInt("color2");
        color3 = PlayerPrefs.GetInt("color3");
        color4 = PlayerPrefs.GetInt("color4");


        nummaps = PlayerPrefs.GetInt("NumMaps");
        switch (nummaps)
        {
            case 0:
                {
                    Debug.Log("ERROR: No maps");
                    break;
                }
            case 1:
                {
                    HandleRound1();
                    break;
                }
            case 2:
                {
                    HandleRound1();
                    HandleRound2();
                    break;
                }
            case 3:
                {
                    HandleRound1();
                    HandleRound2();
                    HandleRound3();
                    break;
                }
            case 4:
                {
                    HandleRound1();
                    HandleRound2();
                    HandleRound3();
                    HandleRound4();
                    break;
                }
        }

        PlayerPrefs.SetInt("CurrentPlayer", 1);
        PlayerPrefs.SetInt("NumMaps", 0);
        PlayerPrefs.SetInt("color1", 0);
        PlayerPrefs.SetInt("color2", 0);
        PlayerPrefs.SetInt("color3", 0);
        PlayerPrefs.SetInt("color4", 0);
    }

    private void HandleRound1()
    {
        winner1 = PlayerPrefs.GetInt("winner1");
        Round1.SetActive(true);
        Text Text1 = Round1.GetComponent<Text>();
        Text1.text = winners[winner1];
        Text1.color = colors[winner1];
    }

    private void HandleRound2()
    {
        winner2 = PlayerPrefs.GetInt("winner2");
        Round2.SetActive(true);
        Text Text2 = Round2.GetComponent<Text>();
        Text2.text = winners[winner2];
        Text2.color = colors[winner2];
    }

    private void HandleRound3()
    {
        winner3 = PlayerPrefs.GetInt("winner3");
        Round3.SetActive(true);
        Text Text3 = Round2.GetComponent<Text>();
        Text3.text = winners[winner3];
        Text3.color = colors[winner3];
    }

    private void HandleRound4()
    {
        winner4 = PlayerPrefs.GetInt("winner4");
        Round4.SetActive(true);
        Text Text4 = Round2.GetComponent<Text>();
        Text4.text = winners[winner4];
        Text4.color = colors[winner4];
    }


    void Update () {
		
	}
}
