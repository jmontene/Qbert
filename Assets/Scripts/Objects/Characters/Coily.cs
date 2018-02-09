using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coily : Ball {

	public AnimatorOverrideController coilyController;
	public float ballOffset = 0.05f;

	[HideInInspector]
	public QBertCharacter qbert;

	bool born = false;

	public override void Init(Level l){
		base.Init (l);
		qbert = l.qbert;
		characterName = "Coily";
	}

	protected override void BallBehaviour(){
		if (!born) {
			if (canMove) {
				BallMovement ();
			}
			if (currentLevel.layout.bottomPositions.Contains (levelPos)) {
				born = true;
				GetComponentInChildren<Animator> ().runtimeAnimatorController = coilyController;
				transform.Find ("sprite").transform.Translate (Vector2.up * ballOffset);
			}
		} else {
			if (canMove) {
				CoilyMovement ();
			}
		}
	}

	protected void CoilyMovement(){
		Vector2Int qbertPos = qbert.levelPos;
		List<float> distances = new List<float>();
		Dictionary<float, Vector2Int> positions = new Dictionary<float, Vector2Int> ();

		Vector2Int down = new Vector2Int (levelPos.x + 1, levelPos.y);
		Vector2Int up = new Vector2Int (levelPos.x - 1, levelPos.y);
		Vector2Int right = new Vector2Int (levelPos.x, levelPos.y + 1);
		Vector2Int left = new Vector2Int (levelPos.x, levelPos.y - 1);

		float d;

		Space dSpace = currentLevel.GetSpaceAt (down);
		if (dSpace != null && dSpace.spaceName != "Elevator") {
			d = Vector2Int.Distance (qbertPos, down);
			distances.Add(d);
			positions [d] = down;
		}

		Space uSpace = currentLevel.GetSpaceAt (up);
		if (uSpace != null && uSpace.spaceName != "Elevator") {
			d = Vector2Int.Distance (qbertPos, up);
			distances.Add(d);
			positions [d] = up;
		}

		Space rSpace = currentLevel.GetSpaceAt (right);
		if (rSpace != null && rSpace.spaceName != "Elevator") {
			d = Vector2Int.Distance (qbertPos, right);
			distances.Add(d);
			positions [d] = right;
		}

		Space lSpace = currentLevel.GetSpaceAt (left);
		if (lSpace != null && lSpace.spaceName != "Elevator") {
			d = Vector2Int.Distance (qbertPos, left);
			distances.Add(d);
			positions [d] = left;
		}

		float dist = Mathf.Min (distances.ToArray());
		JumpTo (positions [dist]);
	}
}
