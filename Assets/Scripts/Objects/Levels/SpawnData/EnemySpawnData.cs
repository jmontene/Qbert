using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySpawnData : ScriptableObject {
	public float spawnRate = 3f;
	public float spawnOffset = 0.2f;
	public abstract QBertCharacter spawnEnemy (Level level);
	public abstract void Init ();
}
