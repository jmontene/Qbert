using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : Space {
	public float yOffset = 0.2f;
	public float arrivalTime = 2f;

	public override void Init(Level l, Vector2Int p){
		base.Init (l, p);
		spaceName = "Elevator";
	}

	public override void OnLanded (QBertCharacter character){
		if (character.characterType == QBertCharacter.CHARACTER_TYPE.PLAYER) {
			character.transform.SetParent (transform);
			StartCoroutine (MoveToTarget (character));
		}
	}

	IEnumerator MoveToTarget(QBertCharacter character){
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
		level.MoveCharacterTo (character, level.layout.elevatorTarget, false, true);
		character.transform.SetParent (null);
		Destroy (gameObject);
	}
}
