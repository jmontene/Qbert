using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreUI : MonoBehaviour {

	public GameManager gameManager;

	InputField field;

	// Use this for initialization
	void Start () {
		field = GetComponentInChildren<InputField> ();
	}

	public void SubmitScores(){
		if (field.text == "") {
			gameManager.SubmitHighScore ("AAA");
		} else {
			gameManager.SubmitHighScore (field.text);
		}
	}

}
