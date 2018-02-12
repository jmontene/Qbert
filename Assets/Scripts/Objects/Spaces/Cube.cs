using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : Space {

	public Sprite landedSprite;
	public float flashSpeed = 0.1f;
	public List<Sprite> flashingSprites;

	[HideInInspector]
	public bool lighted = false;
	int flashIndex = 0;
	SpriteRenderer childRenderer;

	public override void Init(Level l, Vector2Int p){
		base.Init (l, p);
		spaceName = "Cube";
		childRenderer = GetComponentInChildren<SpriteRenderer> (); 
	}

	public override void OnLanded(QBertCharacter character){
		if (!lighted && character.characterType == QBertCharacter.CHARACTER_TYPE.PLAYER) {
			childRenderer.sprite = landedSprite;
			lighted = true;
			level.OnLightedCube ();
		}
		character.ReloadMovement ();
		SoundManager.instance.PlaySFX (character.landSFX);
	}

	public void StartFlash(){
		InvokeRepeating ("Flash", 0f, flashSpeed);
	}

	void Flash(){
		childRenderer.sprite = flashingSprites [flashIndex];
		flashIndex = (flashIndex + 1) % flashingSprites.Count;
	}
}
