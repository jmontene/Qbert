using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Space : MonoBehaviour {
	[HideInInspector]
	public Transform landingPoint;

	protected Level level;

	public void Init(Level l){
		level = l;
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
		character.transform.position = landingPoint.position;
	}
}
