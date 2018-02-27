using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string playernum = PlayerPrefs.GetString("Winner");
        Text text = GetComponent<Text>();
        text.text = playernum + " Wins!";
        text.enabled = true;
	}
	
}
