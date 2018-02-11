using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void OnStart(){
		SceneManager.LoadScene ("game");
	}

	public void OnLeaderboards(){
		SceneManager.LoadScene ("leaderboards");
	}

	public void OnBackToMain(){
		SceneManager.LoadScene ("menu");
	}

	public void OnQuit(){
		Application.Quit();
	}
}
