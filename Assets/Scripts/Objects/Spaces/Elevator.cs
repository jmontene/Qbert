using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : Space {
	public float yOffset = 0.2f;
	public float arrivalTime = 2f;
	public AudioClip elevatorSFX;

	[HideInInspector]
	public bool used;

	public override void Init(Level l, Vector2Int p){
		base.Init (l, p);
		spaceName = "Elevator";
		used = false;
	}

	public override void OnLanded (QBertCharacter character){
		if (!used && character.characterType == QBertCharacter.CHARACTER_TYPE.PLAYER) {
			((QBert)character).ridingElevator = true;
			SoundManager.instance.PlaySFX (character.landSFX);
			character.transform.SetParent (transform);
			landingPoint.SetParent (level.transform);
			StartCoroutine (MoveToTarget (character));
			spaceName = "MovingElevator";
		} else {
			character.OnFallStarted ();
		}
	}

	IEnumerator MoveToTarget(QBertCharacter character){
		SoundManager.instance.PlaySFX (elevatorSFX);
		used = true;
		character.ToggleActions ();
		Vector2Int pos = level.layout.elevatorTarget;
		Vector3 target = level.spaces [pos.x] [pos.y].landingPoint.position;
		target.y += yOffset;
		Vector3 origPos = transform.position;

		float t = 0f;
		while (t < arrivalTime) {
			t += Time.deltaTime;
			transform.position = Vector3.Lerp (origPos, target, t / arrivalTime);
			yield return null;
		}

		character.ToggleActions ();
		((QBert)character).ridingElevator = false;
		level.MoveCharacterTo (character, level.layout.elevatorTarget, false, true);
		character.transform.SetParent (level.transform);
		spaceName = "Elevator";
		GetComponentInChildren<SpriteRenderer> ().enabled = false;
	}
}
