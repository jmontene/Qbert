using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject pauseCanvas;

	bool paused = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!paused && Input.GetKeyDown (KeyCode.Escape)) {
			paused = true;
			pauseCanvas.SetActive (paused);
			Time.timeScale = 0f;
		}
	}

	public void UnPause(){
		paused = false;
		Time.timeScale = 1f;
		pauseCanvas.SetActive (paused);
	}

	public void QuitGame(){
		Time.timeScale = 1f;
		SceneManager.LoadScene ("menu");
	}

}
