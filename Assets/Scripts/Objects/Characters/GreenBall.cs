﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBall : Ball {
	public AudioClip obtainedSFX;

	protected override void OnQBertCollision(QBert qbert){
		currentLevel.AddScore (100);
		SoundManager.instance.PlaySFX (obtainedSFX);
		currentLevel.OnGreenBall ();
		currentLevel.RemoveEnemy (this);
	}
}
