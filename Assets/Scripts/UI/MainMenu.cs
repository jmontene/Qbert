using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void OnStart(){
		SceneManager.LoadScene ("game");
	}

	public void OnLeaderboards(){
		Debug.Log("Leaderboards pressed");
	}

	public void OnQuit(){
		Application.Quit();
	}
}
