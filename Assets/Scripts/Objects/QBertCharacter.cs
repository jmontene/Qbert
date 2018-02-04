using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBertCharacter : MonoBehaviour {
	public enum CHARACTER_TYPE {PLAYER, ENEMY};

	[HideInInspector]
	public Vector2Int levelPos;
	[HideInInspector]
	public CHARACTER_TYPE characterType;

	Level currentLevel;

	public virtual void Init(){}
	public virtual void AnimateJump(Space space, Vector3 targetPos, bool fall = false){}
	public virtual void ToggleActions(){}

	public void SetLevel(Level l){
		currentLevel = l;
	}

	public void JumpTo(Vector2Int pos){
		currentLevel.MoveCharacterTo (this, pos);
	}
}
