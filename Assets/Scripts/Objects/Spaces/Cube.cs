using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : Space {

	public Sprite landedSprite;
	bool lighted = false;

	public override void OnLanded(QBertCharacter character){
		if (!lighted && character.characterType == QBertCharacter.CHARACTER_TYPE.PLAYER) {
			GetComponentInChildren<SpriteRenderer> ().sprite = landedSprite;
			lighted = true;
		}
	}
}
