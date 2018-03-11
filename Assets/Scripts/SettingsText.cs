using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsText : MonoBehaviour {

    private Text text;
    private int selected;
    public string basetext;

	void Start () {
        text = GetComponent<Text>();
	}
	
	void Update () {

    }

    public void UpdateText(string newtext, int newselected)
    {
        selected = newselected;
        text.text = basetext;
        IEnumerator addLetters = AddLetters(newtext, newselected);
        StartCoroutine(addLetters);
    }

    IEnumerator AddLetters(string newtext, int newselected)
    {
        foreach (char c in newtext)
        {
            if (newselected != selected)
                break;
            text.text += c;
            yield return null;
            yield return null;
        }
    }
}
