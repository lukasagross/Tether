using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsText : MonoBehaviour {

    private Text text;
    public string basetext;

	void Start () {
        text = GetComponent<Text>();
	}
	
	void Update () {

    }

    public void UpdateText(string newtext)
    {
        text.text = basetext;
        IEnumerator addLetters = AddLetters(newtext);
        StartCoroutine(addLetters);
    }

    IEnumerator AddLetters(string newtext)
    {
        bool skip = false;
        foreach (char c in newtext)
        {
            
            text.text += c;
            yield return null;
            yield return null;
        }
    }
}
