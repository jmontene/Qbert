using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int lives = 3;
	public GameObject pauseCanvas;
	public GameUI gameUI;
	public GameObject HighScoreCanvas;
	public GameObject GameOverCanvas;

	bool paused = false;

	[HideInInspector]
	public int score = 0;
	
	// Update is called once per frame
	void Update () {
		if (!paused && Input.GetKeyDown (KeyCode.Escape)) {
			Pause ();
		}
	}

	public void Pause(float s = 0f){
		if (s == 0) {
			paused = true;
			pauseCanvas.SetActive (paused);
			Time.timeScale = 0f;
		} else {
			StartCoroutine (PauseForSeconds (s));
		}
	}

	public void UnPause(){
		paused = false;
		Time.timeScale = 1f;
		pauseCanvas.SetActive (paused);
	}

	public void QuitGame(){
		Time.timeScale = 1f;
		LeaderboardsManager.instance.SaveRecords ();
		SceneManager.LoadScene ("menu");
	}

	public void AddScore(int s){
		score += s;
		gameUI.SetScore (score);
	}

	public void SubmitHighScore(string name){
		LeaderboardsManager.instance.AddRecord (name, score);
		QuitGame ();
	}

	public void ShowHighScoreWindow(){
		GameOverCanvas.SetActive (false);
		HighScoreCanvas.SetActive (true);
	}

	public void ShowGameOverWindow(){
		GameOverCanvas.SetActive (true);
	}

	public void LoseLife(){
		lives -= 1;
		if (lives > 0) {
			gameUI.RemoveLife ();
		}
	}

	IEnumerator PauseForSeconds(float s){
		paused = true;
		Time.timeScale = 0f;
		yield return new WaitForSecondsRealtime (s);
		paused = false;
		Time.timeScale = 1f;
	}

}
