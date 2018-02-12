using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coily : Ball {

	public AnimatorOverrideController coilyController;
	public AudioClip coilyLandClip;
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
		if (frozen) {
			return;
		}

		if (!born) {
			if (canMove) {
				BallMovement ();
			}
			if (currentLevel.layout.bottomPositions.Contains (levelPos)) {
				born = true;
				GetComponentInChildren<Animator> ().runtimeAnimatorController = coilyController;
				transform.Find ("sprite").transform.Translate (Vector2.up * ballOffset);
				landSFX = coilyLandClip;
			}
		} else {
			if (canMove) {
				CoilyMovement ();
			}
		}
	}

	protected override void OnFallEnded(){
		canCollide = true;
		currentLevel.AddScore (500);
		currentLevel.RemoveEnemy (this);
	}

	protected override void OnQBertCollision(QBert qbert){
		if (!qbert.ridingElevator && qbert.canCollide) {
			currentLevel.KillQBert ();
		}
	}

	protected void CoilyMovement(){

		Vector2Int qbertPos = qbert.levelPos;
		List<float> distances = new List<float>();
		Dictionary<float, Vector2Int> positions = new Dictionary<float, Vector2Int> ();

		Vector2Int down = new Vector2Int (1, 0);
		Vector2Int up = new Vector2Int (-1, 0);
		Vector2Int right = new Vector2Int (0, 1);
		Vector2Int left = new Vector2Int (0, -1);

		float d;

		Space dSpace = currentLevel.GetSpaceAt (levelPos + down);
		if (dSpace != null && dSpace.spaceName != "Elevator" && dSpace.spaceName != "EmptySpace") {
			d = Vector2Int.Distance (qbertPos, levelPos + down);
			distances.Add(d);
			positions [d] = down;
		}

		Space uSpace = currentLevel.GetSpaceAt (levelPos + up);
		if (uSpace != null && uSpace.spaceName != "Elevator" && uSpace.spaceName != "EmptySpace") {
			d = Vector2Int.Distance (qbertPos, levelPos + up);
			distances.Add(d);
			positions [d] = up;
		}

		Space rSpace = currentLevel.GetSpaceAt (levelPos + right);
		if (rSpace != null && rSpace.spaceName != "Elevator" && rSpace.spaceName != "EmptySpace") {
			d = Vector2Int.Distance (qbertPos, levelPos + right);
			distances.Add(d);
			positions [d] = right;
		}

		Space lSpace = currentLevel.GetSpaceAt (levelPos + left);
		if (lSpace != null && lSpace.spaceName != "Elevator" && lSpace.spaceName != "EmptySpace") {
			d = Vector2Int.Distance (qbertPos, levelPos + left);
			distances.Add(d);
			positions [d] = left;
		}

		float dist = Mathf.Min (distances.ToArray());
		Vector2Int finalPos = positions [dist];
		SetAnimatorDirection (finalPos.y, -finalPos.x);
		JumpTo (levelPos + finalPos);
	}
}
