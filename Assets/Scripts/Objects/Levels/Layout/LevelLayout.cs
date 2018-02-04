using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLayout : ScriptableObject {
	public Space[] availableSpaces;
	public Vector2 offset;
	public Vector2Int initialPos;
	public Vector2Int elevatorTarget;
	public float spawnRate;
	public Vector2Int[] spawnPoints;
	[HideInInspector]
	public int[][] rows;

	public virtual void Init(){
		//Initialization functions
	}
}
