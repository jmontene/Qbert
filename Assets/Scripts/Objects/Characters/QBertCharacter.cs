using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBertCharacter : MonoBehaviour {
	public enum CHARACTER_TYPE {PLAYER, ENEMY};
	public float jumpTime = 1f;
	public float xDistance = 0.1f;

	protected Animator anim;
	protected bool jumping;
	protected Vector2Int dir;

	[HideInInspector]
	public Vector2Int levelPos;
	[HideInInspector]
	public CHARACTER_TYPE characterType;
	[HideInInspector]
	public string characterName;

	Level currentLevel;

	public virtual void Init(Level l){
		currentLevel = l;
		anim = GetComponentInChildren<Animator> ();
		jumping = false;
		SetAnimatorDirection (0f, -1f);
	}
	public virtual void ToggleActions(){}

	public void Update(){
		anim.SetBool ("Jumping", jumping);
	}

	public void JumpTo(Vector2Int pos){
		currentLevel.MoveCharacterTo (this, pos);
	}

	public virtual void AnimateJump (Space space, Vector3 targetPos, bool fall = false){
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

	protected void SetAnimatorDirection(float x, float y){
		anim.SetFloat ("xDir", x);
		anim.SetFloat ("yDir", y);
		dir = new Vector2Int ((int)x, (int)y);
	}
}
