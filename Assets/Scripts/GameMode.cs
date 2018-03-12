using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour {

    private Score sc;
    private CarrotController cc;
    private PlayerControls[] playersAlive;

    private int currentMap;
    private int numMaps;

    public enum Mode {hits, health, carrots}
    public Mode currentMode;

    public int startHealth;
    public int startCarrots;
    public int scoreToWin;

    // Use this for initialization
    void Start()
    {
        playersAlive = FindObjectsOfType<PlayerControls>();
    }

    void Awake()
    {
        sc = FindObjectOfType<Score>();
        cc = FindObjectOfType<CarrotController>();

        currentMap = PlayerPrefs.GetInt("CurrentMap");
        numMaps = PlayerPrefs.GetInt("NumMaps");
        currentMode = (Mode)PlayerPrefs.GetInt("mode" + currentMap);

        StartCoroutine(StartUp());
    }

    void Update()
    {
        int count = 0;
        foreach(var player in playersAlive)
        {
            if(player.playerNum != -1)
            {
                count++;
            }
        }

        if (currentMode == Mode.health && count == 1)
        {
            foreach (var player in playersAlive)
            {
                if (player.playerNum != -1)
                {
                    sc.Win("Player " + player.playerNum);
                }
            }
        }
    }

    private IEnumerator StartUp()
    {
        yield return new WaitForSeconds(0.01f);


            switch (currentMode)
        {
            case (Mode.health):
                
                sc.AddScore(1, startHealth);
                sc.AddScore(2, startHealth);
                sc.AddScore(3, startHealth);
                sc.AddScore(4, startHealth);
                break;
            case (Mode.carrots):
                

                sc.AddScore(1, startCarrots);
                sc.AddScore(2, startCarrots);
                sc.AddScore(3, startCarrots);
                sc.AddScore(4, startCarrots);
                break;
        }
    }
    
    public void killPlayer(int playerNumber)
    {
        foreach(var player in playersAlive)
        {
            if(player.playerNum == playerNumber)
            {
                player.playerNum = -1;
            }
        }
    }

}
