﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

	public LevelLayout layout;
	public QBertCharacter qbertPrefab;

	[HideInInspector]
	public Space[][] spaces;
	QBertCharacter qbert;

	// Use this for initialization
	void Start () {
		MakeLevel ();
		qbert = Instantiate (qbertPrefab, transform.position, Quaternion.identity) as QBertCharacter;
		qbert.Init ();
		qbert.SetLevel (this);
		MoveCharacterTo (qbert, layout.initialPos, true);
	}

	void MakeLevel(){
		layout.Init ();
		int[][] rows = layout.rows;
		spaces = new Space[rows.GetLength(0)][];

		for (int i = 0; i < rows.Length; ++i) {
			spaces [i] = new Space[rows [i].Length];
			for (int j = 0; j < rows [i].Length; ++j) {
				if (rows [i] [j] != 0) {
					Vector2 newPos = new Vector2 (transform.position.x + j * layout.offset.x - i * layout.offset.x, transform.position.y + j * layout.offset.y + i * layout.offset.y);
					spaces [i] [j] = Instantiate (layout.availableSpaces [rows [i] [j] - 1], newPos, Quaternion.identity, transform) as Space;
					spaces [i] [j].Init (this);
				} else {
					spaces [i] [j] = null;
				}
			}
		}
	}

	public void MoveCharacterTo(QBertCharacter character, Vector2Int pos, bool teleport = false, bool fall = false){
		Space s;
		if (pos.x < 0 || pos.x >= spaces.GetLength (0) || pos.y < 0 || pos.y >= spaces [0].Length) {
			s = null;		
		} else {
			s = spaces [pos.x] [pos.y];
		}
		if (s == null) {
			Debug.LogWarning ("Null space found in level");
			return;
		}

		character.levelPos = pos;

		if (teleport) {
			s.Teleport (character);
		} else {
			s.Move (character, fall);
		}
	}

}
