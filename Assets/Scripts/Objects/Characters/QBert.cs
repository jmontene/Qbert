﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBert : QBertCharacter {
	[HideInInspector]
	public bool listenToInput = true;
	[HideInInspector]
	public bool ridingElevator = false;

	public AudioClip deathSFX;

	GameObject speechBubble;
	SpriteRenderer mainRenderer;

	public override void Init(Level l){
		base.Init (l);
		characterType = CHARACTER_TYPE.PLAYER;
		characterName = "QBert";
		speechBubble = transform.Find ("speech_bubble").gameObject;
		mainRenderer = transform.Find ("sprite").GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	public new void Update () {
		base.Update ();
		if (canMove && listenToInput) {
			ProcessInput ();
		}
	}

	void ProcessInput(){
		if (Input.GetKeyDown (KeyCode.Keypad9)) {
			SetAnimatorDirection (0f, 1f);
			JumpTo (new Vector2Int (levelPos.x - 1, levelPos.y));
		} else if (Input.GetKeyDown (KeyCode.Keypad1)) {
			SetAnimatorDirection (0f, -1f);
			JumpTo (new Vector2Int (levelPos.x + 1, levelPos.y));
		}else if (Input.GetKeyDown (KeyCode.Keypad7)) {
			SetAnimatorDirection (-1f, 0f);
			JumpTo (new Vector2Int (levelPos.x, levelPos.y - 1));
		} else if (Input.GetKeyDown (KeyCode.Keypad3)) {
			SetAnimatorDirection (1f, 0f);
			JumpTo (new Vector2Int (levelPos.x, levelPos.y + 1));
		}
	}

	public override void ToggleActions(){
		listenToInput = !listenToInput;
	}

	protected override void OnFallEnded(){
		mainRenderer.sortingLayerName = "Actors";
		currentLevel.OnQBertFallEnd ();
		canMove = true;
	}

	public void SetSpeechBubble(bool s){
		if (s) {
			SoundManager.instance.PlaySFX (deathSFX);
		}
		speechBubble.SetActive (s);
	}

	void OnTriggerEnter2D(Collider2D other){
	}
}
