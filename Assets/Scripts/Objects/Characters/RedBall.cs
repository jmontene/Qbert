﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBall : Ball {

	protected override void OnQBertCollision(QBert qbert){
		if (!qbert.ridingElevator && qbert.canCollide) {
			currentLevel.KillQBert ();
		}
	}
}
