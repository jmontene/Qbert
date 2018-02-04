using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnData", menuName = "Qbert/Spawn Data/Basic QBert Spawn Data", order = 1)]
public class BasicQBertSpawnData : EnemySpawnData {

	public QBertCharacter redBall;
	public QBertCharacter greenBall;

	[Range(0f,1f)]
	public float redBallChance = 0.7f;

	public override QBertCharacter spawnEnemy(Level level){
		float chance = Random.Range (0f, 1f);
		if (chance <= redBallChance) {
			return redBall;
		} else {
			return greenBall;
		}
	}
}
