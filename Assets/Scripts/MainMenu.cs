using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Text StartButton;
    public Text TutorialButton;
    public Text QuitButton;
    public PlayerControls player;
    public AudioSource click;

    private Text[] texts;
    private int selected = 0;
    private Color c;
    private float delay = 0f;

    void Start()
    {
        c = StartButton.color;
        texts = new Text[3];
        texts[0] = StartButton;
        texts[1] = TutorialButton;
        texts[2] = QuitButton;      
    }

    void Update()
    {
        int oldselected = selected;

        HandleInput();

        if (selected != oldselected)
        {
            texts[selected].color = c;
            texts[selected].fontSize = 70;

            texts[oldselected].color = Color.white;
            texts[oldselected].fontSize = 50;

            click.Play();
        }
    }

    private void HandleInput()
    {

        if (player.getJumpAxis() || Input.GetKeyDown(KeyCode.Return))
        {
            switch (selected)
            {
                case 0:
                    {
                        goToGame();
                        break;
                    }
                case 1:
                    {
                        goToTutorial();
                        break;
                    }
                case 2:
                    {
                        quitGame();
                        break;
                    }
            }
        }

        delay += Time.deltaTime;

        if (delay < 0.2f)
        {
            return;
        }
        delay = 0f;

        if (player.getVerticalAxis() > 0 || Input.GetKeyDown(KeyCode.UpArrow))
        {
            selected = Mathf.Max(0, selected - 1);
        }

        if (player.getVerticalAxis() < 0 || Input.GetKeyDown(KeyCode.DownArrow))
        {
            selected = Mathf.Min(2, selected + 1);
        }
    }

    public void goToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

	public void goToGame()
	{
		SceneManager.LoadScene("Select");		
	}

	public void quitGame()
	{
		Application.Quit();		
	}
}
