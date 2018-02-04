using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLayout : ScriptableObject {
	public Space[] availableSpaces;
	public Vector2 offset;
	public Vector2Int initialPos;
	public Vector2Int elevatorTarget;
	[HideInInspector]
	public int[][] rows;

	public virtual void Init(){
		//Initialization functions
	}
}
