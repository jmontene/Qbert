using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnData", menuName = "Qbert/Spawn Data/Basic QBert Spawn Data", order = 1)]
public class BasicQBertSpawnData : EnemySpawnData {

	public QBertCharacter redBall;
	public QBertCharacter greenBall;
	public QBertCharacter coily;

	[Range(0f,1f)]
	public float redBallChance = 0.7f;

	int coilyCounter = 0;
	int nextCoilyValue;

	public override QBertCharacter spawnEnemy(Level level){
		Debug.Log (coilyCounter + " vs " + nextCoilyValue );
		if (!isCoilyOnLevel(level)) {
			coilyCounter += 1;
			if (coilyCounter == nextCoilyValue) {
				nextCoilyValue = Random.Range (2, 4);
				coilyCounter = 0;
				return coily;
			}
		}

		float chance = Random.Range (0f, 1f);
		if (chance <= redBallChance) {
			return redBall;
		} else {
			return greenBall;
		}
	}

	public override void Init(){
		nextCoilyValue = Random.Range (2, 4);
		coilyCounter = 0;
	}

	bool isCoilyOnLevel(Level level){
		foreach (QBertCharacter enemy in level.currentEnemies) {
			if (enemy.characterName == "Coily") {
				return true;
			}
		}

		return false;
	}
}
