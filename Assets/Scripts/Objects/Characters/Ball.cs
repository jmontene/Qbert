using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : QBertCharacter {

	public override void Init(Level l){
		base.Init (l);
		characterType = CHARACTER_TYPE.ENEMY;
	}

	public new void Update(){
		base.Update ();
		BallBehaviour ();
	}

	protected virtual void BallBehaviour(){
		if (canMove) {
			BallMovement ();
		}
	}

	protected void BallMovement(){
		int dir = Random.Range (0, 2);
		switch(dir){
			case 0:
				SetAnimatorDirection (1f, 0f);
				JumpTo (new Vector2Int (levelPos.x, levelPos.y + 1));
				break;
			case 1:
				SetAnimatorDirection (0f, -1f);
				JumpTo (new Vector2Int (levelPos.x + 1, levelPos.y));
				break;
		}
	}

	protected virtual void OnQBertCollision(QBert qbert){
	}

	protected override void OnFallEnded(){
		currentLevel.RemoveEnemy(this);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			OnQBertCollision (other.gameObject.GetComponent<QBert> ());
		} else if (other.tag == "Deadzone") {
			fallFinished = true;
			OnFallEnded ();
		}
	}
}
