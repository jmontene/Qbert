using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptySpace : Space {
	public override void Init(Level l, Vector2Int p){
		base.Init (l, p);
		spaceName = "EmptySpace";
	}

	public override void OnLanded(QBertCharacter character){
		character.OnFallStarted ();
	}
}
