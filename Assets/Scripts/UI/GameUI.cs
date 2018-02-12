using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

	Text score;
	RectTransform livesPanel;

	// Use this for initialization
	void Start () {
		score = transform.Find ("Score").GetComponent<Text> ();
		livesPanel = transform.Find ("LivesPanel").GetComponent<RectTransform> ();
	}

	public void SetScore(int s){
		score.text = s.ToString ();
	}

	public void RemoveLife(){
		Destroy (livesPanel.GetChild (0).gameObject);
	}
}
