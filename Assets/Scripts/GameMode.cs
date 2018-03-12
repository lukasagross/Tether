using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour {

    private Score sc;
    private CarrotController cc;

    public enum Mode {hits, health, carrots}
    public Mode currentMode;
    

    // Use this for initialization
    void Awake()
    {
        sc = FindObjectOfType<Score>();
        cc = FindObjectOfType<CarrotController>();
        StartCoroutine(StartUp());
    }

    private IEnumerator StartUp()
    {
        yield return new WaitForSeconds(0.01f);


            switch (currentMode)
        {
            case (Mode.hits):
                cc.active = false;

                break;
            case (Mode.health):
                cc.active = false;

                sc.AddScore(1, 10);
                sc.AddScore(2, 10);
                sc.AddScore(3, 10);
                sc.AddScore(4, 10);
                break;
            case (Mode.carrots):
                cc.active = true;

                sc.AddScore(1, 3);
                sc.AddScore(2, 3);
                sc.AddScore(3, 3);
                sc.AddScore(4, 3);
                break;
        }
    }
    
}
