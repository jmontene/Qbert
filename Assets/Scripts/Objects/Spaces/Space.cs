using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Space : MonoBehaviour {
	[HideInInspector]
	public Transform landingPoint;
	[HideInInspector]
	public string spaceName;
	[HideInInspector]
	public Vector2Int pos;

	protected Level level;

	public virtual void Init(Level l, Vector2Int p){
		level = l;
		pos = p;
		landingPoint = transform.Find ("LandingPoint");
		if (landingPoint == null) {
			landingPoint = transform;
		}
	}

	public virtual void Move (QBertCharacter character, bool fall = false){
		character.AnimateJump (this, landingPoint.position, fall);
	}
	public abstract void OnLanded (QBertCharacter character);

	public virtual void Teleport(QBertCharacter character){
		character.levelPos = pos;
		character.transform.position = landingPoint.position;
	}
}
