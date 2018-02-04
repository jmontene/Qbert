using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBert : QBertCharacter {
	public float jumpTime = 1f;
	public float xDistance = 0.1f;

	Animator anim;
	bool jumping;
	[HideInInspector]
	public bool listenToInput = true;
	Vector2Int dir;

	public override void Init(){
		anim = GetComponentInChildren<Animator> ();
		jumping = false;
		SetAnimatorDirection (0f, -1f);
		characterType = CHARACTER_TYPE.PLAYER;
	}
	
	// Update is called once per frame
	void Update () {
		if (!jumping && listenToInput) {
			ProcessInput ();
		}
		anim.SetBool ("Jumping", jumping);
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

	void SetAnimatorDirection(float x, float y){
		anim.SetFloat ("xDir", x);
		anim.SetFloat ("yDir", y);
		dir = new Vector2Int ((int)x, (int)y);
	}

	public override void ToggleActions(){
		listenToInput = !listenToInput;
	}

	public override void AnimateJump (Space space, Vector3 targetPos, bool fall = false){
		StartCoroutine (AnimateJumpCo (space, targetPos, fall));
	}

	IEnumerator AnimateJumpCo(Space space, Vector3 targetPos, bool fall){
		jumping = true;
		float mt = (dir.y == 0 ? 1f : -1f);
		if (fall) {
			mt = 0f;
		}
		Vector3 origPos = transform.position;
		float t = 0f;
		while (t < jumpTime) {
			t += Time.deltaTime;
			float p = t / jumpTime;
			transform.position = Vector3.Lerp (origPos, targetPos, p);

			if (p <= 0.5f) {
				transform.Translate (Vector2.right * xDistance * mt * 2 * p);
			} else {
				transform.Translate (Vector2.right * xDistance * mt * 2 * (1f - p));
			}
			yield return null;
		}
		transform.position = targetPos;
		jumping = false;
		space.OnLanded (this);
	}
}
