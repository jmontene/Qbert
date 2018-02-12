using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBertCharacter : MonoBehaviour {
	public enum CHARACTER_TYPE {PLAYER, ENEMY};
	public float jumpTime = 1f;
	public float xDistance = 0.1f;
	public float landTime = 0f;
	public float fallSpeed = 0.2f;
	public string fallLayer;

	public AudioClip landSFX;
	public AudioClip fallSFX;

	protected Animator anim;
	protected bool jumping;
	[HideInInspector]
	public bool canMove;
	[HideInInspector]
	public bool frozen;
	[HideInInspector]
	public bool canCollide;
	protected Vector2Int dir;
	protected bool fallFinished = false;

	[HideInInspector]
	public Vector2Int levelPos;
	[HideInInspector]
	public CHARACTER_TYPE characterType;
	[HideInInspector]
	public string characterName;

	protected Level currentLevel;

	public virtual void Init(Level l){
		canCollide = true;
		frozen = false;
		currentLevel = l;
		anim = GetComponentInChildren<Animator> ();
		jumping = false;
		canMove = true;
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
		canMove = false;
		jumping = true;
		float mt = (dir.y == 0 ? 1f : -1f);
		if (fall) {
			mt = 0f;
		}
		Vector3 origPos = transform.position;
		float t = 0f;
		while (t < jumpTime) {
			if (frozen) {
				yield return null;
				continue;
			}
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
		OnSpaceLand (space);
	}

	protected void SetAnimatorDirection(float x, float y){
		anim.SetFloat ("xDir", x);
		anim.SetFloat ("yDir", y);
		dir = new Vector2Int ((int)x, (int)y);
	}

	public virtual void OnSpaceLand(Space s){
		s.OnLanded (this);
		levelPos = s.pos;
	}

	public void ReloadMovement(){
		StartCoroutine (WaitToMove ());
	}

	public virtual void OnFallStarted(){
		canCollide = false;
		if (fallSFX != null) {
			SoundManager.instance.PlaySFX (fallSFX);
			Invoke ("FinishFall", fallSFX.length + 1f);
		}
		GetComponentInChildren<SpriteRenderer> ().sortingLayerName = fallLayer;
		StartCoroutine (AnimateFallCo ());
	}

	protected IEnumerator AnimateFallCo(){
		canMove = false;
		while (!fallFinished) {
			if (frozen) {
				yield return null;
				continue;
			}
			transform.Translate (Vector2.down * fallSpeed * Time.deltaTime);
			yield return null;
		}
	}

	protected virtual void OnFallEnded(){
	}

	protected IEnumerator WaitToMove(){
		yield return new WaitForSeconds (landTime);
		canMove = true;
	}

	void FinishFall(){
		fallFinished = true;
		OnFallEnded ();
		Invoke ("FinishFallEnd", 0.1f);
	}

	void FinishFallEnd(){
		fallFinished = false;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Deadzone") {
			fallFinished = true;
			OnFallEnded ();
		}
	}
}
