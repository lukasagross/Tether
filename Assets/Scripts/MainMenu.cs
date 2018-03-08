using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void goToGame()
	{
		SceneManager.LoadScene("UnderwaterMap");
		
	}

	public void quitGame()
	{
		Application.Quit();
		Debug.Log("Quitting game");
		
	}
}
